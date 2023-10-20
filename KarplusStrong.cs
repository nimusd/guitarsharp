using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{


    public class KarplusStrong
    {
        private readonly double decay = 0.998;
        private readonly List<float> delayLine;
        private readonly List<float> excitationSample;
        private int pos = 0;
        public float frequency;
        private int envelopePosition = 0;
        public int envelopeLength;

        public KarplusStrong(int sampleRate, float thefrequency)
        {
            WaveFormat format = GlobalConfig.GlobalWaveFormat;
            frequency = thefrequency;
            int delayLineLength = (int)Math.Round(format.SampleRate / frequency);

            delayLine = new List<float>(new float[delayLineLength]);
            excitationSample = new List<float>(new float[delayLineLength]);

            var random = new Random();
            for (int i = 0; i < delayLineLength; i++)
            {
                excitationSample[i] = (float)(random.NextDouble() * 2.0 - 1.0);
            }
        }

        public float NextSample()
        {
            int nextPos = (pos + 1) % delayLine.Count;
            double currentDecay = GetDecayFactor(frequency);
            delayLine[nextPos] = (float)(currentDecay * 0.5 * (delayLine[nextPos] + delayLine[pos]));
            //delayLine[nextPos] = (float)(decay * 0.5 * (delayLine[nextPos] + delayLine[pos]));

            //float output = delayLine[pos];
            float output = lowPassFilter(delayLine[pos]);

            // Apply the envelope
            if (envelopePosition < envelopeLength)
            {
                output *= (float)Math.Sin(Math.PI * envelopePosition / envelopeLength);
                envelopePosition++;
            }

            pos = nextPos;

            return output;
        }
        private float previousSample = 0.0f;
        public float alpha = 0.1f;
        private float lowPassFilter(float sample)
        {
            //float alpha = 0.1f; // You can adjust this value for more or less filtering
            float filteredSample = alpha * sample + (1.0f - alpha) * previousSample;
            previousSample = filteredSample;
            return filteredSample;
        }
        public void Pluck(float amplitude)
        {
            for (int i = 0; i < excitationSample.Count; i++)
            {
                delayLine[i] = amplitude * excitationSample[i];
            }
            envelopePosition = 0; // Reset the envelope position
        }

        public void UpdateFrequency(float newFrequency)
        {
            WaveFormat format = GlobalConfig.GlobalWaveFormat;

            frequency = newFrequency;
            int newDelayLineLength = (int)Math.Round(format.SampleRate / frequency);
            envelopeLength = (int)(format.SampleRate / frequency); // Example calculation
            envelopePosition = 0; // Reset the envelope position
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

        private double GetDecayFactor(float frequency)
        {
            return 0.998 - (0.001 * (frequency / 1000.0f));
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
