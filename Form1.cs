



namespace Guitarsharp
{
    using NAudio.Midi;
    using NAudio.Wave;
    using System.Reflection.Metadata;
    using System.Security.Cryptography;
    using System;
    using System.Text.Json;
    using System.Windows.Forms;
    using static System.Runtime.InteropServices.JavaScript.JSType;
    using NAudio.Mixer;
    using NAudio.Wave.SampleProviders;
    using static System.Windows.Forms.DataFormats;
    using Microsoft.VisualBasic;
    using System.Linq;
    using System.Diagnostics;

    [Serializable]
    public partial class Form1 : Form
    {
        public KarplusStrong karplusStrong;
        private MidiHandler midiHandler;
        private WaveOut waveOut;
        private System.Windows.Forms.Timer midiPlaybackTimer = new System.Windows.Forms.Timer();
        private float currentTime = 0;
        private int currentEventIndex = 0;
        public KarplusStrongSampleProvider sampleProvider;
        public int fretboardMidiNoteNumber = 0;
        private List<Note> allNotes = new List<Note>();
        private Composition composition;
        private float PixelsPerSecond = 150.0f;  // zoom factor: adjust this value as needed
        private bool noNoteAddedSinceLastSpacePress;
        Note selectedNote = null;
        Note noteToDelete = null;
        Point initialMousePosition;
        public int version = 1;
        public float activeNoteDuration = 1f;
        bool isDraggingLeft = false;
        bool isDraggingRight = false;
        private bool isDraggingUp = false;
        private bool isDraggingDown = false;
        private float baseNoteDuration;
        private int timeSignatureNumerator = 5;
        private int timeSignatureDenominator = 4;
        private int tempo = 100;//default
        private int[] midiChannelPerString = { 1, 2, 3, 4, 5, 6 };
        private long currentPlaybackPosition = 0;
        private List<Note> notes = new List<Note>();
        private bool isDotted = false;
        private bool isTriplet = false;
        private Timer debounceTimer;
        private bool isSpacebarPressed = false;
        private int initialNoteHeight = 0;
        private int initialNoteWidth = 0;
        private int numberOfBars = 10;
        private IWavePlayer waveOutDevice;
        private MixingSampleProvider mixer;

        private System.Windows.Forms.Timer playbackTimer = new System.Windows.Forms.Timer();
        private int currentNoteIndex = 0;
        private float currentPlaybackTime = 0;

        private FretPattern activeFretPattern;
        public List<FretPattern> allFretPatterns = new List<FretPattern>();
        public bool fringerPatternMode = false;


        public Form1()
        {
            InitializeComponent();



            WaveFormat format = GlobalConfig.GlobalWaveFormat;
            string midiFilePath = "test.mid"; // Ensure this path is correct
            double defaultTempo = 60.0; // Example default tempo
            midiHandler = new MidiHandler(midiFilePath, defaultTempo);

            InitializeAudio();
            this.KeyPreview = true;



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

            playbackTimer.Interval = 10; // Check every 10ms, adjust as needed
            playbackTimer.Tick += PlaybackTimer_Tick;
            InitializeComposition();

            noNoteAddedSinceLastSpacePress = true;

            baseNoteDuration = CalculateBeatDuration();
            guitarRollPanel.Invalidate();

            debounceTimer = new Timer { Interval = 500 };
            debounceTimer.Tick += (s, e) =>
            {
                debounceTimer.Stop();
                isSpacebarPressed = false;
            };
            this.KeyPreview = true;

            // Set the padding for the guitarRollPanel to accommodate the scrollbar
            int scrollbarHeight = SystemInformation.HorizontalScrollBarHeight;

            guitarRollPanel.Padding = new Padding(0, 0, 0, scrollbarHeight + 20);

            // Set a default length for the guitar roll panel
            int defaultLength = ((int)CalculateBarDuration() * numberOfBars) * (int)PixelsPerSecond; // Example length in pixels

            guitarRollPanel.AutoScrollMinSize = new Size(defaultLength, guitarRollPanel.Height);
            blancheButton.BackColor = Color.LightGreen;
            rondeButton.BackColor = Color.LightGreen;
            noireButton.BackColor = Color.LightBlue;
            crocheButton.BackColor = Color.LightGreen;
            doubleCrocheButton.BackColor = Color.LightGreen;
            tripleCrocheButton.BackColor = Color.LightGreen;
            CreateFretPatterns();



            Debug.WriteLine("Init completed");



        }



