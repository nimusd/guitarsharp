﻿using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{

    [Serializable]
    public class KarplusStrong : ISampleProvider
    {
        private readonly double decay = 0.998;
        public readonly List<float> delayLine;
        private readonly List<float> excitationSample;
        private int pos = 0;
        public float frequency;
        private int envelopePosition = 0;
        public int envelopeLength;
        private float previousSample = 0.0f;
        public float alpha = 0.1f;
        private Complex[] impulseResponseFrequency;
        public WaveFormat WaveFormat { get; } = GlobalConfig.GlobalWaveFormat;

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
        public void SetImpulseResponse(float[] impulseResponse)
        {
            Complex[] impulseResponseComplex = impulseResponse.Select(x => new Complex(x, 0)).ToArray();
            Fourier.Forward(impulseResponseComplex, FourierOptions.Matlab);
            this.impulseResponseFrequency = impulseResponseComplex;
        }
        public void Stop()
        {
            // Quickly decay the remaining samples in the delay line to zero to stop the sound
            for (int i = 0; i < delayLine.Count; i++)
            {
                delayLine[i] *= 0.1f; // Apply a strong damping factor to each sample
            }

            // Reset envelope position to prevent the sound from starting again
            envelopePosition = envelopeLength;
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
            for (int n = 0; n < count; n++)
            {
                buffer[n + offset] = NextSample();
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
