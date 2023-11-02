using NAudio.Midi;
using System;
[Serializable]
public class MidiHandler
{
    private string midiFilePath;
    private double tempo;

    // Define the NoteOnReceived event
    public event Action<int> NoteOnReceived;
    public List<MidiEventWithTimestamp> SortedMidiEvents { get; private set; } = new List<MidiEventWithTimestamp>();
    public MidiHandler(string midiFilePath, double tempo)
    {
        this.midiFilePath = midiFilePath;
        this.tempo = tempo;
    }
    public double MidiNoteToFrequency(int midiNote)
    {
        return 440.0 * Math.Pow(2.0, (midiNote - 69) / 12.0);
    }
    public void ReadMidiFile()
    {
        var midiFile = new MidiFile(midiFilePath, false);

        foreach (var trackEvents in midiFile.Events)
        {
            long currentTime = 0;
            foreach (var midiEvent in trackEvents)
            {
                currentTime += midiEvent.DeltaTime;
                SortedMidiEvents.Add(new MidiEventWithTimestamp(midiEvent, currentTime));
            }
        }

        SortedMidiEvents.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp));
        Console.WriteLine($"Total MIDI events: {SortedMidiEvents.Count}"); // Add this line
    }

    public class MidiEventWithTimestamp
    {
        public MidiEvent MidiEvent { get; }
        public long Timestamp { get; }

        public MidiEventWithTimestamp(MidiEvent midiEvent, long timestamp)
        {
            MidiEvent = midiEvent;
            Timestamp = timestamp/10;
        }
    }


private void MidiPlaybackTimer_Tick(object sender, EventArgs e)
    {
        // Check the next MIDI event in the list.
        // If its timestamp matches the current playback position, trigger the event and remove it from the list.
        // Continue this until you've processed all events that match the current playback position.
    }

    

}
