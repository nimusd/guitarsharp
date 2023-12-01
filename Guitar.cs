using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Guitarsharp
{
    public class Guitar
    {
       public KarplusStrong[] strings;
       public MixingSampleProvider mixer;

        public Guitar(int sampleRate)
        {
            // Define the frequencies for standard guitar tuning E2, A2, D3, G3, B3, E4
            double[] stringFrequencies = { 82.41, 110.00, 146.83, 196.00, 246.94, 329.63 };

            // Initialize the Karplus-Strong synthesizer for each string
            strings = new KarplusStrong[6];
            for (int i = 0; i < 6; i++)
            {
                strings[i] = new KarplusStrong(sampleRate, (float) stringFrequencies[i]);
                
            }

            // Initialize the mixer
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 2));
            mixer.ReadFully = true;
        }

        public void PluckString(int stringNumber, float pluckAmplitude)
        {
            if (stringNumber < 0 || stringNumber > 6)
                throw new ArgumentOutOfRangeException(nameof(stringNumber), "String number must be between 0 and 5.");

            // Convert string number to index
            int stringIndex = stringNumber ;

            // Pluck the string with the specified amplitude
            strings[stringIndex].Pluck(pluckAmplitude);

            // Since KarplusStrong implements ISampleProvider, we can directly add it to the mixer
            ISampleProvider stringAudio = strings[stringIndex];
            mixer.AddMixerInput(stringAudio);
        }


        public ISampleProvider GetMixerOutput()
        {
            return mixer;
        }

        public void Stop()
        {
            // Stop all strings and clear the mixer
            foreach (var stringSynth in strings)
            {
                stringSynth.Stop();
            }
            mixer.RemoveAllMixerInputs();
        }
    }

}