        private void InitializeComposition()
        {
            composition = new Composition
            {
                Title = "May and June",
                Tempo = this.tempo,
                TimeSignatureNumerator = this.timeSignatureNumerator,
                TimeSignatureDenominator = this.timeSignatureDenominator,
                Strings = new List<GuitarString>()
            };

            // Initialize GuitarString objects for a 6-string guitar
            for (int i = 1; i <= 6; i++)
            {
                composition.Strings.Add(new GuitarString { StringNumber = i });
            }

            // ... You can also initialize Notes here if needed ...
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                currentTime += activeNoteDuration;
                guitarRollPanel.Invalidate();
            }
            else if (e.KeyCode == Keys.Space && !isSpacebarPressed)
            {
                isSpacebarPressed = true;
                debounceTimer.Start();
                e.SuppressKeyPress = true; // Suppress the key event
                                           // Your code to add a note goes here
            }
            else if (e.KeyCode == Keys.Delete && noteToDelete != null)
            {

                composition.Notes.Remove(noteToDelete);
                noteToDelete = null;
                guitarRollPanel.Invalidate();
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

            Debug.WriteLine("Timer started!"); // Add this line
            midiHandler.ReadMidiFile();

        }





        private void MidiPlaybackTimer_Tick(object sender, EventArgs e)
        {

            if (midiHandler.SortedMidiEvents.Count == 0)
            {
                midiPlaybackTimer.Stop(); // Stop the timer if there are no more events.

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



        private Form1Data GetForm1Data()
        {
            return new Form1Data
            {
                AllNotes = this.allNotes,
                Composition = this.composition,
                FretboardMidiNoteNumber = this.fretboardMidiNoteNumber,
                PixelsPerSecond = PixelsPerSecond,
                Version = this.version,
                MidiChannelPerString = this.midiChannelPerString,

                timeSignatureNumerator = this.timeSignatureNumerator,
                timeSignatureDenominator = this.timeSignatureNumerator,
                tempo = this.tempo



            };
        }

        /*
         * 
         * 
        public List<Note> AllNotes { get; set; }
        public Composition Composition { get; set; }
        public int FretboardMidiNoteNumber { get; set; }
        public float PixelsPerSecond { get; set; } = 50.0f;
        public int Version { get; set; }
        public int[] MidiChannelPerString { get; set; }

        public int timeSignatureNumerator { get; set; }
        public int timeSignatureDenominator { get; set; }
        public int tempo { get; set; }
         * */
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "gur";
            openFileDialog1.Filter = "Guitarsharp Files (*.gur)|*.gur";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                string jsonString = File.ReadAllText(openFileDialog1.FileName);
                Form1Data data = JsonSerializer.Deserialize<Form1Data>(jsonString);

                this.allNotes = data.AllNotes;
                this.composition = data.Composition;
                this.fretboardMidiNoteNumber = data.FretboardMidiNoteNumber;
                this.PixelsPerSecond = data.PixelsPerSecond;
                this.midiChannelPerString = data.MidiChannelPerString;
                this.timeSignatureNumerator = data.timeSignatureNumerator;
                this.timeSignatureDenominator = data.timeSignatureNumerator;
                this.tempo = data.tempo;

                //check the version and handle older versions accordingly(e.g., provide default values for new properties or convert data from old formats).
                if (this.version != data.Version) MessageBox.Show("wrong version");// we can do better :) but for now that's it...

                guitarRollPanel.Invalidate(); // Refresh the panel to reflect the loaded data
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "gur";
            saveFileDialog1.Filter = "Guitarsharp Files (*.gur)|*.gur";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Form1Data data = GetForm1Data();
                string jsonString = JsonSerializer.Serialize(data);
                File.WriteAllText(saveFileDialog1.FileName, jsonString);
            }
        }

        public enum NoteType
        {
            Ronde,      // Whole Note
            Blanche,    // Half Note
            Noire,      // Quarter Note
            Croche,     // Eighth Note
            DoubleCroche, // Sixteenth Note
            TripleCroche  // 32nd Note
        }

