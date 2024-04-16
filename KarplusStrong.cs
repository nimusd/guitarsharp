using NAudio.Dsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;using NAudio.Dsp;
using NAudio.Wave;



namespace Guitarsharp
{

    [Serializable]
    public class KarplusStrong : ISampleProvider
    {

        public readonly List<float> delayLine;
        private readonly List<float> excitationSample;
        private int pos = 0;
        public float frequency;
        public float lowPassCutOffValue { get; set; } = 300;
        public float lowPassQValue { get; set; } = 1;
        public bool lowPassActive = false;
        private float previousSample = 0.0f;
        public int attackPhaseSamples { get; set; } = 1;// lowest value
       
        public float damping
        {
            get { return _damping; }
            set
            {
                // Ensure the damping value is within the range 0.980 to 0.999
                _damping = Math.Max(0.980f, Math.Min(0.999f, value));
            }
        }
        private float _damping = 0.989f;

        private List<float> sampleBuffer = new List<float>();
        private int bufferLength = 1024; // Example size, adjust as needed
        public float dryWetMix = .5f; // Value from 0 (fully dry) to 1 (fully wet)
        public Equalizer eightBands;
        public bool eqActive = true;
        public EqualizerBand[] bands = new EqualizerBand[8];
        public NAudio.Wave.WaveFormat WaveFormat { get; set; } = GlobalConfig.GlobalWaveFormat;

        private BiQuadFilter lowPassFilter;




        public KarplusStrong(int sampleRate, float thefrequency)
        {
            NAudio.Wave.WaveFormat format = GlobalConfig.GlobalWaveFormat;
            frequency = thefrequency;
            int delayLineLength = (int)Math.Round(format.SampleRate / frequency);

            delayLine = new List<float>(new float[delayLineLength]);
            excitationSample = new List<float>(new float[delayLineLength]);

            var random = new Random();
            for (int i = 0; i < delayLineLength; i++)
            {
                excitationSample[i] = (float)(random.NextDouble() * 2.0 - 1.0);
            }

            lowPassFilter = BiQuadFilter.LowPassFilter(WaveFormat.SampleRate, lowPassCutOffValue, lowPassQValue);
            for (int i = 0; i < 8; i++)
            {
                bands[i] = new EqualizerBand();
                bands[i].Frequency = 100 * (i+1);
                bands[i].Bandwidth = 1;
                bands[i].Gain = -3;
            }
            Debug.WriteLine("KarplusStrong initialized");
            eightBands = new Equalizer(this, bands);//initialize  empty Bands to be set by UI later
            attackPhaseSamples = 480;
        }


        public void SetDryWetMix(float mix)
        {
            dryWetMix = mix / 100;

        }
        public void SetlowPassCutOffValue(float numericUpDownValue)
        {

            lowPassFilter.SetLowPassFilter(WaveFormat.SampleRate, numericUpDownValue, lowPassQValue);


        }
        public void SetlowPassQValue(float numericUpDownValue)
        {


            lowPassFilter.SetLowPassFilter(WaveFormat.SampleRate, lowPassCutOffValue, numericUpDownValue);
            //Debug.WriteLine("Q value: " + numericUpDownValue);

        }
        public void Stop()
        {
            // Quickly decay the remaining samples in the delay line to zero to stop the sound
            for (int i = 0; i < delayLine.Count; i++)
            {
                delayLine[i] *= 0.1f; // Apply a strong damping factor to each sample
            }



        }


        public float NextSample()
        {


            //to dampen the sound
            
           // damping = 0.989f;//between 0.980 and 0.999 (from mute to too much sustain)
            float output = delayLine[pos];

            // Apply the filters directly
            if (eqActive)
                output = eightBands.Process(new List<float> { output })[0];

            if (lowPassActive)
                output = lowPassFilter.Transform(output);

            delayLine[pos] = _damping * 0.5f * (delayLine[pos] + delayLine[(pos + 1) % delayLine.Count]);


            pos = (pos + 1) % delayLine.Count;



            return output;
        }


