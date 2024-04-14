



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
    using NAudio.Utils;
    using System.Security.Principal;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
    using RadioButton = RadioButton;
   
    using MathNet.Numerics.IntegralTransforms;

    using System.Numerics;
    
    using System.Diagnostics.Eventing.Reader;
    using System.Text;

    [Serializable]
    public partial class Form1 : Form
    {
        public KarplusStrong karplusStrong;
        private MidiHandler midiHandler;
        private WaveOut waveOut;
        private WaveOut playWaveOut;
        private System.Windows.Forms.Timer midiPlaybackTimer = new System.Windows.Forms.Timer();
        private float currentTime = 0;
        private int currentEventIndex = 0;
        public KarplusStrongSampleProvider sampleProvider;
        public int fretboardMidiNoteNumber = 0;
        private List<Note> allNotes = new List<Note>();
        private Composition composition;
        private Composition fingerPatternComposition = new Composition();

        private float PixelsPerSecond = 150.0f;  // zoom factor: adjust this value as needed
        private bool noNoteAddedSinceLastSpacePress;
        public Note selectedNote = null;
        public Note noteToDelete = null;
        public Point initialMousePosition;
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
        private int numberOfBars = 100;
        private IWavePlayer waveOutDevice;
        private IWavePlayer PlayWaveOutDevice;
        private MixingSampleProvider mixer;


        private int currentNoteIndex = 0;
        private float currentPlaybackTime = 0;

        private FretPattern activeFretPattern;
        public List<FretPattern> allFretPatterns = new List<FretPattern>();
        public List<FingeringPattern> allFingeringPatterns = new List<FingeringPattern>();
        public int activeFingeringPattern = 0;
        public bool fingeringPatternMode = false;
        public bool fretPatternMode = false;

        public bool triplePerBeatMode = false;
        public bool quintuplePerBeatMode = false;
        public bool sexatupePerBeatMode = false;
        public bool septatuplePerBeatMode = false;

        private string currentAudioFilePath;
        private AudioFileReader audioFileReader;
        private int filenumber = 1;
        private bool staccatoMode = false;
        private int selectedStringIndex;
        private Guitar theGuitar;
        public float lowPassCutOffValue;
        public float lowPassQValue;
        private bool applyToAllStrings = false;

        public Form1()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;

            WaveFormat format = GlobalConfig.GlobalWaveFormat;
            string midiFilePath = "firstglobal.gur"; // Ensure this path is correct
            double defaultTempo = 60.0; // Example default tempo
            midiHandler = new MidiHandler(midiFilePath, defaultTempo);
            karplusStrong = new KarplusStrong(GlobalConfig.GlobalWaveFormat.SampleRate,333);
            // InitializeAudio();
            this.KeyPreview = true;



            // Attach the single event handler to all relevant buttons
            foreach (Control control in fretboardPanel.Controls)
            {
                if (control is Button && control.Tag != null)
                {

                    control.Click += FretboardButton_Click;
                }
            }

            karplusStrong = new KarplusStrong(format.SampleRate, 220.0f); // This is just an initial frequency.
            karplusStrong.UpdateFrequency(20000f);
            sampleProvider = new KarplusStrongSampleProvider(karplusStrong, 20000f);
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
            noireButton.BackColor = Color.Coral;
            crocheButton.BackColor = Color.LightGreen;
            doubleCrocheButton.BackColor = Color.LightGreen;
            tripleCrocheButton.BackColor = Color.LightGreen;
            triplePerBeatButton.BackColor = Color.LightGreen;
            quintuplePerBeatButton.BackColor = Color.LightGreen;
            sexatuplePerBeatButton.BackColor = Color.LightGreen;
            setptatuplePerBeatButton.BackColor = Color.LightGreen;

            CreateFretPatterns();

            for (int i = 0; i < 12; i++)
            {
                FingeringPattern fingeringPattern = new FingeringPattern();

                allFingeringPatterns.Add(fingeringPattern);
            }
            InitFretBoard();
            fingerPatternComposition.Title = "fingering pattern";

            for (int i = 0; i < 6; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Height = 50;
                radioButton.Width = 200;
                radioButton.Text = $"String {i + 1}";
                radioButton.Location = new Point((i * 200), 80); // Adjust the location for each button
                if (i == 0) radioButton.Checked = true; else radioButton.Checked = false;
                radioButton.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
                stringSynthroupBox.Controls.Add(radioButton);
            }


            theGuitar = new Guitar(GlobalConfig.GlobalWaveFormat.SampleRate);


            // loadfilesfortesting();
            Debug.WriteLine("Init completed");



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void loadfilesfortesting()
        {/*
            string guitarbody = "RAMIREZ.wav";
            string reverb = "Performance Hall - XY Close.wav";
            float[] impulseResponse = LoadWaveFile("RAMIREZ.wav");
            for (int i = 0; i < 6; i++)
            {

                // Now, pass this impulseResponse to the KarplusStrong 
                theGuitar.strings[i].SetImpulseResponse(impulseResponse);


                //might as well set the lowpassfilter:)
                theGuitar.strings[i].lowPassCutOffValue = lowPassCutOffValue;//default value
            }
            LoadFretPatterns("base.gfp");
*/
            string jsonString = File.ReadAllText("first.gur");
            Form1Data data = JsonSerializer.Deserialize<Form1Data>(jsonString);

            this.allNotes = data.AllNotes;
            this.composition = data.Composition;
            //this.fretboardMidiNoteNumber = data.FretboardMidiNoteNumber;
            //this.PixelsPerSecond = data.PixelsPerSecond;
            // this.midiChannelPerString = data.MidiChannelPerString;
            // this.timeSignatureNumerator = data.timeSignatureNumerator;
            // this.timeSignatureDenominator = data.timeSignatureNumerator;
            //this.tempo = data.tempo;

            //check the version and handle older versions accordingly(e.g., provide default values for new properties or convert data from old formats).
            //if (this.version != data.Version) MessageBox.Show("wrong version");// we can do better :) but for now that's it...

            guitarRollPanel.Invalidate(); // Refresh the panel to reflect the loaded data

        }


        private void InitFretBoard()
        {
            int buttonWidth = 100;
            int buttonHeight = 50;
            int numberOfStrings = 6;
            int numberOfFrets = 24;

            // Clear existing buttons if needed
            fretboardPanel.Controls.Clear();

            for (int stringIndex = 0; stringIndex < numberOfStrings; stringIndex++)
            {
                for (int fretIndex = 0; fretIndex < numberOfFrets; fretIndex++)
                {
                    int midiNoteNumber = GetMidiNoteNumber(stringIndex, fretIndex);
                    Button fretButton = new Button
                    {
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Location = new Point(fretIndex * buttonWidth, stringIndex * buttonHeight),
                        Name = $"string{stringIndex}fret{fretIndex}Button",
                        Text = GetNoteName(midiNoteNumber),
                        Tag = new Tuple<int, int>(midiNoteNumber, stringIndex)
                    };

                    // Subscribe to the Click event of the button
                    fretButton.Click += FretboardButton_Click;

                    // Add the button to the fretboardPanel
                    fretboardPanel.Controls.Add(fretButton);
                }
            }
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;



            Label[] frequencyLabels = {
            EQBand1FrequencyLabel,
            EQBand2FrequencyLabel,
            EQBand3FrequencyLabel,
            EQBand4FrequencyLabel,
            EQBand5FrequencyLabel,
            EQBand6FrequencyLabel,
            EQBand7FrequencyLabel,
            EQBand8FrequencyLabel
            };

            Label[] widthLabels = {
            EQBand1WidthLabel,
            EQBand2WidthLabel,
            EQBand3WidthLabel,
            EQBand4WidthLabel ,
            EQBand5WidthLabel,
            EQBand6WidthLabel,
            EQBand7WidthLabel,
            EQBand8WidthLabel};

            Label[] gainLabels = {
            EQBand1GainLabel,
            EQBand2GainLabel,
            EQBand3GainLabel,
            EQBand4GainLabel,
            EQBand5GainLabel,
            EQBand6GainLabel,
            EQBand7GainLabel,
            EQBand8GainLabel,};


            TrackBar[] frequencies = {

                EQBand1FrequencySlider,
                EQBand2FrequencySlider,
                EQBand3FrequencySlider,
                EQBand4FrequencySlider,
                EQBand5FrequencySlider,
                EQBand6FrequencySlider,
                EQBand7FrequencySlider,
                EQBand8FrequencySlider};

            TrackBar[] gains =
            {
                EQBand1GainSlider,
                EQBand2GainSlider,
                EQBand3GainSlider,
                EQBand4GainSlider,
                EQBand5GainSlider,
                EQBand6GainSlider,
                EQBand7GainSlider,
                EQBand8GainSlider};

            TrackBar[] widths =
            {
                EQBand1WidthSlider,
                EQBand2WidthSlider,
                EQBand3WidthSlider,
                EQBand4WidthSlider,
                EQBand5WidthSlider,
                EQBand6WidthSlider,
                EQBand7WidthSlider,
                EQBand8WidthSlider};


            if (radioButton != null && radioButton.Checked)
            {
                selectedStringIndex = int.Parse(radioButton.Text.Split(' ')[1]) - 1;

                // Update checkbox based on lowPassActive  and eqActive state
                lowPassActiveCheckBox.Checked = theGuitar.strings[selectedStringIndex].lowPassActive;
                equalizerActiveCheckBox.Checked = theGuitar.strings[selectedStringIndex].eqActive;

                //update the string 8 bands eq labels
                for (int i = 0; i < 8; i++)
                {
                    frequencies[i].Value = theGuitar.strings[selectedStringIndex].bands[i].Frequency;
                    frequencyLabels[i].Text = theGuitar.strings[selectedStringIndex].bands[i].Frequency.ToString();

                    //******************************************************************
                    // transform value of trackbar between .1 and 3 (i.e. divide by 10)
                    float scaledValue = Math.Clamp(theGuitar.strings[selectedStringIndex].bands[i].Bandwidth, 0.1f, 3.0f) * 10;
                    widths[i].Value = (int)scaledValue;
                    widthLabels[i].Text = (scaledValue / 10f).ToString();

                    gains[i].Value = (int)theGuitar.strings[selectedStringIndex].bands[i].Gain;
                    gainLabels[i].Text = theGuitar.strings[selectedStringIndex].bands[i].Gain.ToString();

                }

                if (theGuitar.strings[selectedStringIndex].lowPassCutOffValue > 50 && theGuitar.strings[selectedStringIndex].lowPassCutOffValue < 10000)
                    lowPassFrequencyCutoffnumericUpDown.Value = (int)theGuitar.strings[selectedStringIndex].lowPassCutOffValue;

                if (theGuitar.strings[selectedStringIndex].lowPassQValue > 1f && theGuitar.strings[selectedStringIndex].lowPassQValue < 10)
                {
                    lowPassQTrackBar.Value = (int)theGuitar.strings[selectedStringIndex].lowPassQValue * 10;
                    lowPassResonanceLabel.Text = "low pass  resonance (Q): " + theGuitar.strings[selectedStringIndex].lowPassQValue;
                }
                else if (theGuitar.strings[selectedStringIndex].lowPassQValue >= .1f && theGuitar.strings[selectedStringIndex].lowPassQValue <= 1)
                {
                    float newvalue = theGuitar.strings[selectedStringIndex].lowPassQValue * 10;

                    lowPassQTrackBar.Value = (int)newvalue;
                    lowPassResonanceLabel.Text = "low pass  resonance (Q): " + theGuitar.strings[selectedStringIndex].lowPassQValue;
                }

                if (theGuitar.strings[selectedStringIndex].attackPhaseSamples >= 1 && theGuitar.strings[selectedStringIndex].attackPhaseSamples <= 48000)
                {
                    attackPhaseNmericUpDown.Value = (int)theGuitar.strings[selectedStringIndex].attackPhaseSamples;
                }
            }
        }



        private void attackPhaseNmericUpDown_ValueChanged_2(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].attackPhaseSamples = (int)attackPhaseNmericUpDown.Value;
        }

        private void lowPassActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Update lowPassActive based on the checkbox checked state
            theGuitar.strings[selectedStringIndex].lowPassActive = lowPassActiveCheckBox.Checked;
        }

        private void lowPassFrequencyCutoffnumericUpDown_ValueChanged_1(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].lowPassCutOffValue = (float)lowPassFrequencyCutoffnumericUpDown.Value;
            theGuitar.strings[selectedStringIndex].SetlowPassCutOffValue((float)lowPassFrequencyCutoffnumericUpDown.Value);
        }
        private void lowPassQTrackBar_ValueChanged_1(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].lowPassQValue = (float)lowPassQTrackBar.Value / 10;//divide the value by 10
            lowPassResonanceLabel.Text = "low pass  resonance (Q): " + theGuitar.strings[selectedStringIndex].lowPassQValue;
            theGuitar.strings[selectedStringIndex].SetlowPassQValue(theGuitar.strings[selectedStringIndex].lowPassQValue);

        }








        private void InitializeComposition()
        {
            composition = new Composition
            {
                Title = "May and June",
                Tempo = this.tempo,
                TimeSignatureNumerator = this.timeSignatureNumerator,
                TimeSignatureDenominator = this.timeSignatureDenominator,

            };


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
                var currentComposition = fingeringPatternMode ? fingerPatternComposition : composition;

                currentComposition.Notes.Remove(noteToDelete);
                noteToDelete = null;
                guitarRollPanel.Invalidate();
            }
        }

        private void nextNoteButton_Click(object sender, EventArgs e)
        {
            currentTime += activeNoteDuration;
            guitarRollPanel.Invalidate();
        }

        private void deleteSelectedNoteButton_Click(object sender, EventArgs e)
        {
            var currentComposition = fingeringPatternMode ? fingerPatternComposition : composition;

            currentComposition.Notes.Remove(noteToDelete);
            noteToDelete = null;
            guitarRollPanel.Invalidate();
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


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "gur";
            openFileDialog1.Filter = "Guitarsharp Files (*.gur)|*.gur";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                string jsonString = File.ReadAllText(openFileDialog1.FileName);
                // Form1Data data = JsonSerializer.Deserialize<Form1Data>(jsonString);
                AllDataContainer container = JsonSerializer.Deserialize<AllDataContainer>(jsonString);
                this.allNotes = container.Form1Data.AllNotes;
                this.composition = container.Form1Data.Composition;
                this.fretboardMidiNoteNumber = container.Form1Data.FretboardMidiNoteNumber;
                this.PixelsPerSecond = container.Form1Data.PixelsPerSecond;
                this.midiChannelPerString = container.Form1Data.MidiChannelPerString;
                this.timeSignatureNumerator = container.Form1Data.timeSignatureNumerator;
                this.timeSignatureDenominator = container.Form1Data.timeSignatureNumerator;
                this.tempo = container.Form1Data.tempo;

                //check the version and handle older versions accordingly(e.g., provide default values for new properties or convert data from old formats).
                if (this.version != container.Form1Data.Version) MessageBox.Show("wrong version");// we can do better :) but for now that's it...
                                                                                                  // Load fret patterns
                LoadFretPatternsFromData(container.FretPatternData);

                // Load fingering patterns
                allFingeringPatterns = container.FingeringPatterns;

                guitarRollPanel.Invalidate(); // Refresh the panel to reflect the loaded data
            }
        }

        private void LoadFretPatternsFromData(List<FretPatternData> fretPatternData)
        {
            // Clear the existing UI elements
            fretPatternPanel.Controls.Clear();
            allFretPatterns.Clear();

            // Constants for layout
            int patternsPerRow = 7;
            int patternSpacing = 100;
            int patternWidth = 400; // Adjust as needed
            int patternHeight = 1500; // Adjust as needed

            // Rebuild the UI elements with the loaded data
            for (int i = 0; i < fretPatternData.Count; i++)
            {
                var patternData = fretPatternData[i];
                int row = i / patternsPerRow;
                int col = i % patternsPerRow;

                // Calculate the location for each pattern
                Point location = new Point(col * (patternWidth + patternSpacing) + 20, row * (patternHeight + patternSpacing) + 200);

                FretPattern pattern = new FretPattern(patternData.Name, patternData.BaseFret, this);
                pattern.Description = patternData.Description;
                pattern.IsActive = patternData.IsActive;

                // Set up the pattern's UI elements
                pattern.CreateFretboard(6, 13, pattern.BaseFret, location, i);





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

            }
            // activeFretPattern = allFretPatterns[0];
            // Optionally, refresh the panel or form if necessary
            fretPatternPanel.Refresh();

            //MessageBox.Show("Fret patterns loaded successfully.", "Load Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);




        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "gur";
            saveFileDialog1.Filter = "Guitarsharp Files (*.gur)|*.gur";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var container = new AllDataContainer
                {
                    Form1Data = GetForm1Data(),
                    FretPatternData = GetAllFretPatternData(),
                    FingeringPatterns = allFingeringPatterns // Assuming this is your list of fingering patterns
                };

                string jsonString = JsonSerializer.Serialize(container, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(saveFileDialog1.FileName, jsonString);
            }


        }
        private List<FretPatternData> GetAllFretPatternData()
        {
            List<FretPatternData> allPatternData = new List<FretPatternData>();
            foreach (var pattern in allFretPatterns)
            {
                FretPatternData patternData = pattern.GetSerializableData();
                allPatternData.Add(patternData);
            }
            return allPatternData;
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
                tripletButton.BackColor = Color.Coral; // Example active color

                if (rondeButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Ronde) * (2.0f / 3.0f);
                else if (blancheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Blanche) * (2.0f / 3.0f);
                else if (noireButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Noire) * (2.0f / 3.0f);
                else if (crocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Croche) * (2.0f / 3.0f);
                else if (doubleCrocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.DoubleCroche) * (2.0f / 3.0f);
                else if (tripleCrocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.TripleCroche) * (2.0f / 3.0f);

                isTriplet = true;
            }
            else if (isTriplet == true)
            {
                tripletButton.BackColor = SystemColors.Control; // Default color

                isTriplet = false;

                if (rondeButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Ronde);
                else if (blancheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Blanche);
                else if (noireButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Noire);
                else if (crocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Croche);
                else if (doubleCrocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.DoubleCroche);
                else if (tripleCrocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.TripleCroche);


            }
        }
        private void pointeeButton_Click(object sender, EventArgs e)
        {


            if (isDotted == false)
            {
                pointeeButton.BackColor = Color.Coral; // Example active color

                if (rondeButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Ronde) * 1.5f;
                else if (blancheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Blanche) * 1.5f;
                else if (noireButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Noire) * 1.5f;
                else if (crocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Croche) * 1.5f;
                else if (doubleCrocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.DoubleCroche) * 1.5f;
                else if (tripleCrocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.TripleCroche) * 1.5f;

                isDotted = true;
            }
            else if (isDotted == true)
            {
                pointeeButton.BackColor = SystemColors.Control; // Default color

                isDotted = false;

                if (rondeButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Ronde);
                else if (blancheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Blanche);
                else if (noireButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Noire);
                else if (crocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.Croche);
                else if (doubleCrocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.DoubleCroche);
                else if (tripleCrocheButton.BackColor == Color.Coral) activeNoteDuration = GetNoteMultiplier(NoteType.TripleCroche);



            }
        }
        private void rondeButton_Click(object sender, EventArgs e)
        {
            activeNoteDuration = baseNoteDuration * GetNoteMultiplier(NoteType.Ronde);
            blancheButton.BackColor = Color.LightGreen;
            rondeButton.BackColor = Color.Coral;
            noireButton.BackColor = Color.LightGreen;
            crocheButton.BackColor = Color.LightGreen;
            doubleCrocheButton.BackColor = Color.LightGreen;
            tripleCrocheButton.BackColor = Color.LightGreen;

        }

        private void blancheButton_Click(object sender, EventArgs e)
        {
            activeNoteDuration = baseNoteDuration * GetNoteMultiplier(NoteType.Blanche);
            blancheButton.BackColor = Color.Coral;
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
            noireButton.BackColor = Color.Coral;
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
            crocheButton.BackColor = Color.Coral;
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
            doubleCrocheButton.BackColor = Color.Coral;
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
            tripleCrocheButton.BackColor = Color.Coral;
        }



        private void triplePerBeatButton_Click(object sender, EventArgs e)
        {

            triplePerBeatMode = !triplePerBeatMode;

            triplePerBeatButton.BackColor = triplePerBeatMode ? Color.Coral : Color.LightGreen;

            if (!triplePerBeatMode)
            {

                activeNoteDuration *= 3;
            }

            else
            {
                if (activeNoteDuration > 0)
                {
                    activeNoteDuration /= 3;
                }
            }

        }

        private void quintuplePerBeatButton_Click(object sender, EventArgs e)
        {
            quintuplePerBeatMode = !quintuplePerBeatMode;
            quintuplePerBeatButton.BackColor = quintuplePerBeatMode ? Color.Coral : Color.LightGreen;

            if (!quintuplePerBeatMode)
            {

                activeNoteDuration *= 5;
            }

            else
            {
                if (activeNoteDuration > 0)
                {
                    activeNoteDuration /= 5;
                }
            }

        }

        private void sexatuplePerBeatButton_Click(object sender, EventArgs e)
        {
            sexatupePerBeatMode = !sexatupePerBeatMode;
            sexatuplePerBeatButton.BackColor = sexatupePerBeatMode ? Color.Coral : Color.LightGreen;
            if (!sexatupePerBeatMode)
            {

                activeNoteDuration *= 6;
            }

            else
            {
                if (activeNoteDuration > 0)
                {
                    activeNoteDuration /= 6;
                }
            }

        }

        private void setptatuplePerBeatButton_Click(object sender, EventArgs e)
        {
            septatuplePerBeatMode = !septatuplePerBeatMode;
            setptatuplePerBeatButton.BackColor = septatuplePerBeatMode ? Color.Coral : Color.LightGreen;
            if (!septatuplePerBeatMode)
            {

                activeNoteDuration *= 7;
            }

            else
            {
                if (activeNoteDuration > 0)
                {
                    activeNoteDuration /= 7;
                }
            }

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

            PlayWaveOutDevice = new WaveOutEvent();
            mixer = new MixingSampleProvider(GlobalConfig.GlobalWaveFormat);
            mixer.ReadFully = true;
            PlayWaveOutDevice.Init(mixer);
            PlayWaveOutDevice.Play();
        }


        // Start playing
        private async void startPlayingButton_Click(object sender, EventArgs e)
        {
            string audioFilePath = "";
            List<float>[] mixedAudioData = new List<float>[6];
            try
            {
                startPlayingButton.Enabled = false;
                SetStatusMessage("Generating audio...");


                // Generate the WAV file instead of a MemoryStream
                await Task.Run(() => audioFilePath = GenerateCompositionAudioFile(composition));

                SetStatusMessage("Playing the audio...");

                // Play the generated WAV file
                PlayAudioFile(audioFilePath);



                /*
                // Generate a MemoryStream instead of a Wav file
                await Task.Run(() => mixedAudioData = GenerateCompositionAudio(composition));

                SetStatusMessage("Playing the audio...");

                // Play the generated WAV file
                // PlayAudioFile(audioFilePath);

                byte[] mixedAudioDataBytes = MixAudioDataStream(mixedAudioData);

                PlayAudioFromMemory(mixedAudioDataBytes);
                */

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                startPlayingButton.Enabled = true;
                SetStatusMessage("playing audio: " + audioFilePath);
            }
        }



        // Method to play the audio file
        private void PlayAudioFile(string filePath)
        {
            DisposeAudioResources();


            PlayWaveOutDevice = new WaveOutEvent();
            var audioFileReader = new AudioFileReader(filePath);

            PlayWaveOutDevice.Init(audioFileReader);
            PlayWaveOutDevice.PlaybackStopped += OnPlaybackStopped;
            PlayWaveOutDevice.Play();
        }

        // This method is the event handler for the PlaybackStopped event
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Clean up resources after playback has naturally finished
            CleanUpAfterPlayback();
        }
        // Stop playing

        // This method is the event handler for the Stop Playing button click
        private void StopPlayingButton_Click(object sender, EventArgs e)
        {
            // Stop the audio playback if it's currently playing
            if (PlayWaveOutDevice != null && PlayWaveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                PlayWaveOutDevice.Stop(); // This will eventually trigger the OnPlaybackStopped event
            }
            else
            {
                // If not playing, clean up right away
                CleanUpAfterPlayback();
            }
        }

        // This method handles the cleanup
        private void CleanUpAfterPlayback()
        {
            DisposeAudioResources();

            SetStatusMessage("Playback stopped. " + currentAudioFilePath);
            // absolute strangeness... but it works!!!!! mmmm... sometimes..
            /*
            if (filenumber > 2)
                File.Delete("composition" + filenumber-- + ".wav");



            // Delete the file if it exists
            if (!string.IsNullOrEmpty(currentAudioFilePath) && File.Exists(currentAudioFilePath))
            {
                try
                {
                    File.Delete(currentAudioFilePath);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting temp file: {ex.Message}");
                }
            }

            //currentAudioFilePath = null; // Clear the file path
            */
        }


        // Dispose resources when done
        private void DisposeAudioResources()
        {
            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }

            if (PlayWaveOutDevice != null)
            {
                PlayWaveOutDevice.Dispose();
                PlayWaveOutDevice = null;
            }

            // Clear the file path after disposing of the resources
            // currentAudioFilePath = null;
        }

        private List<float>[] GenerateCompositionAudio(Composition composition)
        {


            // Step 1: Organize notes by string
            List<Note>[] notesPerString = new List<Note>[6];
            for (int i = 0; i < notesPerString.Length; i++)
            {
                notesPerString[i] = composition.Notes.Where(n => n.StringNumber == i).OrderBy(n => n.StartTime).ToList();
            }

            // Step 2: Generate audio for each string
            List<float>[] audioDataPerString = new List<float>[6];


            for (int i = 0; i < notesPerString.Length; i++)
            {
                audioDataPerString[i] = GenerateAudioForString(notesPerString[i], theGuitar.strings[i], i);
            }


            // Return the file path of the generated audio file
            return audioDataPerString;
        }




        private string GenerateCompositionAudioFile(Composition composition)
        {


            // Step 1: Organize notes by string
            List<Note>[] notesPerString = new List<Note>[6];
            for (int i = 0; i < notesPerString.Length; i++)
            {
                notesPerString[i] = composition.Notes.Where(n => n.StringNumber == i).OrderBy(n => n.StartTime).ToList();
            }

            // Step 2: Generate audio for each string
            List<float>[] audioDataPerString = new List<float>[6];


            for (int i = 0; i < notesPerString.Length; i++)
            {
                audioDataPerString[i] = GenerateAudioForString(notesPerString[i], theGuitar.strings[i], i);
            }

            // Step 3: Mix audio from all strings
            List<float> mixedAudioData = MixAudioData(audioDataPerString);

            // Step 4: Write mixed audio to file
            string filePath = WriteAudioToFile(mixedAudioData, "Take " + filenumber++ + ".wav");

            // Return the file path of the generated audio file
            return filePath;
        }




        /**************************************************************************************************/
        private List<float> GenerateAudioForString(List<Note> notes, KarplusStrong stringSynth, int stringNumber)
        {


            List<float> stringAudio = new List<float>();

            // If the first note starts after time zero, add silence up to the start time of the first note
            if (notes.Any() && notes.First().StartTime > 0)
            {
                int silenceSamples = (int)(notes.First().StartTime * GlobalConfig.GlobalWaveFormat.SampleRate);
                stringAudio.AddRange(new float[silenceSamples]);
            }

            // Define the duration of the attack phase in samples (e.g., 0.1 seconds)
            int attackPhaseSamples = stringSynth.attackPhaseSamples;


            for (int i = 0; i < notes.Count; i++)
            {
                Note currentNote = notes[i];
                Note nextNote = (i < notes.Count - 1) ? notes[i + 1] : null;

                // Update the frequency for the note
                stringSynth.UpdateFrequency((float)MidiUtilities.GetFrequencyFromMidiNote(currentNote.MidiNoteNumber));

                stringSynth.Pluck(((float)currentNote.Velocity / 127));

                // Determine the duration to generate based on staccato flag or gap to the next note
                var (samplesForNote, samplesForSilence) = CalculateSamplesToGenerate(currentNote, nextNote, GlobalConfig.GlobalWaveFormat.SampleRate);

                // Generate audio for the note
                for (int j = 0; j < samplesForNote; j++)
                {
                    // Generate the sample from your Karplus-Strong algorithm
                    float sample = stringSynth.NextSample();


                    if (j < attackPhaseSamples)
                    {
                        // Calculate an exponential envelope for the ramp-up
                        float envelope = (float)Math.Pow((j / (float)attackPhaseSamples), 2); // Exponential ramp-up
                        sample *= envelope;

                    }


                    stringAudio.Add(sample);
                }

                // Add silence if necessary
                if (samplesForSilence > 0)
                {
                    stringAudio.AddRange(new float[samplesForSilence]);
                }


            }


            return stringAudio;
        }


        private (int samplesForNote, int samplesForSilence) CalculateSamplesToGenerate(Note currentNote, Note nextNote, int sampleRate)
        {
            // Calculate samples for the current note's duration
            int samplesForCurrentNote = (int)((currentNote.EndTime - currentNote.StartTime) * sampleRate);

            // If the note is staccato, return samples just for the note's duration
            if (currentNote.IsStaccato)
            {
                // For staccato, calculate the silence period until the next note starts
                if (nextNote != null)
                {
                    int samplesForSilence = (int)((nextNote.StartTime - currentNote.EndTime) * sampleRate);
                    return (samplesForCurrentNote, samplesForSilence);
                }
                else
                {
                    // If there is no next note, return samples just for the current note's duration
                    return (samplesForCurrentNote, 0);
                }
            }
            else if (nextNote != null)
            {
                int samplesUntilNextNoteStarts = (int)((nextNote.StartTime - currentNote.EndTime) * sampleRate);
                return (samplesForCurrentNote + samplesUntilNextNoteStarts, 0);
            }
            return (samplesForCurrentNote + (sampleRate * 10), 0);
            /*
            // For legato, if there is a next note, calculate the samples to reach the start of the next note
            if (nextNote != null)
            {
                int samplesUntilNextNoteStarts = (int)((nextNote.StartTime - currentNote.EndTime) * sampleRate);
                // Ensure there's no gap between the notes for legato playing
                if (samplesUntilNextNoteStarts < 0)
                {
                    samplesUntilNextNoteStarts = 0;
                }
                return samplesForCurrentNote + samplesUntilNextNoteStarts;
            }

            // If there is no next note, just return samples for the current note's duration
            return samplesForCurrentNote;
            */
        }

        public byte[] ConvertFloatListToByteArray(List<float> floatData)
        {
            int sampleCount = floatData.Count;
            if (sampleCount == 0) Debug.WriteLine("sample count is zero");
            int bytesPerSample = sizeof(float); // Assuming 32-bit floats
            if (bytesPerSample == 0) Debug.WriteLine("sample count is zero");
            // Calculate total data size (including header)
            int dataSize = sampleCount * bytesPerSample + 44; // Adjust 44 for header size

            // Allocate memory for the byte array
            byte[] byteArray = new byte[dataSize];

            // **WAV Header Construction**
            string riffString = "RIFF";
            int chunkId = BitConverter.ToInt32(Encoding.ASCII.GetBytes(riffString), 0);
            int chunkSize = BitConverter.GetBytes(dataSize - 8)[0]; // -8 to exclude header size itself
            string waveString = "WAVE";
            int format = BitConverter.ToInt32(Encoding.ASCII.GetBytes(waveString), 0);
            string theformat = "fmt "; // Space required for header format
            int subchunk1Id = BitConverter.ToInt32(Encoding.ASCII.GetBytes(theformat), 0);
            int subchunk1Size = BitConverter.GetBytes(16)[0]; // PCM format requires 16 for subchunk1
            int audioFormat = BitConverter.GetBytes(1)[0]; // 1 for PCM
            int numChannels = BitConverter.GetBytes(2)[0]; // Assuming stereo
            int sampleRate = BitConverter.GetBytes(GlobalConfig.GlobalWaveFormat.SampleRate)[0];
            int byteRate = BitConverter.GetBytes(numChannels * bytesPerSample)[0];
            int blockAlign = BitConverter.GetBytes(bytesPerSample * numChannels)[0];
            int bitsPerSample = BitConverter.GetBytes(bytesPerSample * 8)[0];
            string dataString = "data";
            int subchunk2Id = BitConverter.ToInt32(Encoding.ASCII.GetBytes(dataString), 0);
            int subchunk2Size = BitConverter.GetBytes(sampleCount * bytesPerSample)[0];

            // Copy header bytes to the beginning of the array
            Array.Copy(BitConverter.GetBytes(chunkId), 0, byteArray, 0, 4);
            Array.Copy(BitConverter.GetBytes(chunkSize), 0, byteArray, 4, 4);
            Array.Copy(BitConverter.GetBytes(format), 0, byteArray, 8, 4);
            Array.Copy(BitConverter.GetBytes(subchunk1Id), 0, byteArray, 12, 4);
            Array.Copy(BitConverter.GetBytes(subchunk1Size), 0, byteArray, 16, 4);
            Array.Copy(BitConverter.GetBytes(audioFormat), 0, byteArray, 20, 4);
            Array.Copy(BitConverter.GetBytes(numChannels), 0, byteArray, 24, 4);
            Array.Copy(BitConverter.GetBytes(sampleRate), 0, byteArray, 28, 4);
            Array.Copy(BitConverter.GetBytes(byteRate), 0, byteArray, 32, 4);
            Array.Copy(BitConverter.GetBytes(blockAlign), 0, byteArray, 36, 4);
            Array.Copy(BitConverter.GetBytes(bitsPerSample), 0, byteArray, 40, 4);
            Array.Copy(BitConverter.GetBytes(subchunk2Id), 0, byteArray, 44, 4);
            Array.Copy(BitConverter.GetBytes(subchunk2Size), 0, byteArray, 48, 4);

            // Copy float data after the header
            int dataOffset = 44; // Offset after the 44-byte header
            for (int i = 0; i < sampleCount; i++)
            {
                float sample = floatData[i];
                byte[] sampleBytes = BitConverter.GetBytes(sample);
                Array.Copy(sampleBytes, 0, byteArray, dataOffset + i * bytesPerSample, bytesPerSample);
            }

            return byteArray;
        }



        private byte[] MixAudioDataStream(List<float>[] audioDataPerString)
        {
            // Find the maximum length of non-empty lists
            int maxSamples = audioDataPerString.Where(list => list.Count > 0).Max(list => list.Count);


            List<float> mixedAudio = new List<float>(maxSamples);

            for (int i = 0; i < maxSamples; i++)
            {
                float mixedSample = 0f;

                // Mix samples from each string
                foreach (var stringAudio in audioDataPerString)
                {
                    if (i < stringAudio.Count)
                    {
                        mixedSample += stringAudio[i];
                    }
                }
                mixedAudio.Add(mixedSample);
            }

            byte[] mixedAudioBytes = ConvertFloatListToByteArray(mixedAudio);
            return mixedAudioBytes;
        }
        private void PlayAudioFromMemory(byte[] audioData)
        {
            DisposeAudioResources(); // Dispose previous resources




            // Create a memory stream from the byte array
            using (MemoryStream memoryStream = new MemoryStream(audioData))
            {
                // Create a RawSourceWaveStream from the memory stream
                using (var waveStream = new RawSourceWaveStream(memoryStream, new WaveFormat(GlobalConfig.GlobalWaveFormat.SampleRate, 16, 2))) // Adjust format as needed
                {


                    // Create a WaveOut object for playback
                    using (var waveOut = new WaveOut())
                    {

                        waveOut.Init(waveStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            // Optional: Implement waiting logic while playing (avoid blocking UI thread)
                        }
                        waveOut.Stop();
                    }
                }
            }
        }

        private List<float> MixAudioData(List<float>[] audioDataPerString)
        {
            // Determine the longest audio data list
            int maxSamples = audioDataPerString.Max(a => a.Count);

            List<float> mixedAudio = new List<float>(maxSamples);

            for (int i = 0; i < maxSamples; i++)
            {
                float mixedSample = 0f;

                // Mix samples from each string
                foreach (var stringAudio in audioDataPerString)
                {
                    if (i < stringAudio.Count)
                    {
                        mixedSample += stringAudio[i];
                    }
                }

                // Normalize if necessary to prevent clipping
                // mixedSample = NormalizeSample(mixedSample);

                mixedAudio.Add(mixedSample);
            }

            return mixedAudio;
        }



        private string WriteAudioToFile(List<float> audioData, string fileName)
        {
            string filePath = fileName; // Path.Combine(Path.GetTempPath(), fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            using (var writer = new WaveFileWriter(fileStream, GlobalConfig.GlobalWaveFormat))
            {
                writer.WriteSamples(audioData.ToArray(), 0, audioData.Count);

            }
            ;
            return filePath;
        }




        // Update the UI to show a message to the user
        private void SetStatusMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetStatusMessage), message);
            }
            else
            {
                statusLabel.Text = message;
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
                            // buffer[i] = NormalizeSample(buffer[i]);
                        }
                        writer.WriteSamples(buffer, 0, samplesRead);
                        elapsedTime += (double)samplesRead / waveFormat.SampleRate;
                    }
                }

                //MessageBox.Show("Audio export completed successfully.", "Audio Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            int numberOfPatterns = 14; // Set the number of patterns to 14
            int patternsPerRow = 7;
            int patternSpacing = 100;
            int patternWidth = 400; // Adjust as needed
            int patternHeight = 1500; // Adjust as needed

            for (int i = 0; i < numberOfPatterns; i++)
            {
                int row = i / patternsPerRow;
                int col = i % patternsPerRow;

                FretPattern pattern = new FretPattern($"Pattern {i + 1}", 0, this);
                allFretPatterns.Add(pattern); // Add the pattern to the list
                Point location = new Point(col * (patternWidth + patternSpacing) + 20, row * (patternHeight + patternSpacing) + 200);
                pattern.CreateFretboard(6, 13, pattern.BaseFret, location, i); // Use 13 frets instead of 5

                // Add fret buttons, activation button, and base fret control to the panel
                foreach (Button button in pattern.Buttons)
                {
                    fretPatternPanel.Controls.Add(button);
                }
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



        // Method to clear the existing patterns from the UI
        private void ClearFretPatternsUI()
        {
            // Clear the UI elements that represent the fret patterns

            fretPatternPanel.Controls.Clear(); // Example for clearing a panel
        }
        private void saveFretPatternsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFretPatternsFileDialog.Filter = "Guitar Fret Patterns (*.fret)|*.fret";
            saveFretPatternsFileDialog.DefaultExt = "fret";
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
                    // MessageBox.Show("Fret patterns saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save fret patterns: {ex.Message}", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadFretPatternsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadFretPatternsDialog.Filter = "Guitar Fret Patterns (*.fret)|*.fret";
            loadFretPatternsDialog.DefaultExt = "fret";

            if (loadFretPatternsDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = loadFretPatternsDialog.FileName;
                LoadFretPatterns(fileName);
            }
        }
        private void LoadFretPatterns(string fileName)
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);

                // Deserialize the JSON to a list of FretPatternData
                List<FretPatternData> loadedPatternData = JsonSerializer.Deserialize<List<FretPatternData>>(jsonString);

                // Clear the existing UI elements
                fretPatternPanel.Controls.Clear();
                allFretPatterns.Clear();

                // Constants for layout
                int patternsPerRow = 7;
                int patternSpacing = 100;
                int patternWidth = 400; // Adjust as needed
                int patternHeight = 1500; // Adjust as needed

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
                    pattern.CreateFretboard(6, 13, pattern.BaseFret, location, i);





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

                }
                //activeFretPattern = allFretPatterns[0];
                // Optionally, refresh the panel or form if necessary
                fretPatternPanel.Refresh();

                //MessageBox.Show("Fret patterns loaded successfully.", "Load Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load fret patterns: {ex.Message}", "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateFretPatternsUI()
        {
            Color fretSelectedColor = Color.Bisque; // Color for selected frets
            Color fretNotSelectedColor = Color.Beige; // Color for not selected frets



            foreach (FretPattern pattern in allFretPatterns)
            {
                // Update the fret buttons
                foreach (Button button in pattern.Buttons)
                {
                    Tuple<int, int> tag = (Tuple<int, int>)button.Tag;
                    int stringIndex = tag.Item1;
                    int fretIndex = tag.Item2;

                    // Set the color based on whether the fret is selected or not
                    // button.BackColor = button.BackColor;


                    // Regular fret button color is based on its current color
                    button.BackColor = button.BackColor == fretSelectedColor ? fretSelectedColor : fretNotSelectedColor;
                    button.Text = pattern.GetNoteName(stringIndex, fretIndex + pattern.BaseFret);
                }
            }
        }

        private void clearFretPatternButton_Click(object sender, EventArgs e)
        {

            // Find the active FretPattern
            var activeFretPattern = allFretPatterns.FirstOrDefault(fp => fp.IsActive);

            if (activeFretPattern != null)
            {
                int patternNum = activeFretPattern.PatternNumber;
                if (patternNum > 0) patternNum -= 1;

                // Clear the active FretPattern
                allFretPatterns[patternNum].Clear();



                Debug.WriteLine("pattern number: " + patternNum);
            }


        }

        private void fretPatternModeButton_Click(object sender, EventArgs e)
        {
            int midiNoteNumber;
            int stringIndex;
            int fretIndex;

            // Assuming bisqueColor is the ARGB value for Color.Bisque
            Color bisqueColor = Color.FromArgb(255, 255, 228, 196);

            fretPatternMode = !fretPatternMode;
            fretPatternModeButton.BackColor = fretPatternMode ? Color.Red : Color.White;


            if (fretPatternMode == false)
            {
                //exiting fret pattern mode
                foreach (Button button in fretboardPanel.Controls)
                {
                    button.BackColor = SystemColors.Control;
                }
                // Determine which composition to use based on the fingering pattern mode
                var currentComposition = fingeringPatternMode ? fingerPatternComposition : composition;

                var notesAtNowTime = GetNotesAtNowTime(currentTime, currentComposition);
                HighlightFretboard(notesAtNowTime);
            }
            else if (fretPatternMode == true)
            {
                if (activeFretPattern != null)
                {
                    Debug.WriteLine("active fret pattern # " + activeFretPattern.PatternNumber);

                    baseFretForFretActivePatternNumericUpDown.Value = activeFretPattern.BaseFret;

                    foreach (Button button in fretboardPanel.Controls)
                    {
                        button.BackColor = SystemColors.Control;
                    }
                    //Debug the button tags and colors
                    foreach (var button in activeFretPattern.Buttons)
                    {
                        //Debug.WriteLine(" active pattern != null");
                        if (button.Tag is Tuple<int, int> tag && button.BackColor.ToArgb() == bisqueColor.ToArgb())
                        {
                            stringIndex = tag.Item1;
                            fretIndex = tag.Item2;
                            midiNoteNumber = GetMidiNoteNumber(stringIndex, fretIndex);
                            FindButtonByStringAndNote(stringIndex, midiNoteNumber); //turn to color bisque

                            // Debug.WriteLine($"Button for string {tag.Item1} has color {button.BackColor}" + " fretindex = " + tag.Item2);
                        }
                        else
                        {
                            // Debug.WriteLine("Button has no tag or incorrect tag format");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("No active pattern");
                    return;
                }

            }
        }

        private void fretPatternSelectionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int midiNoteNumber;
            int stringIndex;
            int fretIndex;

            // Assuming bisqueColor is the ARGB value for Color.Bisque
            Color bisqueColor = Color.FromArgb(255, 255, 228, 196);

            //clear the active fret pattern
            var activeFretPattern = allFretPatterns.FirstOrDefault(fp => fp.IsActive);
            if (activeFretPattern != null) activeFretPattern.IsActive = false;
            //activate the new one and select it
            allFretPatterns[(int)fretPatternSelectionNumericUpDown.Value].IsActive = true;
            activeFretPattern = allFretPatterns.FirstOrDefault(fp => fp.IsActive);

            baseFretForFretActivePatternNumericUpDown.Value = activeFretPattern.BaseFret;

            if (fretPatternMode == true)
            {

                foreach (Button button in fretboardPanel.Controls)
                {
                    button.BackColor = SystemColors.Control;
                }
                //Debug the button tags and colors
                foreach (var button in activeFretPattern.Buttons)
                {

                    if (button.Tag is Tuple<int, int> tag && button.BackColor.ToArgb() == bisqueColor.ToArgb())
                    {
                        stringIndex = tag.Item1;
                        fretIndex = tag.Item2;
                        midiNoteNumber = GetMidiNoteNumber(stringIndex, fretIndex);
                        FindButtonByStringAndNote(stringIndex, midiNoteNumber);//turn to color bisque

                        //Debug.WriteLine($"Button for string {tag.Item1} has color {button.BackColor}" + " fretindex = " + tag.Item2);
                    }
                    else
                    {
                        // Debug.WriteLine("Button has no tag or incorrect tag format");
                    }
                }
            }


        }


        private Button FindButtonByStringAndNote(int stringNumber, int midiNoteNumber)
        {
            foreach (Button btn in fretboardPanel.Controls)
            {
                if (btn.Tag is Tuple<int, int> tag)
                {
                    int btnMidiNoteNumber = tag.Item1;
                    int btnStringNumber = tag.Item2;
                    if (btnMidiNoteNumber == midiNoteNumber && btnStringNumber == stringNumber)
                    {
                        btn.BackColor = Color.Bisque;
                        return btn; // Button found
                    }
                }
            }
            return null; // Button not found
        }

        private void baseFretForFretActivePatternNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown baseFretUpDown = sender as NumericUpDown;
            var activeFretPattern = allFretPatterns.FirstOrDefault(fp => fp.IsActive);
            int midiNoteNumber;
            int stringIndex1;
            int fretIndex1;
            Color bisqueColor = Color.FromArgb(255, 255, 228, 196);


            if (baseFretUpDown != null && activeFretPattern != null && fretPatternMode == true)
            {
                int newBaseFret = (int)baseFretUpDown.Value;
                int fretChange = newBaseFret - activeFretPattern.BaseFret; // Calculate the change in frets

                foreach (Button button in activeFretPattern.Buttons)
                {
                    if (button.BackColor == bisqueColor) button.BackColor = Color.Beige;
                    Tuple<int, int> tag = (Tuple<int, int>)button.Tag;
                    int stringIndex = tag.Item1;
                    int fretIndex = tag.Item2 + fretChange; // Update the fret index based on the change
                    // Ensure fretIndex stays within valid range
                    fretIndex = Math.Max(0, fretIndex);
                    fretIndex = Math.Min(fretIndex, 23); // Assuming a maximum of 24 frets

                    // Update the button tag and text with the new fret index
                    button.Tag = new Tuple<int, int>(stringIndex, fretIndex);
                    Tuple<int, int> tag1 = (Tuple<int, int>)button.Tag;

                    //button.Text = activeFretPattern.GetNoteName(stringIndex, fretIndex);
                }

                activeFretPattern.BaseFret = newBaseFret; // Update the BaseFret property to the new value


                foreach (Button button in fretboardPanel.Controls)
                {
                    button.BackColor = SystemColors.Control;
                }
                // the button colors
                foreach (var button in activeFretPattern.Buttons)
                {

                    if (button.Tag is Tuple<int, int> tag && button.BackColor.ToArgb() == bisqueColor.ToArgb())
                    {
                        stringIndex1 = tag.Item1;
                        fretIndex1 = tag.Item2;
                        midiNoteNumber = GetMidiNoteNumber(stringIndex1, fretIndex1);
                        FindButtonByStringAndNote(stringIndex1, midiNoteNumber);//turn to color bisque

                        //Debug.WriteLine($"Button for string {tag.Item1} has color {button.BackColor}" + " fretindex = " + tag.Item2);
                    }
                    else
                    {
                        // Debug.WriteLine("Button has no tag or incorrect tag format");
                    }
                }


                foreach (var button in activeFretPattern.Buttons)
                {

                }
            }

        }


        private int GetMidiNoteNumber(int stringIndex, int fretIndex)
        {
            // MIDI note numbers for open strings in standard tuning
            int[] openStringMidiNotes = new int[] { 64, 59, 55, 50, 45, 40 };

            // Check if the string index is valid
            if (stringIndex < 0 || stringIndex >= openStringMidiNotes.Length)
                throw new ArgumentOutOfRangeException(nameof(stringIndex), "Invalid string index for MIDI note calculation.");

            int midiNoteNumber = openStringMidiNotes[stringIndex] + fretIndex;
            // Debug.WriteLine("note number form get midi note number: " +midiNoteNumber);

            // Calculate the MIDI note number for the given string and fret
            return openStringMidiNotes[stringIndex] + fretIndex;
        }
        private string GetNoteName(int midiNoteNumber)
        {
            string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int noteIndex = (midiNoteNumber - 12) % 12; // MIDI note number 12 is C, so we subtract 12 to align with our array
            return noteNames[noteIndex];
        }



        private void FingerPatternModeButton_Click(object sender, EventArgs e)
        {
            // Toggle the fingering pattern mode
            fingeringPatternMode = !fingeringPatternMode;
            FingerPatternModeButton.BackColor = fingeringPatternMode ? Color.Red : Color.White;

            if (fingeringPatternMode == false)
            {
                // Exiting fingering pattern mode

                PopulateFingeringPattern((int)selectedFingeringPatternNumericUpDown.Value - 1);
                // Debug.WriteLine(" 1 fingering pattern #: " + ((int)selectedFingeringPatternNumericUpDown.Value -1));
                RefreshFingeringPatternsUI();

                guitarRollPanel.Invalidate();
            }
            else
            {
                // Entering fingering pattern mode
                // Clear the guitar roll UI or set up for new pattern creation as needed
                // This might involve clearing the fingerPatternComposition or setting it to a new Composition
                fingerPatternComposition.Notes = allFingeringPatterns[(int)selectedFingeringPatternNumericUpDown.Value - 1].Notes;
                guitarRollPanel.Invalidate();

            }
        }

        private void PopulateFingeringPattern(int patternNumber)
        {
            FingeringPattern targetPattern = allFingeringPatterns[patternNumber];

            // Make a copy of the notes from fingerPatternComposition
            List<Note> notesCopy = new List<Note>(fingerPatternComposition.Notes);

            // Debug.WriteLine("before clear notes number: " + notesCopy.Count);
            targetPattern.Notes.Clear(); // Clear existing notes if necessary.

            // Now add the copied notes to the targetPattern
            targetPattern.Notes.AddRange(notesCopy);

            // Debug.WriteLine("after addrange notes number: " + targetPattern.Notes.Count);

            // Invalidate the corresponding panel to trigger a repaint.
            var panelName = $"fingeringPatternPanel{patternNumber}";
            var panel = this.Controls.Find(panelName, true).FirstOrDefault() as Panel;

            panel?.Invalidate(); // This will cause the panel's Paint event to fire.
        }


        private void fingeringPatternPanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            int panelNumber = Convert.ToInt32(panel.Tag); // Assuming each panel's Tag property is set to its corresponding pattern number
                                                          // Debug.WriteLine("paint panel number: "+panelNumber);
            float contentWidth = 0;
            // Retrieve the fingering pattern for this panel
            FingeringPattern fingeringPattern = allFingeringPatterns[panelNumber]; // Adjusted to use the list

            int numberOfStrings = 6;
            int laneHeight = (panel.Height - 50) / numberOfStrings;

            // Draw the strings
            for (int i = 0; i <= numberOfStrings; i++)
            {
                e.Graphics.DrawLine(Pens.Gray, 0, i * laneHeight, panel.Width, i * laneHeight);
            }


            // Calculate beat and bar durations in pixels
            float beatPixelSpacing = CalculateBeatDuration() * PixelsPerSecond;
            float barPixelSpacing = CalculateBarDuration() * PixelsPerSecond;

            // The starting position is always at the beginning of the panel since there's no scroll
            float startX = 0;

            // Create pens outside the loop for better performance
            using (Pen barPen = new Pen(Color.Black, 6.0f))
            using (Pen beatPen = new Pen(Color.Gray, 1.0f))

            {
                // Consider the entire content width of the panel
                contentWidth = panel.Width;

                for (float x = startX; x < contentWidth; x += beatPixelSpacing)
                {
                    if (x % barPixelSpacing == 0)
                    {
                        // Draw a thicker line for the bar
                        e.Graphics.DrawLine(barPen, x, 0, x, panel.Height - 50);
                    }
                    else
                    {
                        // Draw a regular line for the beat
                        e.Graphics.DrawLine(beatPen, x, 0, x, panel.Height - 50);
                    }
                }

            }

            // Check if there are notes in the fingering pattern
            if (fingeringPattern.Notes.Any())
            {
                // Draw the notes
                foreach (Note note in fingeringPattern.Notes)
                {
                    DrawNoteOnFingeringPattern(note, e.Graphics, panel);
                }
            }
            else
            {
                // Optionally, draw a placeholder or message indicating the pattern is empty
                e.Graphics.DrawString("Empty Pattern: " + panelNumber, new Font("Arial", 12), Brushes.Gray, new PointF(panel.Width / 2 - 50, panel.Height / 2));
            }

            // ... Additional drawing logic for beats and bars, if necessary
        }


        // Implement this method to retrieve the correct fingering pattern based on the panel number
        private FingeringPattern GetFingeringPatternByNumber(int patternNumber)
        {
            // Logic to retrieve the correct FingeringPattern instance
            // This could be from an array, list, or other collection that holds the patterns
            // For example:
            return allFingeringPatterns[patternNumber - 1];
        }
        private void DrawNoteOnFingeringPattern(Note note, Graphics g, Panel panel)
        {
            int laneHeight = (panel.Height - 50) / 6;
            int noteStartX = (int)(note.StartTime * PixelsPerSecond) + panel.AutoScrollPosition.X;
            int noteWidth = (int)((note.EndTime - note.StartTime) * PixelsPerSecond);
            // Debug.WriteLine("DrawNoteOnFingeringPattern ");

            // Assuming velocity affects the height of the note rectangle.
            // You might want to define a maximum height for the note rectangle.
            //int maxNoteHeight = laneHeight - 2; // For example, leaving a small gap between lanes
            //int noteHeight = Math.Min((int)(note.Velocity / 127.0 * maxNoteHeight), maxNoteHeight);

            // Calculate note height based on velocity
            int minNoteHeight = 70; // Minimum height so the note doesn't disappear
            int maxNoteHeight = laneHeight; // Maximum height
            int noteHeight = (int)(laneHeight * (note.Velocity / 127.0)); // Adjust height based on velocity



            // Calculate the Y position of the note. Assuming string numbers are 0-indexed.
            int noteY = (note.StringNumber) * laneHeight + (laneHeight - noteHeight);

            // Create a rectangle for the note and draw it
            Rectangle noteRect = new Rectangle(noteStartX, noteY, noteWidth, noteHeight);

            // Use a brush to fill the rectangle. You might choose the color based on the note properties.
            Brush noteBrush = new SolidBrush(Color.FromArgb(note.Velocity * 2, 0, 0)); // Example: darker color for higher velocity
            g.FillRectangle(noteBrush, noteRect);
            g.DrawRectangle(Pens.Black, noteRect);

            // Additional drawing logic for the note text, if needed
            // For example, drawing the note name or number inside the rectangle
            // string noteText = note.Name; // Replace with actual property if different
            // Font noteFont = new Font("Arial", 8);
            //  SizeF textSize = g.MeasureString(noteText, noteFont);
            //  PointF textPosition = new PointF(noteStartX + (noteWidth - textSize.Width) / 2, noteY + (noteHeight - textSize.Height) / 2);
            // g.DrawString(noteText, noteFont, Brushes.White, textPosition);
        }



        private void saveFingeringPatternsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFingeringPatternsFileDialog.Filter = "Fingering Patterns (*.fing)|*.fing";
            saveFingeringPatternsFileDialog.DefaultExt = "fing";
            if (saveFingeringPatternsFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFingeringPatterns(saveFingeringPatternsFileDialog.FileName);
            }
        }

        public void SaveFingeringPatterns(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(allFingeringPatterns, options);
            File.WriteAllText(filePath, jsonString);
        }


        private void loadFingeringPatternsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFingeringPatternsFileDialog.Filter = "Fingering Patterns (*.fing)|*.fing";
            if (openFingeringPatternsFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFingeringPatterns(openFingeringPatternsFileDialog.FileName);
                RefreshFingeringPatternsUI();
            }
        }

        public void LoadFingeringPatterns(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            allFingeringPatterns = JsonSerializer.Deserialize<List<FingeringPattern>>(jsonString);
        }

        private void RefreshFingeringPatternsUI()
        {
            // Invalidate each fingering pattern panel to trigger a repaint
            for (int i = 0; i < allFingeringPatterns.Count; i++)
            {
                var panel = this.Controls.Find("fingeringPatternPanel" + (i), true).FirstOrDefault() as Panel;
                panel?.Invalidate();
            }
        }
        private void selectedFingeringPatternNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Update the selected fingering pattern index based on the NumericUpDown value
            activeFingeringPattern = (int)selectedFingeringPatternNumericUpDown.Value - 1;

            fingerPatternComposition.Notes = allFingeringPatterns[(int)selectedFingeringPatternNumericUpDown.Value - 1].Notes;
            guitarRollPanel.Invalidate();
        }

        private void addFingerinPatternToCompositionButton_Click(object sender, EventArgs e)
        {
            // Step 1: Get the active fingering pattern
            int activePatternIndex = (int)selectedFingeringPatternNumericUpDown.Value - 1; // Assuming the numeric up down is 1-indexed
            var activeFingeringPattern = allFingeringPatterns[activePatternIndex];
            float lastNoteEndTime = 0;
            if (composition.Notes.Count > 0)
            {
                lastNoteEndTime = (float)composition.Notes.Max(note => note.EndTime);
            }

            // Step 2: Identify the active fret pattern
            var activeFretPattern = allFretPatterns.FirstOrDefault(fp => fp.IsActive);

            // Assuming bisqueColor is the ARGB value for Color.Bisque
            Color bisqueColor = Color.FromArgb(255, 255, 228, 196);

            if (activeFretPattern == null)
            {
                // Handle the case where there is no active fret pattern
                MessageBox.Show("No active fret pattern found.");
                return;
            }
            //Debug the button tags and colors
            int isSelected = 0;
            foreach (var button in activeFretPattern.Buttons)
            {

                if (button.Tag is Tuple<int, int> tag)
                {
                    if (button.BackColor == bisqueColor) { isSelected++; }
                    Debug.WriteLine($"Button for string {tag.Item1} has color {button.BackColor}" + " fretindex = " + tag.Item2);
                }
                else
                {
                    Debug.WriteLine("Button has no tag or incorrect tag format");
                }
            }
            if (isSelected == 0)
            {
                // Handle the case where there is nothing in the active fret pattern
                MessageBox.Show(" Active fret pattern has no notes.");
                return;
            }

            // Step 3: Loop through the notes in the active fingering pattern
            foreach (var note in activeFingeringPattern.Notes)
            {
                // Find the corresponding button in the fret pattern based on the string number
                //Debug.WriteLine($"Looking for fret button for note on string {note.StringNumber}");
                // Find the end time of the last note in the composition


                var fretButton = activeFretPattern.Buttons.FirstOrDefault(button =>
                    button.Tag is Tuple<int, int> tag &&
                    tag.Item1 == note.StringNumber &&
                    button.BackColor.ToArgb() == bisqueColor.ToArgb());

                if (fretButton == null)
                {
                    // Debug.WriteLine($"No active fret button found for string {note.StringNumber}");
                }
                else
                {


                    //  Debug.WriteLine("Fingering pattern note found and fret button is active");
                    // Extract the fretIndex from the button's Tag
                    int fretIndex = ((Tuple<int, int>)fretButton.Tag).Item2;

                    // Get the MIDI note number from the fret pattern (you'll need to implement GetMidiNoteNumber)
                    int midiNoteNumber = GetMidiNoteNumber(note.StringNumber, fretIndex);
                    //Debug.WriteLine("String number: "+ note.StringNumber + "fret number: "+fretIndex);

                    // Step 4: Merge the information to create a new Note object
                    Note newNote = new Note
                    {
                        StartTime = currentTime + note.StartTime,
                        EndTime = currentTime + note.EndTime,
                        Velocity = note.Velocity,
                        MidiChannel = note.MidiChannel,
                        StringNumber = note.StringNumber,
                        MidiNoteNumber = midiNoteNumber


                    };



                    // Step 5: Add the new Note to the composition
                    composition.Notes.Add(newNote);



                    using (Graphics g = guitarRollPanel.CreateGraphics())
                    {
                        DrawNoteOnGuitarRoll(newNote, g);
                    }

                }
            }
            // After adding notes, update currentTime to the end of the last note
            currentTime = (float)composition.Notes.Max(note => note.EndTime);

            // Refresh the UI to reflect the new note in the composition
            guitarRollPanel.Invalidate();
        }


        private void clearFingeringPatternButton_Click(object sender, EventArgs e)
        {
            allFingeringPatterns[(int)selectedFingeringPatternNumericUpDown.Value - 1].Notes.Clear();
            RefreshFingeringPatternsUI();
            guitarRollPanel.Invalidate();

        }

        private void editFingeringPatternButton_Click(object sender, EventArgs e)
        {

        }

        private void clearCompositionButton_Click(object sender, EventArgs e)
        {
            composition.Notes.Clear();
            currentTime = 0;
            guitarRollPanel.Invalidate();
        }

        private void staccatoButton_Click(object sender, EventArgs e)
        {
            staccatoMode = !staccatoMode;
            if (staccatoMode)
            {
                staccatoButton.BackColor = Color.Crimson;
            }
            else
            {
                staccatoButton.BackColor = SystemColors.Control;
            }
        }


        private float[] LoadWaveFile(string fileName)
        {
            using (var reader = new NAudio.Wave.AudioFileReader(fileName))
            {
                // Calculate the total number of samples in the file
                int totalSamples = (int)(reader.Length / (reader.WaveFormat.BitsPerSample / 8));

                // Initialize the buffer to the total number of samples
                var buffer = new float[totalSamples];

                // Read the entire file into the buffer
                int read = reader.Read(buffer, 0, buffer.Length);

                // If the read samples are less than the buffer size, resize the array
                if (read < totalSamples)
                {
                    Array.Resize(ref buffer, read);
                }

                return buffer;
            }
        }










        private void tabControl1_Click(object sender, EventArgs e)
        {
            fretPatternPanel.Refresh();
        }

        private void equalizerActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            Label[] frequencyLabels = {
            EQBand1FrequencyLabel,
            EQBand2FrequencyLabel,
            EQBand3FrequencyLabel,
            EQBand4FrequencyLabel,
            EQBand5FrequencyLabel,
            EQBand6FrequencyLabel,
            EQBand7FrequencyLabel,
            EQBand8FrequencyLabel
            };

            Label[] widthLabels = {
            EQBand1WidthLabel,
            EQBand2WidthLabel,
            EQBand3WidthLabel,
            EQBand4WidthLabel ,
            EQBand5WidthLabel,
            EQBand6WidthLabel,
            EQBand7WidthLabel,
            EQBand8WidthLabel};

            Label[] gainLabels = {
            EQBand1GainLabel,
            EQBand2GainLabel,
            EQBand3GainLabel,
            EQBand4GainLabel,
            EQBand5GainLabel,
            EQBand6GainLabel,
            EQBand7GainLabel,
            EQBand8GainLabel,};

            TrackBar[] frequencies = {

                EQBand1FrequencySlider,
                EQBand2FrequencySlider,
                EQBand3FrequencySlider,
                EQBand4FrequencySlider,
                EQBand5FrequencySlider,
                EQBand6FrequencySlider,
                EQBand7FrequencySlider,
                EQBand8FrequencySlider};

            TrackBar[] gains =
            {
                EQBand1GainSlider,
                EQBand2GainSlider,
                EQBand3GainSlider,
                EQBand4GainSlider,
                EQBand5GainSlider,
                EQBand6GainSlider,
                EQBand7GainSlider,
                EQBand8GainSlider};

            TrackBar[] widths =
            {
                EQBand1WidthSlider,
                EQBand2WidthSlider,
                EQBand3WidthSlider,
                EQBand4WidthSlider,
                EQBand5WidthSlider,
                EQBand6WidthSlider,
                EQBand7WidthSlider,
                EQBand8WidthSlider};

            //******************* set the activation for the EQ for that string!!!!!!!!!!!!!!!
            theGuitar.strings[selectedStringIndex].eqActive = equalizerActiveCheckBox.Checked;


                for (int i = 0; i < 8; i++)
                {
                   frequencies[i].Value =  theGuitar.strings[selectedStringIndex].bands[i].Frequency ;
                   frequencyLabels[i].Text = frequencies[i].Value.ToString();
                   gains[i].Value = (int) theGuitar.strings[selectedStringIndex].bands[i].Gain ;
                   gainLabels[i].Text = gains[i].Value.ToString() ;
                float newvalue = theGuitar.strings[selectedStringIndex].bands[i].Bandwidth * 10;
                widths[i].Value = (int) newvalue;
                   // Debug.WriteLine("eq width band"  + i + ": " + theGuitar.strings[selectedStringIndex].bands[i].Bandwidth);
                widthLabels[i].Text = theGuitar.strings[selectedStringIndex].bands[i].Bandwidth.ToString() ;
                }
            
        }

        private void EQBand1FrequencySlider_Scroll(object sender, EventArgs e)
        {

            theGuitar.strings[selectedStringIndex].bands[0].Frequency = EQBand1FrequencySlider.Value;
            EQBand1FrequencyLabel.Text = EQBand1FrequencySlider.Value.ToString();
        }

        private void EQBand2FrequencySlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[1].Frequency = EQBand2FrequencySlider.Value;
            EQBand2FrequencyLabel.Text = EQBand2FrequencySlider.Value.ToString();
        }

        private void EQBand3FrequencySlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[2].Frequency = EQBand3FrequencySlider.Value;
            EQBand3FrequencyLabel.Text = EQBand3FrequencySlider.Value.ToString();
        }

        private void EQBand4FrequencySlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[3].Frequency = EQBand4FrequencySlider.Value;
            EQBand4FrequencyLabel.Text = EQBand4FrequencySlider.Value.ToString();
        }

        private void EQBand5FrequencySlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[4].Frequency = EQBand5FrequencySlider.Value;
            EQBand5FrequencyLabel.Text = EQBand5FrequencySlider.Value.ToString();
        }

        private void EQBand6FrequencySlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[5].Frequency = EQBand6FrequencySlider.Value;
            EQBand6FrequencyLabel.Text = EQBand6FrequencySlider.Value.ToString();
        }

        private void EQBand7FrequencySlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[6].Frequency = EQBand7FrequencySlider.Value;
            EQBand7FrequencyLabel.Text = EQBand7FrequencySlider.Value.ToString();
        }

        private void EQBand8FrequencySlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[7].Frequency = EQBand8FrequencySlider.Value;
            EQBand8FrequencyLabel.Text = EQBand8FrequencySlider.Value.ToString();
        }

        private void EQBand1WidthSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[0].Bandwidth = EQBand1WidthSlider.Value/10f;
            EQBand1WidthLabel.Text = theGuitar.strings[selectedStringIndex].bands[0].Bandwidth.ToString();
            Debug.WriteLine("eq width band 1 : " + theGuitar.strings[selectedStringIndex].bands[0].Bandwidth);
        }

        private void EQBand2WidthSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[1].Bandwidth = EQBand2WidthSlider.Value/10f;
            EQBand2WidthLabel.Text = theGuitar.strings[selectedStringIndex].bands[1].Bandwidth.ToString();
        }

        private void EQBand3WidthSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[2].Bandwidth = EQBand3WidthSlider.Value/10f;
            EQBand3WidthLabel.Text = theGuitar.strings[selectedStringIndex].bands[2].Bandwidth.ToString();
        }

        private void EQBand4WidthSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[3].Bandwidth = EQBand4WidthSlider.Value/10f;
            EQBand4WidthLabel.Text = theGuitar.strings[selectedStringIndex].bands[3].Bandwidth.ToString();
        }

        private void EQBand5WidthSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[4].Bandwidth = EQBand5WidthSlider.Value / 10f;
            EQBand5WidthLabel.Text = theGuitar.strings[selectedStringIndex].bands[4].Bandwidth.ToString();
        }

        private void EQBand6WidthSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[5].Bandwidth = EQBand6WidthSlider.Value / 10f;
            EQBand6WidthLabel.Text = theGuitar.strings[selectedStringIndex].bands[5].Bandwidth.ToString();
        }

        private void EQBand7WidthSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[6].Bandwidth = EQBand7WidthSlider.Value / 10f;
            EQBand7WidthLabel.Text = theGuitar.strings[selectedStringIndex].bands[6].Bandwidth.ToString();
        }

        private void EQBand8WidthSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[7].Bandwidth = EQBand8WidthSlider.Value / 10f;
            EQBand8WidthLabel.Text = theGuitar.strings[selectedStringIndex].bands[7].Bandwidth.ToString();
        }

        private void EQBand1GainSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[0].Gain = EQBand1GainSlider.Value;
            EQBand1GainLabel.Text = EQBand1GainSlider.Value.ToString();
        }

        private void EQBand2GainSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[1].Gain = EQBand2GainSlider.Value;
            EQBand2GainLabel.Text = EQBand2GainSlider.Value.ToString();
        }

        private void EQBand3GainSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[2].Gain = EQBand3GainSlider.Value;
            EQBand3GainLabel.Text = EQBand3GainSlider.Value.ToString();
        }

        private void EQBand4GainSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[3].Gain = EQBand4GainSlider.Value;
            EQBand4GainLabel.Text = EQBand4GainSlider.Value.ToString();
        }

        private void EQBand5GainSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[4].Gain = EQBand5GainSlider.Value;
            EQBand5GainLabel.Text = EQBand5GainSlider.Value.ToString();
        }

        private void EQBand6GainSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[5].Gain = EQBand6GainSlider.Value;
            EQBand6GainLabel.Text = EQBand6GainSlider.Value.ToString();
        }

        private void EQBand7GainSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[6].Gain = EQBand7GainSlider.Value;
            EQBand7GainLabel.Text = EQBand7GainSlider.Value.ToString();
        }

        private void EQBand8GainSlider_Scroll(object sender, EventArgs e)
        {
            theGuitar.strings[selectedStringIndex].bands[7].Gain = EQBand8GainSlider.Value;
            EQBand8GainLabel.Text = EQBand8GainSlider.Value.ToString();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
        public static WaveFormat GlobalWaveFormat { get; } = WaveFormat.CreateIeeeFloatWaveFormat(48000, 2);

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

    [Serializable]
    public class AllDataContainer
    {
        public Form1Data Form1Data { get; set; }
        public List<FretPatternData> FretPatternData { get; set; }
        public List<FingeringPattern> FingeringPatterns { get; set; }

        // Constructor
        public AllDataContainer()
        {
            FretPatternData = new List<FretPatternData>();
            FingeringPatterns = new List<FingeringPattern>();
        }
    }

}