        private float GetNoteMultiplier(NoteType noteType)
        {
            float multiplier = 1.0f;

            switch (noteType)
            {
                case NoteType.Ronde: // Whole Note

                    if (timeSignatureDenominator == 4) multiplier = 4.0f;
                    if (timeSignatureDenominator == 8) multiplier = 8.0f;
                    if (timeSignatureDenominator == 2) multiplier = 2.0f;
                    break;

                case NoteType.Blanche: // Half Note
                    if (timeSignatureDenominator == 4) multiplier = 2.0f;
                    if (timeSignatureDenominator == 8) multiplier = 4.0f;
                    if (timeSignatureDenominator == 2) multiplier = 1.0f;
                    break;

                case NoteType.Noire: // Quarter Note
                    if (timeSignatureDenominator == 4) multiplier = 1.0f;
                    if (timeSignatureDenominator == 8) multiplier = 2.0f;
                    if (timeSignatureDenominator == 2) multiplier = 0.5f;
                    break;

                case NoteType.Croche: // Eighth Note
                    if (timeSignatureDenominator == 4) multiplier = 0.5f;
                    if (timeSignatureDenominator == 8) multiplier = 1.0f;
                    if (timeSignatureDenominator == 2) multiplier = 0.25f;
                    break;

                case NoteType.DoubleCroche: // Sixteenth Note
                    if (timeSignatureDenominator == 4) multiplier = 0.25f;
                    if (timeSignatureDenominator == 8) multiplier = 0.5f;
                    if (timeSignatureDenominator == 2) multiplier = 0.125f;
                    break;

                case NoteType.TripleCroche: // 32nd Note
                    if (timeSignatureDenominator == 4) multiplier = 0.125f;
                    if (timeSignatureDenominator == 8) multiplier = 0.25f;
                    if (timeSignatureDenominator == 2) multiplier = 0.0625f;
                    break;

                default:
                    throw new Exception("Unsupported note type.");
            }

            if (isDotted)
            {

                multiplier *= 1.5f;
            }
            if (isTriplet)
            {
                multiplier *= (2.0f / 3.0f);
            }
            return multiplier;

        }


        private void tripletButton_Click(object sender, EventArgs e)
        {


            if (isTriplet == false)
            {
                tripletButton.BackColor = Color.LightBlue; // Example active color

                if (rondeButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Ronde) * (2.0f / 3.0f);
                else if (blancheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Blanche) * (2.0f / 3.0f);
                else if (noireButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Noire) * (2.0f / 3.0f);
                else if (crocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Croche) * (2.0f / 3.0f);
                else if (doubleCrocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.DoubleCroche) * (2.0f / 3.0f);
                else if (tripleCrocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.TripleCroche) * (2.0f / 3.0f);

                isTriplet = true;
            }
            else if (isTriplet == true)
            {
                tripletButton.BackColor = SystemColors.Control; // Default color

                isTriplet = false;

                if (rondeButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Ronde);
                else if (blancheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Blanche);
                else if (noireButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Noire);
                else if (crocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Croche);
                else if (doubleCrocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.DoubleCroche);
                else if (tripleCrocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.TripleCroche);


            }
        }
        private void pointeeButton_Click(object sender, EventArgs e)
        {


            if (isDotted == false)
            {
                pointeeButton.BackColor = Color.LightBlue; // Example active color

                if (rondeButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Ronde) * 1.5f;
                else if (blancheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Blanche) * 1.5f;
                else if (noireButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Noire) * 1.5f;
                else if (crocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Croche) * 1.5f;
                else if (doubleCrocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.DoubleCroche) * 1.5f;
                else if (tripleCrocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.TripleCroche) * 1.5f;

                isDotted = true;
            }
            else if (isDotted == true)
            {
                pointeeButton.BackColor = SystemColors.Control; // Default color

                isDotted = false;

                if (rondeButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Ronde);
                else if (blancheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Blanche);
                else if (noireButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Noire);
                else if (crocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.Croche);
                else if (doubleCrocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.DoubleCroche);
                else if (tripleCrocheButton.BackColor == Color.LightBlue) activeNoteDuration = GetNoteMultiplier(NoteType.TripleCroche);



            }
        }
        private void rondeButton_Click(object sender, EventArgs e)
        {
            activeNoteDuration = baseNoteDuration * GetNoteMultiplier(NoteType.Ronde);
            blancheButton.BackColor = Color.LightGreen;
            rondeButton.BackColor = Color.LightBlue;
            noireButton.BackColor = Color.LightGreen;
            crocheButton.BackColor = Color.LightGreen;
            doubleCrocheButton.BackColor = Color.LightGreen;
            tripleCrocheButton.BackColor = Color.LightGreen;

        }

