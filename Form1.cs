namespace Guitarsharp
{
    using NAudio.Midi;
    using NAudio.Wave;

    public partial class Form1 : Form
    {
        public KarplusStrong karplusStrong;
        private MidiHandler midiHandler;
        private WaveOut waveOut;
        private System.Windows.Forms.Timer midiPlaybackTimer = new System.Windows.Forms.Timer();
        private long currentTime = 0;
        private int currentEventIndex = 0;
        public KarplusStrongSampleProvider sampleProvider;

        public Form1()
        {
            InitializeComponent();

            

            WaveFormat format = GlobalConfig.GlobalWaveFormat;
            string midiFilePath = "test.mid"; // Ensure this path is correct
            double defaultTempo = 60.0; // Example default tempo
            midiHandler = new MidiHandler(midiFilePath, defaultTempo);



            karplusStrong = new KarplusStrong(44100, 440.0f); // This is just an initial frequency.

            sampleProvider = new KarplusStrongSampleProvider(karplusStrong, 220f);
            //MessageBox.Show(sampleProvider.WaveFormat.SampleRate.ToString());
            waveOut = new WaveOut();
            waveOut.Init(sampleProvider);
            waveOut.Play();

            midiHandler.NoteOnReceived += (noteNumber) =>
            {
                double frequency = midiHandler.MidiNoteToFrequency(noteNumber);
                karplusStrong.Pluck((float)frequency);
            };
            midiPlaybackTimer.Interval = 10; // Check every 10ms
            midiPlaybackTimer.Tick += MidiPlaybackTimer_Tick;
        }



        private void buttonPlayMidi_Click(object sender, EventArgs e)
        {
            currentTime = 0;
            currentEventIndex = 0;
            midiPlaybackTimer.Start();

            Console.WriteLine("Timer started!"); // Add this line
            midiHandler.ReadMidiFile();

        }



        private long currentPlaybackPosition = 0;

        private void MidiPlaybackTimer_Tick(object sender, EventArgs e)
        {

            if (midiHandler.SortedMidiEvents.Count == 0)
            {
                midiPlaybackTimer.Stop(); // Stop the timer if there are no more events.
                //MessageBox.Show(midiHandler.SortedMidiEvents.Count.ToString() + "here");
                return;
            }

            while (midiHandler.SortedMidiEvents.Count > 0 && midiHandler.SortedMidiEvents[0].Timestamp <= currentPlaybackPosition)
            {
                // MessageBox.Show(midiHandler.SortedMidiEvents.Count.ToString());
                var midiEventWithTimestamp = midiHandler.SortedMidiEvents[0];

                // Handle the MIDI event (e.g., play the note)
                if (midiEventWithTimestamp.MidiEvent.CommandCode == MidiCommandCode.NoteOn)
                {
                    var noteOnEvent = (NoteOnEvent)midiEventWithTimestamp.MidiEvent;
                    if (noteOnEvent.Velocity > 0)
                    {
                        double frequency = MidiUtilities.GetFrequencyFromMidiNote(noteOnEvent.NoteNumber);

                        // Update the frequency of the existing KarplusStrong object
                        karplusStrong.UpdateFrequency((float)frequency);

                        // Stop and dispose of the previous waveOut instance
                        waveOut.Stop();
                        waveOut.Dispose();

                        // Re-initialize waveOut with the same sampleProvider
                        waveOut = new WaveOut();
                        waveOut.Init(sampleProvider);
                        waveOut.Play();
                    }
                }


                // Remove the processed event from the list
                midiHandler.SortedMidiEvents.RemoveAt(0);
            }

            currentPlaybackPosition++; // Increment the playback position

        }

       
    }

    public static class MidiUtilities
    {
        public static double GetFrequencyFromMidiNote(int midiNoteNumber)
        {
            return 440.0 * Math.Pow(2.0, (midiNoteNumber - 69) / 12.0);
        }
    }

  

    public static class GlobalConfig
    {
        public static WaveFormat GlobalWaveFormat { get; } = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);

    }


}