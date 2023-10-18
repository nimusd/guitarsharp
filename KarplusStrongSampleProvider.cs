using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System;

namespace Guitarsharp
{


    public class KarplusStrongSampleProvider : ISampleProvider
    {
        public KarplusStrong karplusStrong;
        public WaveFormat WaveFormat { get; } = GlobalConfig.GlobalWaveFormat;
        public KarplusStrongSampleProvider(KarplusStrong karplusStrongInstance, float frequency)
        {
            WaveFormat format = GlobalConfig.GlobalWaveFormat;
            this.karplusStrong = karplusStrongInstance;
            //karplusStrong = new KarplusStrong(44100, frequency); // Assuming a sample rate of 44100
            
            karplusStrong.Pluck(0.5f); // Pluck the string with an amplitude of 0.5
            

        }

        public void UpdateFrequency(float frequency)
        {
            // Update the frequency of the KarplusStrong instance
            this.karplusStrong.UpdateFrequency(frequency);
        }
      

        public int Read(float[] buffer, int offset, int count)
        {
            for (int n = 0; n < count; n++)
            {
                buffer[n + offset] = karplusStrong.NextSample();
            }
            return count;
        }
    }

}
