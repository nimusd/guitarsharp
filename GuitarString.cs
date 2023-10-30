using NAudio.Wave; 
using System;
using System.Collections.Generic;

namespace Guitarsharp
{
    [Serializable]
    public class GuitarString
    {
        public int StringNumber { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public int MidiChannelNumber { get; set; }

        // Karplus-Strong synthesizer for this string
        private KarplusStrong karplusStrongSynthesizer;

        // Parameterless default constructor
        public GuitarString()
        {
            // Initialize Karplus-Strong synthesizer with default frequency
            InitializeKarplusStrongSynthesizer(440.0f); // A4 frequency as default
        }

        // Constructor with string properties
        public GuitarString(int stringNumber, float defaultFrequency) : this()
        {
            StringNumber = stringNumber;
            InitializeKarplusStrongSynthesizer(defaultFrequency);
        }

        // Initialize the Karplus-Strong synthesizer
        private void InitializeKarplusStrongSynthesizer(float frequency)
        {
            // Initialize the synthesizer with the default frequency for this string
            karplusStrongSynthesizer = new KarplusStrong(GlobalConfig.GlobalWaveFormat.SampleRate, frequency);
        }
        // Method to generate audio for a note
        public ISampleProvider GenerateAudioForNote(Note note)
        {
            WaveFormat waveFormat = GlobalConfig.GlobalWaveFormat; // Your global wave format
            float frequency = (float)MidiUtilities.GetFrequencyFromMidiNote(note.MidiNoteNumber);

            // Scale the velocity to an amplitude range of -1.0 to 1.0
            float scaledAmplitude = note.Velocity / 127.0f; // Assuming velocity ranges from 0 to 127

            karplusStrongSynthesizer.UpdateFrequency(frequency);
            karplusStrongSynthesizer.Pluck(scaledAmplitude); // Use the scaled amplitude

            float noteDuration = (float)note.EndTime - (float)note.StartTime; // Calculate note duration
            return new SampleProviderWrapper(karplusStrongSynthesizer, noteDuration);
        }



    }
    public class SampleProviderWrapper : ISampleProvider
    {
        private readonly KarplusStrong karplusStrong;
        private readonly float noteDurationInSeconds;
        private float elapsedTime;

        public SampleProviderWrapper(KarplusStrong karplusStrong, float noteDurationInSeconds)
        {
            this.karplusStrong = karplusStrong;
            this.noteDurationInSeconds = noteDurationInSeconds;
            WaveFormat = GlobalConfig.GlobalWaveFormat;
            elapsedTime = 0;
        }

        public WaveFormat WaveFormat { get; private set; }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesGenerated = 0;
            for (int i = 0; i < count; i++)
            {
                if (elapsedTime < noteDurationInSeconds)
                {
                    buffer[offset + i] = karplusStrong.NextSample();
                    samplesGenerated++;
                }
                else
                {
                    buffer[offset + i] = 0; // Fill the rest with silence
                }
                elapsedTime += 1.0f / WaveFormat.SampleRate;
            }
            return samplesGenerated;
        }
    }


}
