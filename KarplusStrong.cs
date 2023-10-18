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
        public KarplusStrong(int sampleRate, float thefrequency)
        {
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1); // Mono output
            frequency = thefrequency;
            int delayLineLength = (int)Math.Round(44100 / frequency);

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

            delayLine[nextPos] = (float)(decay * 0.5 * (delayLine[nextPos] + delayLine[pos]));

            float output = delayLine[pos];
            pos = nextPos;

            return output;
        }

        public void Pluck(float amplitude)
        {
            for (int i = 0; i < excitationSample.Count; i++)
            {
                delayLine[i] = amplitude * excitationSample[i];
            }
        }

        public void UpdateFrequency(float newFrequency)
        {
            WaveFormat format = GlobalConfig.GlobalWaveFormat;

            frequency = newFrequency;
            int newDelayLineLength = (int)Math.Round(44100 / frequency);

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
