namespace Guitarsharp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonPlayMidi = new Button();
            lowPassFilterAlpha = new TrackBar();
            label1 = new Label();
            EnvelopeLengthSlider = new TrackBar();
            label2 = new Label();
            fretboardPanel = new Panel();
            guitarRollPanel = new Panel();
            panel1 = new Panel();
            rondeButton = new Button();
            label3 = new Label();
            noteDuration = new Panel();
            label9 = new Label();
            setptatuplePerBeatButton = new Button();
            sexatuplePerBeatButton = new Button();
            quintuplePerBeatButton = new Button();
            triplePerBeatButton = new Button();
            tripletButton = new Button();
            pointeeButton = new Button();
            tripleCrocheButton = new Button();
            doubleCrocheButton = new Button();
            crocheButton = new Button();
            noireButton = new Button();
            blancheButton = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exportAudioToolStripMenuItem = new ToolStripMenuItem();
            exportMIDIToolStripMenuItem = new ToolStripMenuItem();
            saveFretPatternsToolStripMenuItem = new ToolStripMenuItem();
            loadFretPatternsToolStripMenuItem = new ToolStripMenuItem();
            saveFingeringPatternsToolStripMenuItem = new ToolStripMenuItem();
            loadFingeringPatternsToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            timeSignatureNumeratorNumericUpDown = new NumericUpDown();
            timeSignatureDenominatorNumericUpDown = new NumericUpDown();
            label4 = new Label();
            tempoNumericUpDown = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            stringOneMidiChannelUpDown = new NumericUpDown();
            stringTwoMidiChannelUpDown = new NumericUpDown();
            stringThreeMidiChannelUpDown = new NumericUpDown();
            stringFourMidiChannelUpDown = new NumericUpDown();
            stringFiveMidiChannelUpDown = new NumericUpDown();
            stringSixMidiChannelUpDown = new NumericUpDown();
            velocitySlider = new TrackBar();
            label7 = new Label();
            startPlayingButton = new Button();
            StopPlayingButton = new Button();
            openAudioFileDialog = new OpenFileDialog();
            exportAudioFileDialog = new SaveFileDialog();
            exportMidiSaveFileDialog = new SaveFileDialog();
            fretPatternPanel = new Panel();
            labelfretpattern = new Label();
            FingerPatternModeButton = new Button();
            saveFretPatternsFileDialog = new SaveFileDialog();
            loadFretPatternsDialog = new OpenFileDialog();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            nextNoteButton = new Button();
            deleteSelectedNoteButton = new Button();
            clearCompositionButton = new Button();
            editFingeringPatternButton = new Button();
            clearFingeringPatternButton = new Button();
            label8 = new Label();
            addFingerinPatternToCompositionButton = new Button();
            selectedFingeringPatternNumericUpDown = new NumericUpDown();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            fingeringPatternpanel5 = new Panel();
            fingeringPatternpanel4 = new Panel();
            fingeringPatternPanel3 = new Panel();
            fingeringPatternPanel2 = new Panel();
            fingeringPatternPanel1 = new Panel();
            openFingeringPatternsFileDialog = new OpenFileDialog();
            saveFingeringPatternsFileDialog = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)lowPassFilterAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EnvelopeLengthSlider).BeginInit();
            panel1.SuspendLayout();
            noteDuration.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)timeSignatureNumeratorNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)timeSignatureDenominatorNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tempoNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stringOneMidiChannelUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stringTwoMidiChannelUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stringThreeMidiChannelUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stringFourMidiChannelUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stringFiveMidiChannelUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stringSixMidiChannelUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)velocitySlider).BeginInit();
            fretPatternPanel.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)selectedFingeringPatternNumericUpDown).BeginInit();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // buttonPlayMidi
            // 
            buttonPlayMidi.Location = new Point(928, 56);
            buttonPlayMidi.Name = "buttonPlayMidi";
            buttonPlayMidi.Size = new Size(169, 50);
            buttonPlayMidi.TabIndex = 0;
            buttonPlayMidi.Text = "play MIDI";
            buttonPlayMidi.UseVisualStyleBackColor = true;
            buttonPlayMidi.Click += buttonPlayMidi_Click;
            // 
            // lowPassFilterAlpha
            // 
            lowPassFilterAlpha.Location = new Point(41, 345);
            lowPassFilterAlpha.Maximum = 100;
            lowPassFilterAlpha.Minimum = 1;
            lowPassFilterAlpha.Name = "lowPassFilterAlpha";
            lowPassFilterAlpha.Size = new Size(628, 114);
            lowPassFilterAlpha.TabIndex = 1;
            lowPassFilterAlpha.Value = 1;
            lowPassFilterAlpha.ValueChanged += lowPassFilterAlpha_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 281);
            label1.Name = "label1";
            label1.Size = new Size(302, 41);
            label1.TabIndex = 2;
            label1.Text = "Low Pass Filter Alpha ";
            // 
            // EnvelopeLengthSlider
            // 
            EnvelopeLengthSlider.Location = new Point(707, 345);
            EnvelopeLengthSlider.Maximum = 100;
            EnvelopeLengthSlider.Minimum = 1;
            EnvelopeLengthSlider.Name = "EnvelopeLengthSlider";
            EnvelopeLengthSlider.Size = new Size(612, 114);
            EnvelopeLengthSlider.TabIndex = 3;
            EnvelopeLengthSlider.Value = 1;
            EnvelopeLengthSlider.ValueChanged += EnvelopeLengthSlider_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(701, 281);
            label2.Name = "label2";
            label2.Size = new Size(233, 41);
            label2.TabIndex = 4;
            label2.Text = "envelope length";
            // 
            // fretboardPanel
            // 
            fretboardPanel.BackColor = SystemColors.AppWorkspace;
            fretboardPanel.BorderStyle = BorderStyle.FixedSingle;
            fretboardPanel.Location = new Point(160, 593);
            fretboardPanel.Name = "fretboardPanel";
            fretboardPanel.Size = new Size(2400, 387);
            fretboardPanel.TabIndex = 5;
            fretboardPanel.Tag = "80";
            // 
            // guitarRollPanel
            // 
            guitarRollPanel.AutoScroll = true;
            guitarRollPanel.AutoScrollMinSize = new Size(5000, 600);
            guitarRollPanel.BackColor = Color.White;
            guitarRollPanel.Location = new Point(38, 33);
            guitarRollPanel.Name = "guitarRollPanel";
            guitarRollPanel.Size = new Size(1808, 722);
            guitarRollPanel.TabIndex = 6;
            guitarRollPanel.Scroll += guitarRollPanel_Scroll;
            guitarRollPanel.Paint += guitarRollPanel_Paint;
            guitarRollPanel.MouseClick += guitarRollPanel_MouseClick;
            guitarRollPanel.MouseDown += guitarRollPanel_MouseDown;
            guitarRollPanel.MouseMove += guitarRollPanel_MouseMove;
            guitarRollPanel.MouseUp += guitarRollPanel_MouseUp;
            // 
            // panel1
            // 
            panel1.Controls.Add(guitarRollPanel);
            panel1.Location = new Point(108, 1014);
            panel1.Name = "panel1";
            panel1.Size = new Size(1849, 769);
            panel1.TabIndex = 8;
            // 
            // rondeButton
            // 
            rondeButton.Location = new Point(3, 44);
            rondeButton.Name = "rondeButton";
            rondeButton.Size = new Size(169, 50);
            rondeButton.TabIndex = 9;
            rondeButton.Text = "Whole";
            rondeButton.UseVisualStyleBackColor = true;
            rondeButton.Click += rondeButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(199, 41);
            label3.TabIndex = 10;
            label3.Text = "note duration";
            // 
            // noteDuration
            // 
            noteDuration.BackColor = SystemColors.ActiveCaption;
            noteDuration.Controls.Add(label9);
            noteDuration.Controls.Add(setptatuplePerBeatButton);
            noteDuration.Controls.Add(sexatuplePerBeatButton);
            noteDuration.Controls.Add(quintuplePerBeatButton);
            noteDuration.Controls.Add(triplePerBeatButton);
            noteDuration.Controls.Add(tripletButton);
            noteDuration.Controls.Add(pointeeButton);
            noteDuration.Controls.Add(tripleCrocheButton);
            noteDuration.Controls.Add(doubleCrocheButton);
            noteDuration.Controls.Add(crocheButton);
            noteDuration.Controls.Add(noireButton);
            noteDuration.Controls.Add(blancheButton);
            noteDuration.Controls.Add(rondeButton);
            noteDuration.Controls.Add(label3);
            noteDuration.Location = new Point(159, 1859);
            noteDuration.Name = "noteDuration";
            noteDuration.Size = new Size(1542, 279);
            noteDuration.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 105);
            label9.Name = "label9";
            label9.Size = new Size(1109, 41);
            label9.TabIndex = 22;
            label9.Text = "Make sure you have a note duration first and when finished click again  to deselect";
            // 
            // setptatuplePerBeatButton
            // 
            setptatuplePerBeatButton.Location = new Point(614, 159);
            setptatuplePerBeatButton.Name = "setptatuplePerBeatButton";
            setptatuplePerBeatButton.Size = new Size(188, 58);
            setptatuplePerBeatButton.TabIndex = 21;
            setptatuplePerBeatButton.Text = "septatuple";
            setptatuplePerBeatButton.UseVisualStyleBackColor = true;
            setptatuplePerBeatButton.Click += setptatuplePerBeatButton_Click;
            // 
            // sexatuplePerBeatButton
            // 
            sexatuplePerBeatButton.Location = new Point(413, 159);
            sexatuplePerBeatButton.Name = "sexatuplePerBeatButton";
            sexatuplePerBeatButton.Size = new Size(188, 58);
            sexatuplePerBeatButton.TabIndex = 20;
            sexatuplePerBeatButton.Text = "sextuple";
            sexatuplePerBeatButton.UseVisualStyleBackColor = true;
            sexatuplePerBeatButton.Click += sexatuplePerBeatButton_Click;
            // 
            // quintuplePerBeatButton
            // 
            quintuplePerBeatButton.Location = new Point(209, 159);
            quintuplePerBeatButton.Name = "quintuplePerBeatButton";
            quintuplePerBeatButton.Size = new Size(188, 58);
            quintuplePerBeatButton.TabIndex = 19;
            quintuplePerBeatButton.Text = "quintuple";
            quintuplePerBeatButton.UseVisualStyleBackColor = true;
            quintuplePerBeatButton.Click += quintuplePerBeatButton_Click;
            // 
            // triplePerBeatButton
            // 
            triplePerBeatButton.Location = new Point(3, 159);
            triplePerBeatButton.Name = "triplePerBeatButton";
            triplePerBeatButton.Size = new Size(188, 58);
            triplePerBeatButton.TabIndex = 18;
            triplePerBeatButton.Text = "triple";
            triplePerBeatButton.UseVisualStyleBackColor = true;
            triplePerBeatButton.Click += triplePerBeatButton_Click;
            // 
            // tripletButton
            // 
            tripletButton.Location = new Point(1325, 44);
            tripletButton.Name = "tripletButton";
            tripletButton.Size = new Size(169, 50);
            tripletButton.TabIndex = 17;
            tripletButton.Text = "triplet";
            tripletButton.UseVisualStyleBackColor = true;
            tripletButton.Click += tripletButton_Click;
            // 
            // pointeeButton
            // 
            pointeeButton.Location = new Point(1130, 44);
            pointeeButton.Name = "pointeeButton";
            pointeeButton.Size = new Size(169, 50);
            pointeeButton.TabIndex = 16;
            pointeeButton.Text = "dotted";
            pointeeButton.UseVisualStyleBackColor = true;
            pointeeButton.Click += pointeeButton_Click;
            // 
            // tripleCrocheButton
            // 
            tripleCrocheButton.Location = new Point(939, 44);
            tripleCrocheButton.Name = "tripleCrocheButton";
            tripleCrocheButton.Size = new Size(169, 50);
            tripleCrocheButton.TabIndex = 15;
            tripleCrocheButton.Text = "32nd";
            tripleCrocheButton.UseVisualStyleBackColor = true;
            tripleCrocheButton.Click += tripleCrocheButton_Click;
            // 
            // doubleCrocheButton
            // 
            doubleCrocheButton.Location = new Point(752, 44);
            doubleCrocheButton.Name = "doubleCrocheButton";
            doubleCrocheButton.Size = new Size(169, 50);
            doubleCrocheButton.TabIndex = 14;
            doubleCrocheButton.Text = "sixteen";
            doubleCrocheButton.UseVisualStyleBackColor = true;
            doubleCrocheButton.Click += doubleCrocheButton_Click;
            // 
            // crocheButton
            // 
            crocheButton.Location = new Point(568, 44);
            crocheButton.Name = "crocheButton";
            crocheButton.Size = new Size(169, 50);
            crocheButton.TabIndex = 13;
            crocheButton.Text = "eigth";
            crocheButton.UseVisualStyleBackColor = true;
            crocheButton.Click += crocheButton_Click;
            // 
            // noireButton
            // 
            noireButton.Location = new Point(369, 44);
            noireButton.Name = "noireButton";
            noireButton.Size = new Size(169, 50);
            noireButton.TabIndex = 12;
            noireButton.Text = "quarter";
            noireButton.UseVisualStyleBackColor = true;
            noireButton.Click += noireButton_Click;
            // 
            // blancheButton
            // 
            blancheButton.Location = new Point(188, 44);
            blancheButton.Name = "blancheButton";
            blancheButton.Size = new Size(169, 50);
            blancheButton.TabIndex = 11;
            blancheButton.Text = "Half";
            blancheButton.UseVisualStyleBackColor = true;
            blancheButton.Click += blancheButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(40, 40);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 1, 0, 1);
            menuStrip1.Size = new Size(3366, 47);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, exportAudioToolStripMenuItem, exportMIDIToolStripMenuItem, saveFretPatternsToolStripMenuItem, loadFretPatternsToolStripMenuItem, saveFingeringPatternsToolStripMenuItem, loadFingeringPatternsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(87, 45);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(497, 54);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(497, 54);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // exportAudioToolStripMenuItem
            // 
            exportAudioToolStripMenuItem.Name = "exportAudioToolStripMenuItem";
            exportAudioToolStripMenuItem.Size = new Size(497, 54);
            exportAudioToolStripMenuItem.Text = "export audio";
            exportAudioToolStripMenuItem.Click += exportAudioToolStripMenuItem_Click;
            // 
            // exportMIDIToolStripMenuItem
            // 
            exportMIDIToolStripMenuItem.Name = "exportMIDIToolStripMenuItem";
            exportMIDIToolStripMenuItem.Size = new Size(497, 54);
            exportMIDIToolStripMenuItem.Text = "export MIDI";
            exportMIDIToolStripMenuItem.Click += exportMIDIToolStripMenuItem_Click;
            // 
            // saveFretPatternsToolStripMenuItem
            // 
            saveFretPatternsToolStripMenuItem.Name = "saveFretPatternsToolStripMenuItem";
            saveFretPatternsToolStripMenuItem.Size = new Size(497, 54);
            saveFretPatternsToolStripMenuItem.Text = "Save Fret Patterns";
            saveFretPatternsToolStripMenuItem.Click += saveFretPatternsToolStripMenuItem_Click;
            // 
            // loadFretPatternsToolStripMenuItem
            // 
            loadFretPatternsToolStripMenuItem.Name = "loadFretPatternsToolStripMenuItem";
            loadFretPatternsToolStripMenuItem.Size = new Size(497, 54);
            loadFretPatternsToolStripMenuItem.Text = "Load Fret Patterns";
            loadFretPatternsToolStripMenuItem.Click += loadFretPatternsToolStripMenuItem_Click;
            // 
            // saveFingeringPatternsToolStripMenuItem
            // 
            saveFingeringPatternsToolStripMenuItem.Name = "saveFingeringPatternsToolStripMenuItem";
            saveFingeringPatternsToolStripMenuItem.Size = new Size(497, 54);
            saveFingeringPatternsToolStripMenuItem.Text = "save Fingering Patterns";
            saveFingeringPatternsToolStripMenuItem.Click += saveFingeringPatternsToolStripMenuItem_Click;
            // 
            // loadFingeringPatternsToolStripMenuItem
            // 
            loadFingeringPatternsToolStripMenuItem.Name = "loadFingeringPatternsToolStripMenuItem";
            loadFingeringPatternsToolStripMenuItem.Size = new Size(497, 54);
            loadFingeringPatternsToolStripMenuItem.Text = "Load Fingering Patterns";
            loadFingeringPatternsToolStripMenuItem.Click += loadFingeringPatternsToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // timeSignatureNumeratorNumericUpDown
            // 
            timeSignatureNumeratorNumericUpDown.Location = new Point(41, 106);
            timeSignatureNumeratorNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            timeSignatureNumeratorNumericUpDown.Name = "timeSignatureNumeratorNumericUpDown";
            timeSignatureNumeratorNumericUpDown.Size = new Size(89, 47);
            timeSignatureNumeratorNumericUpDown.TabIndex = 13;
            timeSignatureNumeratorNumericUpDown.Value = new decimal(new int[] { 5, 0, 0, 0 });
            timeSignatureNumeratorNumericUpDown.ValueChanged += timeSignatureNumeratorNumericUpDown_ValueChanged;
            // 
            // timeSignatureDenominatorNumericUpDown
            // 
            timeSignatureDenominatorNumericUpDown.Location = new Point(41, 159);
            timeSignatureDenominatorNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            timeSignatureDenominatorNumericUpDown.Name = "timeSignatureDenominatorNumericUpDown";
            timeSignatureDenominatorNumericUpDown.Size = new Size(89, 47);
            timeSignatureDenominatorNumericUpDown.TabIndex = 14;
            timeSignatureDenominatorNumericUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });
            timeSignatureDenominatorNumericUpDown.ValueChanged += timeSignatureDenominatorNumericUpDown_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(41, 62);
            label4.Name = "label4";
            label4.Size = new Size(208, 41);
            label4.TabIndex = 15;
            label4.Text = "time signature";
            // 
            // tempoNumericUpDown
            // 
            tempoNumericUpDown.Location = new Point(177, 149);
            tempoNumericUpDown.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            tempoNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            tempoNumericUpDown.Name = "tempoNumericUpDown";
            tempoNumericUpDown.Size = new Size(183, 47);
            tempoNumericUpDown.TabIndex = 16;
            tempoNumericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            tempoNumericUpDown.ValueChanged += tempoNumericUpDown_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(175, 108);
            label5.Name = "label5";
            label5.Size = new Size(106, 41);
            label5.TabIndex = 17;
            label5.Text = "tempo";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(75, 534);
            label6.Name = "label6";
            label6.Size = new Size(198, 41);
            label6.TabIndex = 18;
            label6.Text = "MIDI Channel";
            // 
            // stringOneMidiChannelUpDown
            // 
            stringOneMidiChannelUpDown.Location = new Point(63, 593);
            stringOneMidiChannelUpDown.Name = "stringOneMidiChannelUpDown";
            stringOneMidiChannelUpDown.Size = new Size(89, 47);
            stringOneMidiChannelUpDown.TabIndex = 19;
            stringOneMidiChannelUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            stringOneMidiChannelUpDown.ValueChanged += stringOneMidiChannelUpDown_ValueChanged;
            // 
            // stringTwoMidiChannelUpDown
            // 
            stringTwoMidiChannelUpDown.Location = new Point(63, 638);
            stringTwoMidiChannelUpDown.Name = "stringTwoMidiChannelUpDown";
            stringTwoMidiChannelUpDown.Size = new Size(89, 47);
            stringTwoMidiChannelUpDown.TabIndex = 20;
            stringTwoMidiChannelUpDown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            stringTwoMidiChannelUpDown.ValueChanged += stringTwoMidiChannelUpDown_ValueChanged;
            // 
            // stringThreeMidiChannelUpDown
            // 
            stringThreeMidiChannelUpDown.Location = new Point(63, 689);
            stringThreeMidiChannelUpDown.Name = "stringThreeMidiChannelUpDown";
            stringThreeMidiChannelUpDown.Size = new Size(89, 47);
            stringThreeMidiChannelUpDown.TabIndex = 21;
            stringThreeMidiChannelUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            stringThreeMidiChannelUpDown.ValueChanged += stringThreeMidiChannelUpDown_ValueChanged;
            // 
            // stringFourMidiChannelUpDown
            // 
            stringFourMidiChannelUpDown.Location = new Point(63, 738);
            stringFourMidiChannelUpDown.Name = "stringFourMidiChannelUpDown";
            stringFourMidiChannelUpDown.Size = new Size(89, 47);
            stringFourMidiChannelUpDown.TabIndex = 22;
            stringFourMidiChannelUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });
            stringFourMidiChannelUpDown.ValueChanged += stringFourMidiChannelUpDown_ValueChanged;
            // 
            // stringFiveMidiChannelUpDown
            // 
            stringFiveMidiChannelUpDown.Location = new Point(63, 789);
            stringFiveMidiChannelUpDown.Name = "stringFiveMidiChannelUpDown";
            stringFiveMidiChannelUpDown.Size = new Size(89, 47);
            stringFiveMidiChannelUpDown.TabIndex = 23;
            stringFiveMidiChannelUpDown.Value = new decimal(new int[] { 5, 0, 0, 0 });
            stringFiveMidiChannelUpDown.ValueChanged += stringFiveMidiChannelUpDown_ValueChanged;
            // 
            // stringSixMidiChannelUpDown
            // 
            stringSixMidiChannelUpDown.Location = new Point(63, 835);
            stringSixMidiChannelUpDown.Name = "stringSixMidiChannelUpDown";
            stringSixMidiChannelUpDown.Size = new Size(89, 47);
            stringSixMidiChannelUpDown.TabIndex = 24;
            stringSixMidiChannelUpDown.Value = new decimal(new int[] { 6, 0, 0, 0 });
            stringSixMidiChannelUpDown.ValueChanged += stringSixMidiChannelUpDown_ValueChanged;
            // 
            // velocitySlider
            // 
            velocitySlider.Location = new Point(2000, 1077);
            velocitySlider.Maximum = 127;
            velocitySlider.Minimum = 1;
            velocitySlider.Name = "velocitySlider";
            velocitySlider.Orientation = Orientation.Vertical;
            velocitySlider.Size = new Size(114, 989);
            velocitySlider.TabIndex = 25;
            velocitySlider.Value = 64;
            velocitySlider.ValueChanged += velocitySlider_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1995, 2084);
            label7.Name = "label7";
            label7.Size = new Size(119, 41);
            label7.TabIndex = 26;
            label7.Text = "velocity";
            // 
            // startPlayingButton
            // 
            startPlayingButton.Location = new Point(366, 56);
            startPlayingButton.Name = "startPlayingButton";
            startPlayingButton.Size = new Size(231, 50);
            startPlayingButton.TabIndex = 27;
            startPlayingButton.Text = "Start Playing";
            startPlayingButton.UseVisualStyleBackColor = true;
            startPlayingButton.Click += startPlayingButton_Click;
            // 
            // StopPlayingButton
            // 
            StopPlayingButton.Location = new Point(608, 56);
            StopPlayingButton.Name = "StopPlayingButton";
            StopPlayingButton.Size = new Size(271, 50);
            StopPlayingButton.TabIndex = 28;
            StopPlayingButton.Text = "Stop Playing";
            StopPlayingButton.UseVisualStyleBackColor = true;
            StopPlayingButton.Click += StopPlayingButton_Click;
            // 
            // fretPatternPanel
            // 
            fretPatternPanel.BackColor = SystemColors.AppWorkspace;
            fretPatternPanel.Controls.Add(labelfretpattern);
            fretPatternPanel.Location = new Point(17, 23);
            fretPatternPanel.Name = "fretPatternPanel";
            fretPatternPanel.Size = new Size(3012, 2875);
            fretPatternPanel.TabIndex = 29;
            // 
            // labelfretpattern
            // 
            labelfretpattern.AutoSize = true;
            labelfretpattern.Location = new Point(443, 23);
            labelfretpattern.Name = "labelfretpattern";
            labelfretpattern.Size = new Size(184, 41);
            labelfretpattern.TabIndex = 0;
            labelfretpattern.Text = "Fret Patterns";
            // 
            // FingerPatternModeButton
            // 
            FingerPatternModeButton.Location = new Point(431, 188);
            FingerPatternModeButton.Name = "FingerPatternModeButton";
            FingerPatternModeButton.Size = new Size(344, 59);
            FingerPatternModeButton.TabIndex = 31;
            FingerPatternModeButton.Text = "Finger Pattern Mode";
            FingerPatternModeButton.UseVisualStyleBackColor = true;
            FingerPatternModeButton.Click += FingerPatternModeButton_Click;
            // 
            // loadFretPatternsDialog
            // 
            loadFretPatternsDialog.FileName = "openFileDialog2";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(30, 95);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(3102, 3008);
            tabControl1.TabIndex = 32;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(nextNoteButton);
            tabPage1.Controls.Add(deleteSelectedNoteButton);
            tabPage1.Controls.Add(clearCompositionButton);
            tabPage1.Controls.Add(editFingeringPatternButton);
            tabPage1.Controls.Add(clearFingeringPatternButton);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(addFingerinPatternToCompositionButton);
            tabPage1.Controls.Add(selectedFingeringPatternNumericUpDown);
            tabPage1.Controls.Add(FingerPatternModeButton);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(StopPlayingButton);
            tabPage1.Controls.Add(velocitySlider);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(buttonPlayMidi);
            tabPage1.Controls.Add(timeSignatureNumeratorNumericUpDown);
            tabPage1.Controls.Add(timeSignatureDenominatorNumericUpDown);
            tabPage1.Controls.Add(startPlayingButton);
            tabPage1.Controls.Add(tempoNumericUpDown);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(noteDuration);
            tabPage1.Controls.Add(stringSixMidiChannelUpDown);
            tabPage1.Controls.Add(lowPassFilterAlpha);
            tabPage1.Controls.Add(stringFiveMidiChannelUpDown);
            tabPage1.Controls.Add(EnvelopeLengthSlider);
            tabPage1.Controls.Add(stringFourMidiChannelUpDown);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(stringThreeMidiChannelUpDown);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(stringTwoMidiChannelUpDown);
            tabPage1.Controls.Add(fretboardPanel);
            tabPage1.Controls.Add(stringOneMidiChannelUpDown);
            tabPage1.Controls.Add(label6);
            tabPage1.Location = new Point(10, 58);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(3082, 2940);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Main Page";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // nextNoteButton
            // 
            nextNoteButton.Location = new Point(2167, 1187);
            nextNoteButton.Name = "nextNoteButton";
            nextNoteButton.Size = new Size(333, 58);
            nextNoteButton.TabIndex = 39;
            nextNoteButton.Text = "next note";
            nextNoteButton.UseVisualStyleBackColor = true;
            nextNoteButton.Click += nextNoteButton_Click;
            // 
            // deleteSelectedNoteButton
            // 
            deleteSelectedNoteButton.Location = new Point(2167, 1098);
            deleteSelectedNoteButton.Name = "deleteSelectedNoteButton";
            deleteSelectedNoteButton.Size = new Size(333, 58);
            deleteSelectedNoteButton.TabIndex = 38;
            deleteSelectedNoteButton.Text = "delete selected note";
            deleteSelectedNoteButton.UseVisualStyleBackColor = true;
            deleteSelectedNoteButton.Click += deleteSelectedNoteButton_Click;
            // 
            // clearCompositionButton
            // 
            clearCompositionButton.Location = new Point(1123, 45);
            clearCompositionButton.Name = "clearCompositionButton";
            clearCompositionButton.Size = new Size(390, 58);
            clearCompositionButton.TabIndex = 37;
            clearCompositionButton.Text = "Clear the composition";
            clearCompositionButton.UseVisualStyleBackColor = true;
            clearCompositionButton.Click += clearCompositionButton_Click;
            // 
            // editFingeringPatternButton
            // 
            editFingeringPatternButton.Location = new Point(1263, 185);
            editFingeringPatternButton.Margin = new Padding(4);
            editFingeringPatternButton.Name = "editFingeringPatternButton";
            editFingeringPatternButton.Size = new Size(196, 59);
            editFingeringPatternButton.TabIndex = 36;
            editFingeringPatternButton.Text = "Edit";
            editFingeringPatternButton.UseVisualStyleBackColor = true;
            editFingeringPatternButton.Click += editFingeringPatternButton_Click;
            // 
            // clearFingeringPatternButton
            // 
            clearFingeringPatternButton.Location = new Point(1084, 188);
            clearFingeringPatternButton.Margin = new Padding(4);
            clearFingeringPatternButton.Name = "clearFingeringPatternButton";
            clearFingeringPatternButton.Size = new Size(149, 59);
            clearFingeringPatternButton.TabIndex = 35;
            clearFingeringPatternButton.Text = "Clear";
            clearFingeringPatternButton.UseVisualStyleBackColor = true;
            clearFingeringPatternButton.Click += clearFingeringPatternButton_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(776, 134);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(138, 41);
            label8.TabIndex = 34;
            label8.Text = "Pattern #";
            // 
            // addFingerinPatternToCompositionButton
            // 
            addFingerinPatternToCompositionButton.Location = new Point(945, 191);
            addFingerinPatternToCompositionButton.Margin = new Padding(4);
            addFingerinPatternToCompositionButton.Name = "addFingerinPatternToCompositionButton";
            addFingerinPatternToCompositionButton.Size = new Size(127, 59);
            addFingerinPatternToCompositionButton.TabIndex = 33;
            addFingerinPatternToCompositionButton.Text = "Add ";
            addFingerinPatternToCompositionButton.UseVisualStyleBackColor = true;
            addFingerinPatternToCompositionButton.Click += addFingerinPatternToCompositionButton_Click;
            // 
            // selectedFingeringPatternNumericUpDown
            // 
            selectedFingeringPatternNumericUpDown.Location = new Point(798, 197);
            selectedFingeringPatternNumericUpDown.Margin = new Padding(4);
            selectedFingeringPatternNumericUpDown.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            selectedFingeringPatternNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            selectedFingeringPatternNumericUpDown.Name = "selectedFingeringPatternNumericUpDown";
            selectedFingeringPatternNumericUpDown.Size = new Size(116, 47);
            selectedFingeringPatternNumericUpDown.TabIndex = 32;
            selectedFingeringPatternNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            selectedFingeringPatternNumericUpDown.ValueChanged += selectedFingeringPatternNumericUpDown_ValueChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(fretPatternPanel);
            tabPage2.Location = new Point(10, 58);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(3082, 2940);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Fret Patterns";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(fingeringPatternpanel5);
            tabPage3.Controls.Add(fingeringPatternpanel4);
            tabPage3.Controls.Add(fingeringPatternPanel3);
            tabPage3.Controls.Add(fingeringPatternPanel2);
            tabPage3.Controls.Add(fingeringPatternPanel1);
            tabPage3.Location = new Point(10, 58);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(3082, 2940);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Fingering Patterns";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // fingeringPatternpanel5
            // 
            fingeringPatternpanel5.Location = new Point(42, 988);
            fingeringPatternpanel5.Margin = new Padding(4);
            fingeringPatternpanel5.Name = "fingeringPatternpanel5";
            fingeringPatternpanel5.Size = new Size(1391, 342);
            fingeringPatternpanel5.TabIndex = 1;
            fingeringPatternpanel5.Tag = "4";
            fingeringPatternpanel5.Paint += fingeringPatternPanelPaint;
            // 
            // fingeringPatternpanel4
            // 
            fingeringPatternpanel4.Location = new Point(1532, 525);
            fingeringPatternpanel4.Margin = new Padding(4);
            fingeringPatternpanel4.Name = "fingeringPatternpanel4";
            fingeringPatternpanel4.Size = new Size(1451, 360);
            fingeringPatternpanel4.TabIndex = 1;
            fingeringPatternpanel4.Tag = "3";
            fingeringPatternpanel4.Paint += fingeringPatternPanelPaint;
            // 
            // fingeringPatternPanel3
            // 
            fingeringPatternPanel3.Location = new Point(42, 504);
            fingeringPatternPanel3.Margin = new Padding(4);
            fingeringPatternPanel3.Name = "fingeringPatternPanel3";
            fingeringPatternPanel3.Size = new Size(1391, 347);
            fingeringPatternPanel3.TabIndex = 1;
            fingeringPatternPanel3.Tag = "2";
            fingeringPatternPanel3.Paint += fingeringPatternPanelPaint;
            // 
            // fingeringPatternPanel2
            // 
            fingeringPatternPanel2.Location = new Point(1532, 63);
            fingeringPatternPanel2.Margin = new Padding(4);
            fingeringPatternPanel2.Name = "fingeringPatternPanel2";
            fingeringPatternPanel2.Size = new Size(1451, 381);
            fingeringPatternPanel2.TabIndex = 1;
            fingeringPatternPanel2.Tag = "1";
            fingeringPatternPanel2.Paint += fingeringPatternPanelPaint;
            // 
            // fingeringPatternPanel1
            // 
            fingeringPatternPanel1.Location = new Point(42, 44);
            fingeringPatternPanel1.Margin = new Padding(4);
            fingeringPatternPanel1.Name = "fingeringPatternPanel1";
            fingeringPatternPanel1.Size = new Size(1391, 400);
            fingeringPatternPanel1.TabIndex = 0;
            fingeringPatternPanel1.Tag = "0";
            fingeringPatternPanel1.Paint += fingeringPatternPanelPaint;
            // 
            // openFingeringPatternsFileDialog
            // 
            openFingeringPatternsFileDialog.FileName = "openFileDialog2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(3366, 3131);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Tag = "40";
            Text = "Guitarap";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)lowPassFilterAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)EnvelopeLengthSlider).EndInit();
            panel1.ResumeLayout(false);
            noteDuration.ResumeLayout(false);
            noteDuration.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)timeSignatureNumeratorNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)timeSignatureDenominatorNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)tempoNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stringOneMidiChannelUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stringTwoMidiChannelUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stringThreeMidiChannelUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stringFourMidiChannelUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stringFiveMidiChannelUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stringSixMidiChannelUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)velocitySlider).EndInit();
            fretPatternPanel.ResumeLayout(false);
            fretPatternPanel.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)selectedFingeringPatternNumericUpDown).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonPlayMidi;
        private TrackBar lowPassFilterAlpha;
        private Label label1;
        private TrackBar EnvelopeLengthSlider;
        private Label label2;
        private Panel fretboardPanel;
        private Panel guitarRollPanel;
        private Panel panel1;
        private Button rondeButton;
        private Label label3;
        private Panel noteDuration;
        private Button tripletButton;
        private Button pointeeButton;
        private Button tripleCrocheButton;
        private Button doubleCrocheButton;
        private Button crocheButton;
        private Button noireButton;
        private Button blancheButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private NumericUpDown timeSignatureNumeratorNumericUpDown;
        private NumericUpDown timeSignatureDenominatorNumericUpDown;
        private Label label4;
        private NumericUpDown tempoNumericUpDown;
        private Label label5;
        private Label label6;
        private NumericUpDown stringOneMidiChannelUpDown;
        private NumericUpDown stringTwoMidiChannelUpDown;
        private NumericUpDown stringThreeMidiChannelUpDown;
        private NumericUpDown stringFourMidiChannelUpDown;
        private NumericUpDown stringFiveMidiChannelUpDown;
        private NumericUpDown stringSixMidiChannelUpDown;
        private TrackBar velocitySlider;
        private Label label7;
        private Button startPlayingButton;
        private Button StopPlayingButton;
        private ToolStripMenuItem exportAudioToolStripMenuItem;
        private ToolStripMenuItem exportMIDIToolStripMenuItem;
        private OpenFileDialog openAudioFileDialog;
        private SaveFileDialog exportAudioFileDialog;
        private SaveFileDialog exportMidiSaveFileDialog;
        public Panel fretPatternPanel;
        private Label labelfretpattern;
        private Button FingerPatternModeButton;
        private ToolStripMenuItem saveFretPatternsToolStripMenuItem;
        private ToolStripMenuItem loadFretPatternsToolStripMenuItem;
        private SaveFileDialog saveFretPatternsFileDialog;
        private OpenFileDialog loadFretPatternsDialog;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Panel fingeringPatternPanel1;
        private ToolStripMenuItem saveFingeringPatternsToolStripMenuItem;
        private ToolStripMenuItem loadFingeringPatternsToolStripMenuItem;
        private OpenFileDialog openFingeringPatternsFileDialog;
        private SaveFileDialog saveFingeringPatternsFileDialog;
        private Panel fingeringPatternpanel5;
        private Panel fingeringPatternpanel4;
        private Panel fingeringPatternPanel3;
        private Panel fingeringPatternPanel2;
        private Button addFingerinPatternToCompositionButton;
        private NumericUpDown selectedFingeringPatternNumericUpDown;
        private Label label8;
        private Button clearFingeringPatternButton;
        private Button editFingeringPatternButton;
        private Button clearCompositionButton;
        private Button setptatuplePerBeatButton;
        private Button sexatuplePerBeatButton;
        private Button quintuplePerBeatButton;
        private Button triplePerBeatButton;
        private Label label9;
        private Button nextNoteButton;
        private Button deleteSelectedNoteButton;
    }
}