        public void Pluck(float amplitude)
        {

            //pure sine
            for (int i = 0; i < delayLine.Count; i++)
            {
                delayLine[i] = (amplitude / 2) * (float)(Math.Sin(2 * Math.PI * i / delayLine.Count));

            }


            // Transition to random numbers
            int transitionPoint = (int)(delayLine.Count * .9f);//from .1 "very" metallic to 1 Harp like 
            Random random = new Random();
            for (int i = transitionPoint; i < delayLine.Count; i++)
            {
                delayLine[i] = (float)random.NextDouble() * .1f - 1f;
            }



        }

        public void UpdateFrequency(float newFrequency)
        {
            NAudio.Wave.WaveFormat format = GlobalConfig.GlobalWaveFormat;

            frequency = newFrequency;
            int newDelayLineLength = (int)Math.Round(format.SampleRate / frequency);

            // Resize the delayLine and excitationSample
            delayLine.Resize(newDelayLineLength);
            excitationSample.Resize(newDelayLineLength);

            // Reset the pos variable
            pos = 0;

            // Update the excitationSample with new random values
            var random = new Random();
            for (int i = 0; i < newDelayLineLength; i++)
            {
                excitationSample[i] = (float)(random.NextDouble() * 2.0 - 1.0);
            }
        }
        public List<float> TransitionToFrequency(float newFrequency, int transitionSamples)
        {
            List<float> transitionedSamples = new List<float>();
            int originalDelayLineLength = delayLine.Count;
            int newDelayLineLength = (int)Math.Round(WaveFormat.SampleRate / newFrequency);
            float delayLineChangePerSample = (newDelayLineLength - originalDelayLineLength) / (float)transitionSamples;

            for (int i = 0; i < transitionSamples; i++)
            {
                // Calculate the new length for this sample
                int currentDelayLineLength = originalDelayLineLength + (int)(i * delayLineChangePerSample);
                delayLine.Resize(currentDelayLineLength);

                // Generate the current sample with the current delay line length
                float currentSample = NextSample();

                // Add the current sample to the transitioned samples list
                transitionedSamples.Add(currentSample);
            }

            // Once the transition is complete, set the delay line to the final length
            delayLine.Resize(newDelayLineLength);
            frequency = newFrequency;

            return transitionedSamples;
        }
        public void UpdateFrequencySmoothly(float newFrequency)
        {
            // Calculate the number of samples over which to spread the frequency transition
            int transitionLength = WaveFormat.SampleRate / 10; // for example, over a tenth of a second

            // Calculate the difference in delay line length
            int oldDelayLineLength = delayLine.Count;
            int newDelayLineLength = (int)Math.Round(WaveFormat.SampleRate / newFrequency);
            int delayLineDifference = newDelayLineLength - oldDelayLineLength;

            // Calculate the amount to adjust the delay line length each sample
            float delayLineAdjustmentPerSample = (float)delayLineDifference / transitionLength;

            // Adjust the delay line length gradually over the transitionLength samples
            for (int i = 0; i < transitionLength; i++)
            {
                // Adjust the delay line length
                int adjustedDelayLineLength = oldDelayLineLength + (int)(i * delayLineAdjustmentPerSample);
                delayLine.Resize(adjustedDelayLineLength);

                // Generate the next sample as normal, which will incorporate the new delay line length
                NextSample();
            }

            // After the transition, set the frequency and delay line length to their final values
            frequency = newFrequency;
            delayLine.Resize(newDelayLineLength);
        }


        private double GetDecayFactor(float frequency)
        {
            return 0.998 - (0.001 * (frequency / 1000.0f));
        }


        public int Read(float[] buffer, int offset, int count)
        {
            for (int n = 0; n < count; n += 2)
            {
                float sample = NextSample();


                buffer[n + offset] = sample; // Left channel
                buffer[n + offset + 1] = sample; // Right channel
            }
            return count;
        }
    }




    public static class ListExtensions
    {
        public static void Resize<T>(this List<T> list, int newSize)
        {
            int currentSize = list.Count;
            if (newSize < currentSize)
                list.RemoveRange(newSize, currentSize - newSize);
            else if (newSize > currentSize)
                list.AddRange(Enumerable.Repeat(default(T), newSize - currentSize));
        }
    }


}