        private void blancheButton_Click(object sender, EventArgs e)
        {
            activeNoteDuration = baseNoteDuration * GetNoteMultiplier(NoteType.Blanche);
            blancheButton.BackColor = Color.LightBlue;
            rondeButton.BackColor = Color.LightGreen;
            noireButton.BackColor = Color.LightGreen;
            crocheButton.BackColor = Color.LightGreen;
            doubleCrocheButton.BackColor = Color.LightGreen;
            tripleCrocheButton.BackColor = Color.LightGreen;
        }

        private void noireButton_Click(object sender, EventArgs e)
        {
            activeNoteDuration = baseNoteDuration * GetNoteMultiplier(NoteType.Noire);
            blancheButton.BackColor = Color.LightGreen;
            rondeButton.BackColor = Color.LightGreen;
            noireButton.BackColor = Color.LightBlue;
            crocheButton.BackColor = Color.LightGreen;
            doubleCrocheButton.BackColor = Color.LightGreen;
            tripleCrocheButton.BackColor = Color.LightGreen;
        }

        private void crocheButton_Click(object sender, EventArgs e)
        {
            activeNoteDuration = baseNoteDuration * GetNoteMultiplier(NoteType.Croche);
            blancheButton.BackColor = Color.LightGreen;
            rondeButton.BackColor = Color.LightGreen;
            noireButton.BackColor = Color.LightGreen;
            crocheButton.BackColor = Color.LightBlue;
            doubleCrocheButton.BackColor = Color.LightGreen;
            tripleCrocheButton.BackColor = Color.LightGreen;
        }

        private void doubleCrocheButton_Click(object sender, EventArgs e)
        {
            activeNoteDuration = baseNoteDuration * GetNoteMultiplier(NoteType.DoubleCroche);
            blancheButton.BackColor = Color.LightGreen;
            rondeButton.BackColor = Color.LightGreen;
            noireButton.BackColor = Color.LightGreen;
            crocheButton.BackColor = Color.LightGreen;
            doubleCrocheButton.BackColor = Color.LightBlue;
            tripleCrocheButton.BackColor = Color.LightGreen;
        }

        private void tripleCrocheButton_Click(object sender, EventArgs e)
        {
            activeNoteDuration = baseNoteDuration * GetNoteMultiplier(NoteType.TripleCroche);
            blancheButton.BackColor = Color.LightGreen;
            rondeButton.BackColor = Color.LightGreen;
            noireButton.BackColor = Color.LightGreen;
            crocheButton.BackColor = Color.LightGreen;
            doubleCrocheButton.BackColor = Color.LightGreen;
            tripleCrocheButton.BackColor = Color.LightBlue;
        }







        private void stringOneMidiChannelUpDown_ValueChanged(object sender, EventArgs e)
        {
            midiChannelPerString[0] = (int)stringOneMidiChannelUpDown.Value;
        }

        private void stringTwoMidiChannelUpDown_ValueChanged(object sender, EventArgs e)
        {
            midiChannelPerString[1] = (int)stringTwoMidiChannelUpDown.Value;
        }

        private void stringThreeMidiChannelUpDown_ValueChanged(object sender, EventArgs e)
        {
            midiChannelPerString[2] = (int)stringThreeMidiChannelUpDown.Value;
        }

        private void stringFourMidiChannelUpDown_ValueChanged(object sender, EventArgs e)
        {
            midiChannelPerString[3] = (int)stringFourMidiChannelUpDown.Value;
        }

        private void stringFiveMidiChannelUpDown_ValueChanged(object sender, EventArgs e)
        {
            midiChannelPerString[4] = (int)stringFiveMidiChannelUpDown.Value;
        }

        private void stringSixMidiChannelUpDown_ValueChanged(object sender, EventArgs e)
        {
            midiChannelPerString[5] = (int)stringSixMidiChannelUpDown.Value;
        }

        private void timeSignatureNumeratorNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            baseNoteDuration = CalculateBeatDuration();
            timeSignatureNumerator = (int)timeSignatureNumeratorNumericUpDown.Value;
            guitarRollPanel.Invalidate();

        }

