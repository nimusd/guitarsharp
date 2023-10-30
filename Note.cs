using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Guitarsharp
{
    [Serializable]
    public class Note
    {
        
        public int StringNumber { get; set; }
        public int FretNumber { get; set; }
        public float Frequency;
        public int MidiNoteNumber { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public int Velocity { get; set; }
        public Rectangle DrawingRectangle { get; set; }

        public bool IsSelected { get; set; } = false;
        public int MidiChannel;


        // Define the open string frequencies and MIDI note numbers for a standard-tuned six-string guitar
        private static readonly float[] OpenStringFrequencies = { 82.41f, 110.00f, 146.83f, 196.00f, 246.94f, 329.63f };
        private static readonly int[] OpenStringMidiNumbers = { 40, 45, 50, 55, 59, 64 };

        //Ensure they have a parameterless default constructor, which is required for deserialization.  
        public Note() { } // Default constructor
        public Note(int stringNumber, int fretNumber, float startTime, float endTime, int velocity, float frequency, int midiNoteNumber, int midiChannel)
        {
            StringNumber = stringNumber;
            FretNumber = fretNumber;
            StartTime = startTime;
            EndTime = endTime;
            Velocity = velocity;
            Frequency = frequency;
            MidiChannel = midiChannel;

            // Calculate the frequency 
           // frequency =  (float) MidiUtilities.GetFrequencyFromMidiNote( midiNoteNumber);


            MidiNoteNumber = midiNoteNumber;
            

        }
        private float CalculateFrequency(int fretNumber, int stringNumber)
        {
            // Each fret represents a semitone, so the frequency increases by the twelfth root of 2 for each fret
            return OpenStringFrequencies[stringNumber ] * (float)Math.Pow(2, fretNumber / 12.0);
        }

        private int CalculateMidiNoteNumber(int fretNumber, int stringNumber)
        {
            // Each fret represents a semitone, so the MIDI note number increases by 1 for each fret
            return OpenStringMidiNumbers[stringNumber ] + fretNumber;
        }

    }
}
