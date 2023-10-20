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
        public int fretboardMidiNoteNumber = 0;

        public Form1()
        {
            InitializeComponent();



            WaveFormat format = GlobalConfig.GlobalWaveFormat;
            string midiFilePath = "test.mid"; // Ensure this path is correct
            double defaultTempo = 60.0; // Example default tempo
            midiHandler = new MidiHandler(midiFilePath, defaultTempo);


            // Attach the single event handler to all relevant buttons
            foreach (Control control in fretboard.Controls)
            {
                if (control is Button && control.Tag != null)
                {
                    
                    control.Click += FretboardButton_Click;
                }
            }

            karplusStrong = new KarplusStrong(format.SampleRate, 220.0f); // This is just an initial frequency.
            karplusStrong.UpdateFrequency(880);
            sampleProvider = new KarplusStrongSampleProvider(karplusStrong, 440f);
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


        private void FretboardButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Tag.ToString(), out int midiNoteNumber))
            {
                PlayFromFretboard(midiNoteNumber);
            }
        }
        private void PlayFromFretboard(int midiNoteNumber)
        {
            double frequency = MidiUtilities.GetFrequencyFromMidiNote(midiNoteNumber);

            // Update the frequency of the existing KarplusStrong object
            karplusStrong.UpdateFrequency((float)frequency);

            // Pluck the virtual string to generate sound
            karplusStrong.Pluck(0.5f); // You can adjust the amplitude value as needed

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
                MessageBox.Show(midiHandler.SortedMidiEvents.Count.ToString() + "here");
                return;
            }

            while (midiHandler.SortedMidiEvents.Count > 0 && midiHandler.SortedMidiEvents[0].Timestamp <= currentPlaybackPosition)
            {

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

                        // Pluck the virtual string to generate sound
                        karplusStrong.Pluck(0.5f); // You can adjust the amplitude value as needed


                    }
                }



                // Remove the processed event from the list
                midiHandler.SortedMidiEvents.RemoveAt(0);
            }

            currentPlaybackPosition++; // Increment the playback position

        }

        private void lowPassFilterAlpha_ValueChanged(object sender, EventArgs e)
        {
            karplusStrong.alpha = (float)lowPassFilterAlpha.Value / 100;
        }

        private void EnvelopeLengthSlider_ValueChanged(object sender, EventArgs e)
        {
            double minLog = Math.Log10(441);       // Logarithm of the minimum value
            double maxLog = Math.Log10(220500);    // Logarithm of the maximum value
            double scaledValue = minLog + (maxLog - minLog) * EnvelopeLengthSlider.Value / 100;  // sliderValue is between 0 and 1
            karplusStrong.envelopeLength = (int)Math.Pow(10, scaledValue);

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