        private void timeSignatureDenominatorNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            baseNoteDuration = CalculateBeatDuration();
            timeSignatureDenominator = (int)timeSignatureDenominatorNumericUpDown.Value;
            guitarRollPanel.Invalidate();
        }

        private void tempoNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            //baseNoteDuration = CalculateBeatDuration();
        }





        // Initialize NAudio components
        private void InitializeAudio()
        {
            waveOutDevice = new WaveOutEvent();
            mixer = new MixingSampleProvider(GlobalConfig.GlobalWaveFormat);
            mixer.ReadFully = true;
            waveOutDevice.Init(mixer);
            waveOutDevice.Play();
        }

        // Start playing
        private void startPlayingButton_Click(object sender, EventArgs e)
        {

            currentNoteIndex = 0;
            currentPlaybackTime = 0;
            playbackTimer.Start();
        }


        private void PlaybackTimer_Tick(object sender, EventArgs e)
        {
            // Play notes that should start at the current playback time
            while (currentNoteIndex < composition.Notes.Count && composition.Notes[currentNoteIndex].StartTime <= currentPlaybackTime)
            {
                Note note = composition.Notes[currentNoteIndex];

                // Find the GuitarString that corresponds to the Note's StringNumber
                GuitarString guitarString = composition.Strings.FirstOrDefault(s => s.StringNumber == note.StringNumber);
                if (guitarString != null)
                {

                    ISampleProvider audio = guitarString.GenerateAudioForNote(note);
                    mixer.AddMixerInput(audio);
                }

                currentNoteIndex++;
            }

            // Logic to stop notes that have reached their end time
            // This part needs to be implemented based on how you handle audio playback
            // For example, you might need to remove the note's audio from the mixer or stop its playback

            currentPlaybackTime += playbackTimer.Interval / 1000.0f; // Increment time in seconds

            if (currentNoteIndex >= composition.Notes.Count)
            {
                playbackTimer.Stop(); // Stop the timer if all notes have been played
            }
        }



        // Stop playing

        private void StopPlayingButton_Click(object sender, EventArgs e)
        {
            playbackTimer.Stop();
            waveOutDevice.Stop();
            mixer.RemoveAllMixerInputs(); // Optional, to clear the mixer for the next play
        }

        // Dispose resources when done
        private void DisposeAudioResources()
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }
        }

        public void exportAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportAudioFileDialog.DefaultExt = "wav";
            exportAudioFileDialog.Filter = "wav Files (*.wav)|*.wav";
            // Calculate total duration in seconds
            double totalDurationInSeconds = (60.0 / tempo) * numberOfBars * timeSignatureNumerator;
            // Show the save file dialog
            if (exportAudioFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = exportAudioFileDialog.FileName;
                WaveFormat waveFormat = GlobalConfig.GlobalWaveFormat; // Your global wave format

                // Use the default composition object
                Composition composition = this.composition;

                // List to hold all ISampleProviders
                List<ISampleProvider> sampleProviders = new List<ISampleProvider>();





                // Iterate through each string and generate audio for each note
                foreach (GuitarString guitarString in composition.Strings)
                {
                    foreach (Note note in guitarString.Notes)
                    {
                        ISampleProvider noteSampleProvider = guitarString.GenerateAudioForNote(note);
                        if (noteSampleProvider != null)
                        {
                            // Add silence before the note according to its start time
                            ISampleProvider noteWithSilence = new SilencePrependedSampleProvider(noteSampleProvider, (float)note.StartTime, waveFormat);
                            sampleProviders.Add(noteWithSilence);
                        }
                    }
                }

                // Check if there are any sample providers
                if (sampleProviders.Count == 0)
                {
                    // Handle the case where there are no sample providers
                    MessageBox.Show("No sample providers generated.");
                    return;
                }

                // Create a MixingSampleProvider from the list of ISampleProviders
                MixingSampleProvider mixer = new MixingSampleProvider(sampleProviders);
                mixer.ReadFully = true; // Ensure all samples are read
                                        // Export to .wav format




                // Export to .wav format
                using (WaveFileWriter writer = new WaveFileWriter(filePath, waveFormat))
                {
                    float[] buffer = new float[waveFormat.SampleRate];
                    int samplesRead;
                    double elapsedTime = 0;

                    // Read samples from the mixer and write to the file
                    while ((samplesRead = mixer.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (elapsedTime >= totalDurationInSeconds)
                        {
                            break; // Stop writing if total duration is reached
                        }

                        for (int i = 0; i < samplesRead; i++)
                        {
                            // Normalize the sample
                            buffer[i] = NormalizeSample(buffer[i]);
                        }
                        writer.WriteSamples(buffer, 0, samplesRead);
                        elapsedTime += (double)samplesRead / waveFormat.SampleRate;
                    }
                }

                //MessageBox.Show("Audio export completed successfully.", "Audio Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ;
            }
        }

        private float NormalizeSample(float sample)
        {
            // Assuming the samples are in the range of -1.0 to 1.0
            // Adjust the normalization factor if needed
            float normalizationFactor = 0.9f; // Reduce the amplitude by 20%
            return sample * normalizationFactor;
        }

        private void exportMIDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportMidiSaveFileDialog.Filter = "MIDI files (*.mid)|*.mid";
            exportMidiSaveFileDialog.DefaultExt = "mid";
            exportMidiSaveFileDialog.Title = "Export to MIDI";

            if (exportMidiSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = exportMidiSaveFileDialog.FileName;

                // Assuming you have a Composition object with a list of Notes
                Composition composition = this.composition;

                // Group notes by string
                var notesByString = composition.Notes.GroupBy(note => note.StringNumber).ToList();

                // Adjust note duration for each string group
                foreach (var stringGroup in notesByString)
                {
                    Note previousNote = null;
                    foreach (var note in stringGroup.OrderBy(n => n.StartTime))
                    {
                        if (previousNote != null)
                        {
                            // Adjust the duration of the previous note to end when the current note begins
                            previousNote.EndTime = note.StartTime;
                        }
                        previousNote = note;
                    }
                }

                // Create a MIDI event collection with the format type and tracks count
                MidiEventCollection midiEvents = new MidiEventCollection(1, MidiUtilities.TicksPerQuarterNote);

                // Track 0 is reserved for tempo and time signature events
                IList<MidiEvent> trackEvents = midiEvents.AddTrack();

                // Add tempo event
                int tempo = MidiUtilities.BpmToMicrosecondsPerQuarterNote(composition.Tempo);
                trackEvents.Add(new TempoEvent(tempo, 0));

                // Add time signature event
                trackEvents.Add(new TimeSignatureEvent(0, composition.TimeSignatureNumerator, composition.TimeSignatureDenominator, 24, 8));

                // Add notes to the MIDI event collection
                foreach (Note note in composition.Notes)
                {
                    int startTime = (int)(note.StartTime * MidiUtilities.TicksPerQuarterNote);
                    int duration = (int)((note.EndTime - note.StartTime) * MidiUtilities.TicksPerQuarterNote);

                    NoteOnEvent noteOn = new NoteOnEvent(startTime, note.MidiChannel, note.MidiNoteNumber, note.Velocity, duration);
                    trackEvents.Add(noteOn);

                    NoteEvent noteOff = new NoteEvent(startTime + duration, note.MidiChannel, MidiCommandCode.NoteOff, note.MidiNoteNumber, 0);
                    trackEvents.Add(noteOff);
                }

                // Export the MIDI event collection to a file
                MidiFile.Export(filePath, midiEvents);

                MessageBox.Show("MIDI export completed successfully.", "MIDI Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void editFretPatternButton_Click(object sender, EventArgs e)
        {/*
            FretPattern testFretPattern = new FretPattern("Firstpattern", 5, this);
            testFretPattern.CreateFretboard(6, 4, testFretPattern.BaseFret, new Point(200, 250));
            for (int i = 0; i < testFretPattern.Buttons.Count; i++)
            {
                fretPatternPanel.Controls.Add(testFretPattern.Buttons[i]); // Add the button to the fretPatternPanel
                Debug.WriteLine(" init: " + testFretPattern.Buttons[i].Text);

            }
            */
        }

        private void CreateFretPatterns()
        {
            int numberOfPatterns = 12;
            int patternsPerRow = 3;
            int patternSpacing = 150;
            int patternWidth = 400; // Adjust as needed
            int patternHeight = 550; // Adjust as needed

            for (int i = 0; i < numberOfPatterns; i++)
            {
                int row = i / patternsPerRow;
                int col = i % patternsPerRow;

                FretPattern pattern = new FretPattern($"Pattern {i + 1}", 0, this);
                allFretPatterns.Add(pattern); // Add the pattern to the list
                Point location = new Point(col * (patternWidth + patternSpacing) + 20, row * (patternHeight + patternSpacing) + 200);
                pattern.CreateFretboard(6, 4, pattern.BaseFret, location, i);

                // Add fret buttons, activation button, and base fret control to the panel
                foreach (Button button in pattern.Buttons)
                {
                    fretPatternPanel.Controls.Add(button);
                }
                fretPatternPanel.Controls.Add(pattern.ActivateButton);
                fretPatternPanel.Controls.Add(pattern.BaseFretControl);
            }
        }
        public void DeactivateOtherFretPatterns(FretPattern activePattern)
        {
            foreach (var pattern in allFretPatterns)
            {
                if (pattern != activePattern)
                {
                    pattern.Deactivate();
                }
            }
        }

        private void FingerPatternModeButton_Click(object sender, EventArgs e)
        {
            var theButton = sender as Button;
            if (theButton != null)
            {
                if (fringerPatternMode == true)
                {
                    fringerPatternMode = false;
                    FingerPatternModeButton.BackColor = Color.White;
                    // if less than 12 defined patterns set the UI for next finger pattern
                    return;
                }
                if (fringerPatternMode == false)
                {
                    fringerPatternMode = true;
                    FingerPatternModeButton.BackColor = Color.Red;
                }
            }
        }

        // Method to clear the existing patterns from the UI
        private void ClearFretPatternsUI()
        {
            // Clear the UI elements that represent the fret patterns
            // This will depend on how your UI is structured
            fretPatternPanel.Controls.Clear(); // Example for clearing a panel
        }
        private void saveFretPatternsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFretPatternsFileDialog.Filter = "Guitar Fret Patterns (*.gfp)|*.gfp";
            saveFretPatternsFileDialog.DefaultExt = "gfp";
            saveFretPatternsFileDialog.AddExtension = true;

            if (saveFretPatternsFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFretPatternsFileDialog.FileName;
                try
                {
                    // Create a list to hold the serializable data

                    List<FretPatternData> allPatternData = new List<FretPatternData>();

                    // Populate the list with data from each FretPattern
                    foreach (var pattern in allFretPatterns)
                    {
                        FretPatternData patternData = pattern.GetSerializableData();
                        allPatternData.Add(patternData);
                    }
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string jsonString = JsonSerializer.Serialize(allPatternData, options);
                    File.WriteAllText(fileName, jsonString);
                    MessageBox.Show("Fret patterns saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save fret patterns: {ex.Message}", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadFretPatternsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadFretPatternsDialog.Filter = "Guitar Fret Patterns (*.gfp)|*.gfp";
            loadFretPatternsDialog.DefaultExt = "gfp";

            if (loadFretPatternsDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = loadFretPatternsDialog.FileName;
                try
                {
                    string jsonString = File.ReadAllText(fileName);

                    // Deserialize the JSON to a list of FretPatternData
                    List<FretPatternData> loadedPatternData = JsonSerializer.Deserialize<List<FretPatternData>>(jsonString);

                    // Clear the existing UI elements
                    fretPatternPanel.Controls.Clear();
                    allFretPatterns.Clear();

                    // Constants for layout
                    int patternsPerRow = 3;
                    int patternSpacing = 150;
                    int patternWidth = 400; // Adjust as needed
                    int patternHeight = 550; // Adjust as needed

                    // Rebuild the UI elements with the loaded data
                    for (int i = 0; i < loadedPatternData.Count; i++)
                    {
                        var patternData = loadedPatternData[i];
                        int row = i / patternsPerRow;
                        int col = i % patternsPerRow;

                        // Calculate the location for each pattern
                        Point location = new Point(col * (patternWidth + patternSpacing) + 20, row * (patternHeight + patternSpacing) + 200);

                        FretPattern pattern = new FretPattern(patternData.Name, patternData.BaseFret, this);
                        pattern.Description = patternData.Description;
                        pattern.IsActive = patternData.IsActive;

                        // Set up the pattern's UI elements
                        pattern.CreateFretboard(6, 4, pattern.BaseFret, location, i);

                        // Update the activation button text and back color
                        pattern.ActivateButton.Text = patternData.ActivateButtonText;
                        pattern.ActivateButton.BackColor = Color.FromArgb(patternData.ActivateButtonBackColor);
                        pattern.ActivateButton.Tag = patternData.ActivateButtonTag;

                        // Update the base fret control value
                        if (patternData.BaseFret >= pattern.BaseFretControl.Minimum && patternData.BaseFret <= pattern.BaseFretControl.Maximum)
                        {
                            pattern.BaseFretControl.Value = patternData.BaseFret;
                        }
                        else
                        {
                            // Set to a default value or minimum value if the loaded value is out of range
                            pattern.BaseFretControl.Value = pattern.BaseFretControl.Minimum;
                        }


                        // Update the buttons with the loaded names and back colors
                        for (int j = 0; j < pattern.Buttons.Count; j++)
                        {
                            pattern.Buttons[j].Text = patternData.FretButtonName[j];
                            pattern.Buttons[j].BackColor = Color.FromArgb(patternData.FretButtonBackColor[j]);
                            pattern.Buttons[j].Tag = new Tuple<int, int>(patternData.FretTags[j].StringIndex, patternData.FretTags[j].FretIndex);

                        }

                        // Add the pattern to the list
                        allFretPatterns.Add(pattern);

                        // Add the pattern's UI elements to the panel
                        foreach (Button button in pattern.Buttons)
                        {
                            fretPatternPanel.Controls.Add(button);
                        }
                        fretPatternPanel.Controls.Add(pattern.ActivateButton);
                        fretPatternPanel.Controls.Add(pattern.BaseFretControl);
                    }

                    // Optionally, refresh the panel or form if necessary
                    fretPatternPanel.Refresh();

                    MessageBox.Show("Fret patterns loaded successfully.", "Load Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load fret patterns: {ex.Message}", "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void UpdateFretPatternsUI()
        {
            Color openStringSelectedColor = Color.Bisque; // or any color you use for selected open strings
            Color openStringNotSelectedColor = Color.Blue; // or any color you use for not selected open strings
            Color fretSelectedColor = Color.Bisque; // Color for selected frets
            Color fretNotSelectedColor = Color.Beige; // Color for not selected frets

            for (int i = 0; i < allFretPatterns.Count; i++)
            {
                FretPattern pattern = allFretPatterns[i];

                // Update the activation button
                pattern.ActivateButton.BackColor = pattern.IsActive ? Color.Red : Color.Beige;
                pattern.ActivateButton.Text = pattern.IsActive ? "Active" : "Activate";

                // Update the base fret numeric up/down
                pattern.BaseFretControl.Value = pattern.BaseFret;

                // Update the fret buttons
                foreach (Button button in pattern.Buttons)
                {
                    Tuple<int, int> tag = (Tuple<int, int>)button.Tag;
                    int stringIndex = tag.Item1;
                    int fretIndex = tag.Item2;

                    // Check if the button is for an open string
                    if (fretIndex == -1)
                    {
                        // Open string button color is based on its current color
                        button.BackColor = button.BackColor == openStringSelectedColor ? openStringSelectedColor : openStringNotSelectedColor;
                        button.Text = pattern.GetNoteName(stringIndex, 0); // Assuming open string is equivalent to fret 0
                    }
                    else
                    {
                        // Regular fret button color is based on its current color
                        button.BackColor = button.BackColor == fretSelectedColor ? fretSelectedColor : fretNotSelectedColor;
                        button.Text = pattern.GetNoteName(stringIndex, fretIndex + pattern.BaseFret);
                    }
                }
            }
        }
    }

    public static class MidiUtilities
    {
        public static double GetFrequencyFromMidiNote(int midiNoteNumber)
        {
            return 440.0 * Math.Pow(2.0, (midiNoteNumber - 69) / 12.0);
        }
        public static int BpmToMicrosecondsPerQuarterNote(int bpm)
        {
            return 60000000 / bpm;
        }

        public static int TicksPerQuarterNote
        {
            get { return 960; } // Standard value for MIDI files
        }
    }



    public static class GlobalConfig
    {
        public static WaveFormat GlobalWaveFormat { get; } = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);

    }

    [Serializable]
    public class Form1Data
    {
        public List<Note> AllNotes { get; set; }
        public Composition Composition { get; set; }
        public int FretboardMidiNoteNumber { get; set; }
        public float PixelsPerSecond { get; set; } = 50.0f;
        public int Version { get; set; }
        public int[] MidiChannelPerString { get; set; }

        public int timeSignatureNumerator { get; set; }
        public int timeSignatureDenominator { get; set; }
        public int tempo { get; set; }

    }


    // Custom ISampleProvider to prepend silence to a note
    public class SilencePrependedSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider source;
        private readonly int silenceSamples;
        private int samplesProvided = 0;

        public SilencePrependedSampleProvider(ISampleProvider source, float startTime, WaveFormat waveFormat)
        {
            this.source = source;
            this.silenceSamples = (int)(startTime * waveFormat.SampleRate);
        }

        public WaveFormat WaveFormat => source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesNeeded = count;
            int samplesRead = 0;

            // Provide silence
            while (samplesProvided < silenceSamples && samplesNeeded > 0)
            {
                buffer[offset + samplesRead] = 0f;
                samplesRead++;
                samplesProvided++;
                samplesNeeded--;
            }

            // Provide actual note samples
            if (samplesNeeded > 0)
            {
                int readFromSource = source.Read(buffer, offset + samplesRead, samplesNeeded);
                samplesRead += readFromSource;
                samplesProvided += readFromSource;
            }

            return samplesRead;
        }
    }
}




