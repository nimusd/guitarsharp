using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{
    [Serializable]
    internal class StringSynthesiser
    {


        private readonly double decay = 0.998;
        private double amplitude = 0.0;
        private List<float> excitationSample, delayLine;
        private int pos = 0;

        public StringSynthesiser(double sampleRate, double frequencyInHz)
        {
            PrepareSynthesiserState(sampleRate, frequencyInHz);
        }

        private void PrepareSynthesiserState(double sampleRate, double frequencyInHz)
        {
            int delayLineLength = (int)Math.Round(sampleRate / frequencyInHz);

            if (delayLineLength <= 50)
                throw new InvalidOperationException("Increase sample rate or decrease frequency!");

            delayLine = new List<float>(new float[delayLineLength]);
            excitationSample = new List<float>(new float[delayLineLength]);

            var random = new Random();
            for (int i = 0; i < delayLineLength; i++)
            {
                excitationSample[i] = (float)(random.NextDouble() * 2.0 - 1.0);
            }
        }

        public void StringPlucked(float pluckPosition)
        {
            if (pluckPosition < 0.0 || pluckPosition > 1.0)
                throw new ArgumentOutOfRangeException(nameof(pluckPosition));

            amplitude = Math.Sin(Math.PI * pluckPosition);

            for (int i = 0; i < excitationSample.Count; i++)
            {
                delayLine[i] = (float)(amplitude * excitationSample[i]);
            }
        }

        public float[] GenerateData(int numSamples)
        {
            float[] output = new float[numSamples];
            for (int i = 0; i < numSamples; i++)
            {
                int nextPos = (pos + 1) % delayLine.Count;
                delayLine[nextPos] = (float)(decay * 0.5 * (delayLine[nextPos] + delayLine[pos]));
                output[i] = delayLine[pos];
                pos = nextPos;
            }
            return output;
        }
    }

}

