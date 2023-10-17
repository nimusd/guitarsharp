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

        public KarplusStrong(int sampleRate, float frequency)
        {
            int delayLineLength = (int)Math.Round(sampleRate / frequency);

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
    }

}
