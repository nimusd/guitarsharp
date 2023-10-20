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
            fretboard = new Panel();
            LEB2 = new Button();
            AE2 = new Button();
            LEBb2 = new Button();
            AD2 = new Button();
            LEA2 = new Button();
            AEb2 = new Button();
            LEGS2 = new Button();
            DA2 = new Button();
            LEG2 = new Button();
            ACs2 = new Button();
            LEFS2 = new Button();
            DGs2 = new Button();
            LEF2 = new Button();
            AC2 = new Button();
            LEE2 = new Button();
            DG2 = new Button();
            LEEb1 = new Button();
            AB2 = new Button();
            LED1 = new Button();
            DFs2 = new Button();
            LECS1 = new Button();
            ABb2 = new Button();
            LEC1 = new Button();
            GD2 = new Button();
            LEB1 = new Button();
            AA2 = new Button();
            LEBb1 = new Button();
            DF2 = new Button();
            LEA1 = new Button();
            AGs1 = new Button();
            LEGS1 = new Button();
            GCs2 = new Button();
            LEG1 = new Button();
            AG1 = new Button();
            LEFS1 = new Button();
            LEF1 = new Button();
            DE2 = new Button();
            LEE1 = new Button();
            AFs1 = new Button();
            GC2 = new Button();
            AF1 = new Button();
            DEb2 = new Button();
            AE1 = new Button();
            GB2 = new Button();
            AEb1 = new Button();
            DD2 = new Button();
            AD1 = new Button();
            BFs2 = new Button();
            ACs1 = new Button();
            DCs1 = new Button();
            AC1 = new Button();
            GBb2 = new Button();
            AB1 = new Button();
            DC1 = new Button();
            ABb1 = new Button();
            AA1 = new Button();
            BF2 = new Button();
            DB1 = new Button();
            GA2 = new Button();
            DBb1 = new Button();
            BE2 = new Button();
            DA1 = new Button();
            GGs2 = new Button();
            DGs1 = new Button();
            BEb2 = new Button();
            DG1 = new Button();
            GG2 = new Button();
            DFs1 = new Button();
            BD2 = new Button();
            DF1 = new Button();
            GFs1 = new Button();
            DE1 = new Button();
            Deb1 = new Button();
            BCs2 = new Button();
            DD1 = new Button();
            GF1 = new Button();
            BC2 = new Button();
            GE1 = new Button();
            BB2 = new Button();
            GEb1 = new Button();
            BBb1 = new Button();
            GD1 = new Button();
            BA1 = new Button();
            GCs1 = new Button();
            BGs1 = new Button();
            GC1 = new Button();
            BG1 = new Button();
            GB1 = new Button();
            BFs1 = new Button();
            GBb1 = new Button();
            BF1 = new Button();
            GA1 = new Button();
            BE1 = new Button();
            GGs1 = new Button();
            GG1 = new Button();
            BEb1 = new Button();
            BD1 = new Button();
            BCs1 = new Button();
            BC1 = new Button();
            BB1 = new Button();
            HEB2 = new Button();
            HEBb2 = new Button();
            HEA2 = new Button();
            HEGs2 = new Button();
            HEG2 = new Button();
            HEFs2 = new Button();
            HEF2 = new Button();
            HEE2 = new Button();
            HEEb1 = new Button();
            HED1 = new Button();
            HECs1 = new Button();
            HEC1 = new Button();
            HEB1 = new Button();
            HEBb1 = new Button();
            HEA1 = new Button();
            HEGs1 = new Button();
            HEG1 = new Button();
            HEFs1 = new Button();
            HEF1 = new Button();
            HEE1 = new Button();
            guitarRollPanel = new Panel();
            velocityPanel = new Panel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)lowPassFilterAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EnvelopeLengthSlider).BeginInit();
            fretboard.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonPlayMidi
            // 
            buttonPlayMidi.Location = new Point(107, 60);
            buttonPlayMidi.Name = "buttonPlayMidi";
            buttonPlayMidi.Size = new Size(188, 58);
            buttonPlayMidi.TabIndex = 0;
            buttonPlayMidi.Text = "play MIDI";
            buttonPlayMidi.UseVisualStyleBackColor = true;
            buttonPlayMidi.Click += buttonPlayMidi_Click;
            // 
            // lowPassFilterAlpha
            // 
            lowPassFilterAlpha.Location = new Point(355, 136);
            lowPassFilterAlpha.Maximum = 100;
            lowPassFilterAlpha.Minimum = 1;
            lowPassFilterAlpha.Name = "lowPassFilterAlpha";
            lowPassFilterAlpha.Size = new Size(702, 114);
            lowPassFilterAlpha.TabIndex = 1;
            lowPassFilterAlpha.Value = 1;
            lowPassFilterAlpha.ValueChanged += lowPassFilterAlpha_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(376, 38);
            label1.Name = "label1";
            label1.Size = new Size(354, 47);
            label1.TabIndex = 2;
            label1.Text = "Low Pass Filter Alpha ";
            // 
            // EnvelopeLengthSlider
            // 
            EnvelopeLengthSlider.Location = new Point(1158, 136);
            EnvelopeLengthSlider.Maximum = 100;
            EnvelopeLengthSlider.Minimum = 1;
            EnvelopeLengthSlider.Name = "EnvelopeLengthSlider";
            EnvelopeLengthSlider.Size = new Size(684, 114);
            EnvelopeLengthSlider.TabIndex = 3;
            EnvelopeLengthSlider.Value = 1;
            EnvelopeLengthSlider.ValueChanged += EnvelopeLengthSlider_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1138, 21);
            label2.Name = "label2";
            label2.Size = new Size(270, 47);
            label2.TabIndex = 4;
            label2.Text = "envelope length";
            // 
            // fretboard
            // 
            fretboard.BackColor = SystemColors.AppWorkspace;
            fretboard.BorderStyle = BorderStyle.FixedSingle;
            fretboard.Controls.Add(LEB2);
            fretboard.Controls.Add(AE2);
            fretboard.Controls.Add(LEBb2);
            fretboard.Controls.Add(AD2);
            fretboard.Controls.Add(LEA2);
            fretboard.Controls.Add(AEb2);
            fretboard.Controls.Add(LEGS2);
            fretboard.Controls.Add(DA2);
            fretboard.Controls.Add(LEG2);
            fretboard.Controls.Add(ACs2);
            fretboard.Controls.Add(LEFS2);
            fretboard.Controls.Add(DGs2);
            fretboard.Controls.Add(LEF2);
            fretboard.Controls.Add(AC2);
            fretboard.Controls.Add(LEE2);
            fretboard.Controls.Add(DG2);
            fretboard.Controls.Add(LEEb1);
            fretboard.Controls.Add(AB2);
            fretboard.Controls.Add(LED1);
            fretboard.Controls.Add(DFs2);
            fretboard.Controls.Add(LECS1);
            fretboard.Controls.Add(ABb2);
            fretboard.Controls.Add(LEC1);
            fretboard.Controls.Add(GD2);
            fretboard.Controls.Add(LEB1);
            fretboard.Controls.Add(AA2);
            fretboard.Controls.Add(LEBb1);
            fretboard.Controls.Add(DF2);
            fretboard.Controls.Add(LEA1);
            fretboard.Controls.Add(AGs1);
            fretboard.Controls.Add(LEGS1);
            fretboard.Controls.Add(GCs2);
            fretboard.Controls.Add(LEG1);
            fretboard.Controls.Add(AG1);
            fretboard.Controls.Add(LEFS1);
            fretboard.Controls.Add(LEF1);
            fretboard.Controls.Add(DE2);
            fretboard.Controls.Add(LEE1);
            fretboard.Controls.Add(AFs1);
            fretboard.Controls.Add(GC2);
            fretboard.Controls.Add(AF1);
            fretboard.Controls.Add(DEb2);
            fretboard.Controls.Add(AE1);
            fretboard.Controls.Add(GB2);
            fretboard.Controls.Add(AEb1);
            fretboard.Controls.Add(DD2);
            fretboard.Controls.Add(AD1);
            fretboard.Controls.Add(BFs2);
            fretboard.Controls.Add(ACs1);
            fretboard.Controls.Add(DCs1);
            fretboard.Controls.Add(AC1);
            fretboard.Controls.Add(GBb2);
            fretboard.Controls.Add(AB1);
            fretboard.Controls.Add(DC1);
            fretboard.Controls.Add(ABb1);
            fretboard.Controls.Add(AA1);
            fretboard.Controls.Add(BF2);
            fretboard.Controls.Add(DB1);
            fretboard.Controls.Add(GA2);
            fretboard.Controls.Add(DBb1);
            fretboard.Controls.Add(BE2);
            fretboard.Controls.Add(DA1);
            fretboard.Controls.Add(GGs2);
            fretboard.Controls.Add(DGs1);
            fretboard.Controls.Add(BEb2);
            fretboard.Controls.Add(DG1);
            fretboard.Controls.Add(GG2);
            fretboard.Controls.Add(DFs1);
            fretboard.Controls.Add(BD2);
            fretboard.Controls.Add(DF1);
            fretboard.Controls.Add(GFs1);
            fretboard.Controls.Add(DE1);
            fretboard.Controls.Add(Deb1);
            fretboard.Controls.Add(BCs2);
            fretboard.Controls.Add(DD1);
            fretboard.Controls.Add(GF1);
            fretboard.Controls.Add(BC2);
            fretboard.Controls.Add(GE1);
            fretboard.Controls.Add(BB2);
            fretboard.Controls.Add(GEb1);
            fretboard.Controls.Add(BBb1);
            fretboard.Controls.Add(GD1);
            fretboard.Controls.Add(BA1);
            fretboard.Controls.Add(GCs1);
            fretboard.Controls.Add(BGs1);
            fretboard.Controls.Add(GC1);
            fretboard.Controls.Add(BG1);
            fretboard.Controls.Add(GB1);
            fretboard.Controls.Add(BFs1);
            fretboard.Controls.Add(GBb1);
            fretboard.Controls.Add(BF1);
            fretboard.Controls.Add(GA1);
            fretboard.Controls.Add(BE1);
            fretboard.Controls.Add(GGs1);
            fretboard.Controls.Add(GG1);
            fretboard.Controls.Add(BEb1);
            fretboard.Controls.Add(BD1);
            fretboard.Controls.Add(BCs1);
            fretboard.Controls.Add(BC1);
            fretboard.Controls.Add(BB1);
            fretboard.Controls.Add(HEB2);
            fretboard.Controls.Add(HEBb2);
            fretboard.Controls.Add(HEA2);
            fretboard.Controls.Add(HEGs2);
            fretboard.Controls.Add(HEG2);
            fretboard.Controls.Add(HEFs2);
            fretboard.Controls.Add(HEF2);
            fretboard.Controls.Add(HEE2);
            fretboard.Controls.Add(HEEb1);
            fretboard.Controls.Add(HED1);
            fretboard.Controls.Add(HECs1);
            fretboard.Controls.Add(HEC1);
            fretboard.Controls.Add(HEB1);
            fretboard.Controls.Add(HEBb1);
            fretboard.Controls.Add(HEA1);
            fretboard.Controls.Add(HEGs1);
            fretboard.Controls.Add(HEG1);
            fretboard.Controls.Add(HEFs1);
            fretboard.Controls.Add(HEF1);
            fretboard.Controls.Add(HEE1);
            fretboard.Location = new Point(128, 314);
            fretboard.Name = "fretboard";
            fretboard.Size = new Size(2013, 348);
            fretboard.TabIndex = 5;
            fretboard.Tag = "80";
            // 
            // LEB2
            // 
            LEB2.BackColor = SystemColors.GradientInactiveCaption;
            LEB2.Location = new Point(1900, 291);
            LEB2.Name = "LEB2";
            LEB2.Size = new Size(100, 50);
            LEB2.TabIndex = 20;
            LEB2.Tag = "59";
            LEB2.Text = "B";
            LEB2.UseVisualStyleBackColor = false;
            // 
            // AE2
            // 
            AE2.BackColor = SystemColors.GradientInactiveCaption;
            AE2.Location = new Point(1900, 235);
            AE2.Name = "AE2";
            AE2.Size = new Size(100, 50);
            AE2.TabIndex = 20;
            AE2.Tag = "64";
            AE2.Text = "E";
            AE2.UseVisualStyleBackColor = false;
            // 
            // LEBb2
            // 
            LEBb2.BackColor = SystemColors.GradientInactiveCaption;
            LEBb2.Location = new Point(1800, 291);
            LEBb2.Name = "LEBb2";
            LEBb2.Size = new Size(100, 50);
            LEBb2.TabIndex = 19;
            LEBb2.Tag = "58";
            LEBb2.Text = "Bb";
            LEBb2.UseVisualStyleBackColor = false;
            // 
            // AD2
            // 
            AD2.BackColor = SystemColors.GradientInactiveCaption;
            AD2.Location = new Point(1698, 232);
            AD2.Name = "AD2";
            AD2.Size = new Size(100, 50);
            AD2.TabIndex = 18;
            AD2.Tag = "62";
            AD2.Text = "D";
            AD2.UseVisualStyleBackColor = false;
            // 
            // LEA2
            // 
            LEA2.BackColor = SystemColors.GradientInactiveCaption;
            LEA2.Location = new Point(1700, 291);
            LEA2.Name = "LEA2";
            LEA2.Size = new Size(100, 50);
            LEA2.TabIndex = 18;
            LEA2.Tag = "57";
            LEA2.Text = "A";
            LEA2.UseVisualStyleBackColor = false;
            // 
            // AEb2
            // 
            AEb2.BackColor = SystemColors.GradientInactiveCaption;
            AEb2.Location = new Point(1800, 235);
            AEb2.Name = "AEb2";
            AEb2.Size = new Size(100, 50);
            AEb2.TabIndex = 19;
            AEb2.Tag = "63";
            AEb2.Text = "Eb";
            AEb2.UseVisualStyleBackColor = false;
            // 
            // LEGS2
            // 
            LEGS2.BackColor = SystemColors.GradientInactiveCaption;
            LEGS2.Location = new Point(1600, 291);
            LEGS2.Name = "LEGS2";
            LEGS2.Size = new Size(100, 50);
            LEGS2.TabIndex = 17;
            LEGS2.Tag = "56";
            LEGS2.Text = "G#";
            LEGS2.UseVisualStyleBackColor = false;
            // 
            // DA2
            // 
            DA2.BackColor = SystemColors.GradientInactiveCaption;
            DA2.Location = new Point(1900, 179);
            DA2.Name = "DA2";
            DA2.Size = new Size(100, 50);
            DA2.TabIndex = 20;
            DA2.Tag = "69";
            DA2.Text = "A";
            DA2.UseVisualStyleBackColor = false;
            // 
            // LEG2
            // 
            LEG2.BackColor = SystemColors.GradientInactiveCaption;
            LEG2.Location = new Point(1500, 291);
            LEG2.Name = "LEG2";
            LEG2.Size = new Size(100, 50);
            LEG2.TabIndex = 16;
            LEG2.Tag = "55";
            LEG2.Text = "G";
            LEG2.UseVisualStyleBackColor = false;
            // 
            // ACs2
            // 
            ACs2.BackColor = SystemColors.GradientInactiveCaption;
            ACs2.Location = new Point(1600, 232);
            ACs2.Name = "ACs2";
            ACs2.Size = new Size(100, 50);
            ACs2.TabIndex = 17;
            ACs2.Tag = "61";
            ACs2.Text = "C#";
            ACs2.UseVisualStyleBackColor = false;
            // 
            // LEFS2
            // 
            LEFS2.BackColor = SystemColors.GradientInactiveCaption;
            LEFS2.Location = new Point(1400, 288);
            LEFS2.Name = "LEFS2";
            LEFS2.Size = new Size(100, 50);
            LEFS2.TabIndex = 15;
            LEFS2.Tag = "54";
            LEFS2.Text = "F#";
            LEFS2.UseVisualStyleBackColor = false;
            // 
            // DGs2
            // 
            DGs2.BackColor = SystemColors.GradientInactiveCaption;
            DGs2.Location = new Point(1800, 179);
            DGs2.Name = "DGs2";
            DGs2.Size = new Size(100, 50);
            DGs2.TabIndex = 19;
            DGs2.Tag = "68";
            DGs2.Text = "G#";
            DGs2.UseVisualStyleBackColor = false;
            // 
            // LEF2
            // 
            LEF2.BackColor = SystemColors.GradientInactiveCaption;
            LEF2.Location = new Point(1300, 288);
            LEF2.Name = "LEF2";
            LEF2.Size = new Size(100, 50);
            LEF2.TabIndex = 14;
            LEF2.Tag = "53";
            LEF2.Text = "F";
            LEF2.UseVisualStyleBackColor = false;
            // 
            // AC2
            // 
            AC2.BackColor = SystemColors.GradientInactiveCaption;
            AC2.Location = new Point(1500, 232);
            AC2.Name = "AC2";
            AC2.Size = new Size(100, 50);
            AC2.TabIndex = 16;
            AC2.Tag = "60";
            AC2.Text = "C";
            AC2.UseVisualStyleBackColor = false;
            // 
            // LEE2
            // 
            LEE2.BackColor = SystemColors.Info;
            LEE2.Location = new Point(1200, 288);
            LEE2.Name = "LEE2";
            LEE2.Size = new Size(100, 50);
            LEE2.TabIndex = 13;
            LEE2.Tag = "52";
            LEE2.Text = "E";
            LEE2.UseVisualStyleBackColor = false;
            // 
            // DG2
            // 
            DG2.BackColor = SystemColors.GradientInactiveCaption;
            DG2.Location = new Point(1700, 179);
            DG2.Name = "DG2";
            DG2.Size = new Size(100, 50);
            DG2.TabIndex = 18;
            DG2.Tag = "67";
            DG2.Text = "G";
            DG2.UseVisualStyleBackColor = false;
            // 
            // LEEb1
            // 
            LEEb1.BackColor = SystemColors.GradientInactiveCaption;
            LEEb1.Location = new Point(1100, 288);
            LEEb1.Name = "LEEb1";
            LEEb1.Size = new Size(100, 50);
            LEEb1.TabIndex = 12;
            LEEb1.Tag = "51";
            LEEb1.Text = "Eb";
            LEEb1.UseVisualStyleBackColor = false;
            // 
            // AB2
            // 
            AB2.BackColor = SystemColors.GradientInactiveCaption;
            AB2.Location = new Point(1402, 232);
            AB2.Name = "AB2";
            AB2.Size = new Size(100, 50);
            AB2.TabIndex = 15;
            AB2.Tag = "59";
            AB2.Text = "B";
            AB2.UseVisualStyleBackColor = false;
            // 
            // LED1
            // 
            LED1.BackColor = SystemColors.ControlLightLight;
            LED1.Location = new Point(1000, 288);
            LED1.Name = "LED1";
            LED1.Size = new Size(100, 50);
            LED1.TabIndex = 11;
            LED1.Tag = "50";
            LED1.Text = "D";
            LED1.UseVisualStyleBackColor = false;
            // 
            // DFs2
            // 
            DFs2.BackColor = SystemColors.GradientInactiveCaption;
            DFs2.Location = new Point(1602, 179);
            DFs2.Name = "DFs2";
            DFs2.Size = new Size(100, 50);
            DFs2.TabIndex = 17;
            DFs2.Tag = "66";
            DFs2.Text = "F#";
            DFs2.UseVisualStyleBackColor = false;
            // 
            // LECS1
            // 
            LECS1.BackColor = SystemColors.GradientInactiveCaption;
            LECS1.Location = new Point(902, 288);
            LECS1.Name = "LECS1";
            LECS1.Size = new Size(100, 50);
            LECS1.TabIndex = 10;
            LECS1.Tag = "49";
            LECS1.Text = "C#";
            LECS1.UseVisualStyleBackColor = false;
            // 
            // ABb2
            // 
            ABb2.BackColor = SystemColors.GradientInactiveCaption;
            ABb2.Location = new Point(1302, 232);
            ABb2.Name = "ABb2";
            ABb2.Size = new Size(100, 50);
            ABb2.TabIndex = 14;
            ABb2.Tag = "58";
            ABb2.Text = "Bb";
            ABb2.UseVisualStyleBackColor = false;
            // 
            // LEC1
            // 
            LEC1.BackColor = SystemColors.GradientInactiveCaption;
            LEC1.Location = new Point(802, 288);
            LEC1.Name = "LEC1";
            LEC1.Size = new Size(100, 50);
            LEC1.TabIndex = 9;
            LEC1.Tag = "48";
            LEC1.Text = "C";
            LEC1.UseVisualStyleBackColor = false;
            // 
            // GD2
            // 
            GD2.BackColor = SystemColors.GradientInactiveCaption;
            GD2.Location = new Point(1898, 120);
            GD2.Name = "GD2";
            GD2.Size = new Size(100, 50);
            GD2.TabIndex = 20;
            GD2.Tag = "74";
            GD2.Text = "D";
            GD2.UseVisualStyleBackColor = false;
            // 
            // LEB1
            // 
            LEB1.BackColor = SystemColors.GradientInactiveCaption;
            LEB1.Location = new Point(702, 288);
            LEB1.Name = "LEB1";
            LEB1.Size = new Size(100, 50);
            LEB1.TabIndex = 8;
            LEB1.Tag = "47";
            LEB1.Text = "B";
            LEB1.UseVisualStyleBackColor = false;
            // 
            // AA2
            // 
            AA2.BackColor = SystemColors.Info;
            AA2.Location = new Point(1202, 232);
            AA2.Name = "AA2";
            AA2.Size = new Size(100, 50);
            AA2.TabIndex = 13;
            AA2.Tag = "57";
            AA2.Text = "A";
            AA2.UseVisualStyleBackColor = false;
            // 
            // LEBb1
            // 
            LEBb1.BackColor = SystemColors.GradientInactiveCaption;
            LEBb1.Location = new Point(602, 288);
            LEBb1.Name = "LEBb1";
            LEBb1.Size = new Size(100, 50);
            LEBb1.TabIndex = 7;
            LEBb1.Tag = "46";
            LEBb1.Text = "Bb";
            LEBb1.UseVisualStyleBackColor = false;
            // 
            // DF2
            // 
            DF2.BackColor = SystemColors.GradientInactiveCaption;
            DF2.Location = new Point(1502, 179);
            DF2.Name = "DF2";
            DF2.Size = new Size(100, 50);
            DF2.TabIndex = 16;
            DF2.Tag = "65";
            DF2.Text = "F";
            DF2.UseVisualStyleBackColor = false;
            // 
            // LEA1
            // 
            LEA1.BackColor = SystemColors.ControlLightLight;
            LEA1.Location = new Point(502, 288);
            LEA1.Name = "LEA1";
            LEA1.Size = new Size(100, 50);
            LEA1.TabIndex = 6;
            LEA1.Tag = "45";
            LEA1.Text = "A";
            LEA1.UseVisualStyleBackColor = false;
            // 
            // AGs1
            // 
            AGs1.BackColor = SystemColors.GradientInactiveCaption;
            AGs1.Location = new Point(1102, 232);
            AGs1.Name = "AGs1";
            AGs1.Size = new Size(100, 50);
            AGs1.TabIndex = 12;
            AGs1.Tag = "56";
            AGs1.Text = "G#";
            AGs1.UseVisualStyleBackColor = false;
            // 
            // LEGS1
            // 
            LEGS1.BackColor = SystemColors.GradientInactiveCaption;
            LEGS1.Location = new Point(402, 285);
            LEGS1.Name = "LEGS1";
            LEGS1.Size = new Size(100, 50);
            LEGS1.TabIndex = 5;
            LEGS1.Tag = "44";
            LEGS1.Text = "G#";
            LEGS1.UseVisualStyleBackColor = false;
            // 
            // GCs2
            // 
            GCs2.BackColor = SystemColors.GradientInactiveCaption;
            GCs2.Location = new Point(1800, 120);
            GCs2.Name = "GCs2";
            GCs2.Size = new Size(100, 50);
            GCs2.TabIndex = 19;
            GCs2.Tag = "73";
            GCs2.Text = "C#";
            GCs2.UseVisualStyleBackColor = false;
            // 
            // LEG1
            // 
            LEG1.BackColor = SystemColors.GradientInactiveCaption;
            LEG1.Location = new Point(302, 285);
            LEG1.Name = "LEG1";
            LEG1.Size = new Size(100, 50);
            LEG1.TabIndex = 4;
            LEG1.Tag = "43";
            LEG1.Text = "G";
            LEG1.UseVisualStyleBackColor = false;
            // 
            // AG1
            // 
            AG1.BackColor = SystemColors.ControlLightLight;
            AG1.Location = new Point(1002, 232);
            AG1.Name = "AG1";
            AG1.Size = new Size(100, 50);
            AG1.TabIndex = 11;
            AG1.Tag = "55";
            AG1.Text = "G";
            AG1.UseVisualStyleBackColor = false;
            // 
            // LEFS1
            // 
            LEFS1.BackColor = SystemColors.GradientInactiveCaption;
            LEFS1.Location = new Point(202, 285);
            LEFS1.Name = "LEFS1";
            LEFS1.Size = new Size(100, 50);
            LEFS1.TabIndex = 3;
            LEFS1.Tag = "42";
            LEFS1.Text = "F#";
            LEFS1.UseVisualStyleBackColor = false;
            // 
            // LEF1
            // 
            LEF1.BackColor = SystemColors.GradientInactiveCaption;
            LEF1.Location = new Point(102, 285);
            LEF1.Name = "LEF1";
            LEF1.Size = new Size(100, 50);
            LEF1.TabIndex = 2;
            LEF1.Tag = "41";
            LEF1.Text = "F";
            LEF1.UseVisualStyleBackColor = false;
            // 
            // DE2
            // 
            DE2.BackColor = SystemColors.GradientInactiveCaption;
            DE2.Location = new Point(1402, 179);
            DE2.Name = "DE2";
            DE2.Size = new Size(100, 50);
            DE2.TabIndex = 15;
            DE2.Tag = "64";
            DE2.Text = "E";
            DE2.UseVisualStyleBackColor = false;
            // 
            // LEE1
            // 
            LEE1.BackColor = SystemColors.Info;
            LEE1.Location = new Point(2, 285);
            LEE1.Name = "LEE1";
            LEE1.Size = new Size(100, 50);
            LEE1.TabIndex = 1;
            LEE1.Tag = "40";
            LEE1.Text = "E";
            LEE1.UseVisualStyleBackColor = false;
            // 
            // AFs1
            // 
            AFs1.BackColor = SystemColors.GradientInactiveCaption;
            AFs1.Location = new Point(902, 229);
            AFs1.Name = "AFs1";
            AFs1.Size = new Size(100, 50);
            AFs1.TabIndex = 10;
            AFs1.Tag = "54";
            AFs1.Text = "F#";
            AFs1.UseVisualStyleBackColor = false;
            // 
            // GC2
            // 
            GC2.BackColor = SystemColors.GradientInactiveCaption;
            GC2.Location = new Point(1700, 120);
            GC2.Name = "GC2";
            GC2.Size = new Size(100, 50);
            GC2.TabIndex = 18;
            GC2.Tag = "72";
            GC2.Text = "C";
            GC2.UseVisualStyleBackColor = false;
            // 
            // AF1
            // 
            AF1.BackColor = SystemColors.GradientInactiveCaption;
            AF1.Location = new Point(802, 229);
            AF1.Name = "AF1";
            AF1.Size = new Size(100, 50);
            AF1.TabIndex = 9;
            AF1.Tag = "53";
            AF1.Text = "F";
            AF1.UseVisualStyleBackColor = false;
            // 
            // DEb2
            // 
            DEb2.BackColor = SystemColors.GradientInactiveCaption;
            DEb2.Location = new Point(1302, 179);
            DEb2.Name = "DEb2";
            DEb2.Size = new Size(100, 50);
            DEb2.TabIndex = 14;
            DEb2.Tag = "63";
            DEb2.Text = "Eb";
            DEb2.UseVisualStyleBackColor = false;
            // 
            // AE1
            // 
            AE1.BackColor = SystemColors.GradientInactiveCaption;
            AE1.Location = new Point(702, 229);
            AE1.Name = "AE1";
            AE1.Size = new Size(100, 50);
            AE1.TabIndex = 8;
            AE1.Tag = "52";
            AE1.Text = "E";
            AE1.UseVisualStyleBackColor = false;
            // 
            // GB2
            // 
            GB2.BackColor = SystemColors.GradientInactiveCaption;
            GB2.Location = new Point(1602, 120);
            GB2.Name = "GB2";
            GB2.Size = new Size(100, 50);
            GB2.TabIndex = 17;
            GB2.Tag = "71";
            GB2.Text = "B";
            GB2.UseVisualStyleBackColor = false;
            // 
            // AEb1
            // 
            AEb1.BackColor = SystemColors.GradientInactiveCaption;
            AEb1.Location = new Point(602, 229);
            AEb1.Name = "AEb1";
            AEb1.Size = new Size(100, 50);
            AEb1.TabIndex = 7;
            AEb1.Tag = "51";
            AEb1.Text = "Eb";
            AEb1.UseVisualStyleBackColor = false;
            // 
            // DD2
            // 
            DD2.BackColor = SystemColors.Info;
            DD2.Location = new Point(1202, 179);
            DD2.Name = "DD2";
            DD2.Size = new Size(100, 50);
            DD2.TabIndex = 13;
            DD2.Tag = "62";
            DD2.Text = "D";
            DD2.UseVisualStyleBackColor = false;
            // 
            // AD1
            // 
            AD1.BackColor = SystemColors.ControlLightLight;
            AD1.Location = new Point(502, 229);
            AD1.Name = "AD1";
            AD1.Size = new Size(100, 50);
            AD1.TabIndex = 6;
            AD1.Tag = "50";
            AD1.Text = "D";
            AD1.UseVisualStyleBackColor = false;
            // 
            // BFs2
            // 
            BFs2.BackColor = SystemColors.GradientInactiveCaption;
            BFs2.Location = new Point(1900, 64);
            BFs2.Name = "BFs2";
            BFs2.Size = new Size(100, 50);
            BFs2.TabIndex = 20;
            BFs2.Tag = "78";
            BFs2.Text = "F#";
            BFs2.UseVisualStyleBackColor = false;
            // 
            // ACs1
            // 
            ACs1.BackColor = SystemColors.GradientInactiveCaption;
            ACs1.Location = new Point(404, 229);
            ACs1.Name = "ACs1";
            ACs1.Size = new Size(100, 50);
            ACs1.TabIndex = 5;
            ACs1.Tag = "49";
            ACs1.Text = "C#";
            ACs1.UseVisualStyleBackColor = false;
            // 
            // DCs1
            // 
            DCs1.BackColor = SystemColors.GradientInactiveCaption;
            DCs1.Location = new Point(1104, 179);
            DCs1.Name = "DCs1";
            DCs1.Size = new Size(100, 50);
            DCs1.TabIndex = 12;
            DCs1.Tag = "61";
            DCs1.Text = "C#";
            DCs1.UseVisualStyleBackColor = false;
            // 
            // AC1
            // 
            AC1.BackColor = SystemColors.GradientInactiveCaption;
            AC1.Location = new Point(304, 229);
            AC1.Name = "AC1";
            AC1.Size = new Size(100, 50);
            AC1.TabIndex = 4;
            AC1.Tag = "48";
            AC1.Text = "C";
            AC1.UseVisualStyleBackColor = false;
            // 
            // GBb2
            // 
            GBb2.BackColor = SystemColors.GradientInactiveCaption;
            GBb2.Location = new Point(1502, 120);
            GBb2.Name = "GBb2";
            GBb2.Size = new Size(100, 50);
            GBb2.TabIndex = 16;
            GBb2.Tag = "70";
            GBb2.Text = "Bb";
            GBb2.UseVisualStyleBackColor = false;
            // 
            // AB1
            // 
            AB1.BackColor = SystemColors.GradientInactiveCaption;
            AB1.Location = new Point(204, 229);
            AB1.Name = "AB1";
            AB1.Size = new Size(100, 50);
            AB1.TabIndex = 3;
            AB1.Tag = "47";
            AB1.Text = "B";
            AB1.UseVisualStyleBackColor = false;
            // 
            // DC1
            // 
            DC1.BackColor = SystemColors.ControlLightLight;
            DC1.Location = new Point(1004, 179);
            DC1.Name = "DC1";
            DC1.Size = new Size(100, 50);
            DC1.TabIndex = 11;
            DC1.Tag = "60";
            DC1.Text = "C";
            DC1.UseVisualStyleBackColor = false;
            // 
            // ABb1
            // 
            ABb1.BackColor = SystemColors.GradientInactiveCaption;
            ABb1.Location = new Point(104, 229);
            ABb1.Name = "ABb1";
            ABb1.Size = new Size(100, 50);
            ABb1.TabIndex = 2;
            ABb1.Tag = "46";
            ABb1.Text = "Bb";
            ABb1.UseVisualStyleBackColor = false;
            // 
            // AA1
            // 
            AA1.BackColor = SystemColors.Info;
            AA1.Location = new Point(4, 229);
            AA1.Name = "AA1";
            AA1.Size = new Size(100, 50);
            AA1.TabIndex = 1;
            AA1.Tag = "45";
            AA1.Text = "A";
            AA1.UseVisualStyleBackColor = false;
            // 
            // BF2
            // 
            BF2.BackColor = SystemColors.GradientInactiveCaption;
            BF2.Location = new Point(1800, 64);
            BF2.Name = "BF2";
            BF2.Size = new Size(100, 50);
            BF2.TabIndex = 19;
            BF2.Tag = "77";
            BF2.Text = "F";
            BF2.UseVisualStyleBackColor = false;
            // 
            // DB1
            // 
            DB1.BackColor = SystemColors.GradientInactiveCaption;
            DB1.Location = new Point(904, 176);
            DB1.Name = "DB1";
            DB1.Size = new Size(100, 50);
            DB1.TabIndex = 10;
            DB1.Tag = "59";
            DB1.Text = "B";
            DB1.UseVisualStyleBackColor = false;
            // 
            // GA2
            // 
            GA2.BackColor = SystemColors.GradientInactiveCaption;
            GA2.Location = new Point(1402, 120);
            GA2.Name = "GA2";
            GA2.Size = new Size(100, 50);
            GA2.TabIndex = 15;
            GA2.Tag = "69";
            GA2.Text = "A";
            GA2.UseVisualStyleBackColor = false;
            // 
            // DBb1
            // 
            DBb1.BackColor = SystemColors.GradientInactiveCaption;
            DBb1.Location = new Point(804, 176);
            DBb1.Name = "DBb1";
            DBb1.Size = new Size(100, 50);
            DBb1.TabIndex = 9;
            DBb1.Tag = "58";
            DBb1.Text = "Bb";
            DBb1.UseVisualStyleBackColor = false;
            // 
            // BE2
            // 
            BE2.BackColor = SystemColors.GradientInactiveCaption;
            BE2.Location = new Point(1700, 64);
            BE2.Name = "BE2";
            BE2.Size = new Size(100, 50);
            BE2.TabIndex = 18;
            BE2.Tag = "76";
            BE2.Text = "E";
            BE2.UseVisualStyleBackColor = false;
            // 
            // DA1
            // 
            DA1.BackColor = SystemColors.GradientInactiveCaption;
            DA1.Location = new Point(704, 176);
            DA1.Name = "DA1";
            DA1.Size = new Size(100, 50);
            DA1.TabIndex = 8;
            DA1.Tag = "57";
            DA1.Text = "A";
            DA1.UseVisualStyleBackColor = false;
            // 
            // GGs2
            // 
            GGs2.BackColor = SystemColors.GradientInactiveCaption;
            GGs2.Location = new Point(1302, 120);
            GGs2.Name = "GGs2";
            GGs2.Size = new Size(100, 50);
            GGs2.TabIndex = 14;
            GGs2.Tag = "68";
            GGs2.Text = "G#";
            GGs2.UseVisualStyleBackColor = false;
            // 
            // DGs1
            // 
            DGs1.BackColor = SystemColors.GradientInactiveCaption;
            DGs1.Location = new Point(604, 176);
            DGs1.Name = "DGs1";
            DGs1.Size = new Size(100, 50);
            DGs1.TabIndex = 7;
            DGs1.Tag = "56";
            DGs1.Text = "G#";
            DGs1.UseVisualStyleBackColor = false;
            // 
            // BEb2
            // 
            BEb2.BackColor = SystemColors.GradientInactiveCaption;
            BEb2.Location = new Point(1600, 64);
            BEb2.Name = "BEb2";
            BEb2.Size = new Size(100, 50);
            BEb2.TabIndex = 17;
            BEb2.Tag = "75";
            BEb2.Text = "Eb";
            BEb2.UseVisualStyleBackColor = false;
            // 
            // DG1
            // 
            DG1.BackColor = SystemColors.ControlLightLight;
            DG1.Location = new Point(504, 176);
            DG1.Name = "DG1";
            DG1.Size = new Size(100, 50);
            DG1.TabIndex = 6;
            DG1.Tag = "55";
            DG1.Text = "G";
            DG1.UseVisualStyleBackColor = false;
            // 
            // GG2
            // 
            GG2.BackColor = SystemColors.Info;
            GG2.Location = new Point(1202, 120);
            GG2.Name = "GG2";
            GG2.Size = new Size(100, 50);
            GG2.TabIndex = 13;
            GG2.Tag = "67";
            GG2.Text = "G";
            GG2.UseVisualStyleBackColor = false;
            // 
            // DFs1
            // 
            DFs1.BackColor = SystemColors.GradientInactiveCaption;
            DFs1.Location = new Point(404, 173);
            DFs1.Name = "DFs1";
            DFs1.Size = new Size(100, 50);
            DFs1.TabIndex = 5;
            DFs1.Tag = "54";
            DFs1.Text = "F#";
            DFs1.UseVisualStyleBackColor = false;
            // 
            // BD2
            // 
            BD2.BackColor = SystemColors.GradientInactiveCaption;
            BD2.Location = new Point(1500, 64);
            BD2.Name = "BD2";
            BD2.Size = new Size(100, 50);
            BD2.TabIndex = 16;
            BD2.Tag = "74";
            BD2.Text = "D";
            BD2.UseVisualStyleBackColor = false;
            // 
            // DF1
            // 
            DF1.BackColor = SystemColors.GradientInactiveCaption;
            DF1.Location = new Point(304, 173);
            DF1.Name = "DF1";
            DF1.Size = new Size(100, 50);
            DF1.TabIndex = 4;
            DF1.Tag = "53";
            DF1.Text = "F";
            DF1.UseVisualStyleBackColor = false;
            // 
            // GFs1
            // 
            GFs1.BackColor = SystemColors.GradientInactiveCaption;
            GFs1.Location = new Point(1102, 117);
            GFs1.Name = "GFs1";
            GFs1.Size = new Size(100, 50);
            GFs1.TabIndex = 12;
            GFs1.Tag = "66";
            GFs1.Text = "F#";
            GFs1.UseVisualStyleBackColor = false;
            // 
            // DE1
            // 
            DE1.BackColor = SystemColors.GradientInactiveCaption;
            DE1.Location = new Point(204, 173);
            DE1.Name = "DE1";
            DE1.Size = new Size(100, 50);
            DE1.TabIndex = 3;
            DE1.Tag = "52";
            DE1.Text = "E";
            DE1.UseVisualStyleBackColor = false;
            // 
            // Deb1
            // 
            Deb1.BackColor = SystemColors.GradientInactiveCaption;
            Deb1.Location = new Point(104, 173);
            Deb1.Name = "Deb1";
            Deb1.Size = new Size(100, 50);
            Deb1.TabIndex = 2;
            Deb1.Tag = "51";
            Deb1.Text = "Eb";
            Deb1.UseVisualStyleBackColor = false;
            // 
            // BCs2
            // 
            BCs2.BackColor = SystemColors.GradientInactiveCaption;
            BCs2.Location = new Point(1402, 64);
            BCs2.Name = "BCs2";
            BCs2.Size = new Size(100, 50);
            BCs2.TabIndex = 15;
            BCs2.Tag = "73";
            BCs2.Text = "C#";
            BCs2.UseVisualStyleBackColor = false;
            // 
            // DD1
            // 
            DD1.BackColor = SystemColors.Info;
            DD1.Location = new Point(4, 173);
            DD1.Name = "DD1";
            DD1.Size = new Size(100, 50);
            DD1.TabIndex = 1;
            DD1.Tag = "50";
            DD1.Text = "D";
            DD1.UseVisualStyleBackColor = false;
            // 
            // GF1
            // 
            GF1.BackColor = SystemColors.ControlLightLight;
            GF1.Location = new Point(1002, 117);
            GF1.Name = "GF1";
            GF1.Size = new Size(100, 50);
            GF1.TabIndex = 11;
            GF1.Tag = "65";
            GF1.Text = "F";
            GF1.UseVisualStyleBackColor = false;
            // 
            // BC2
            // 
            BC2.BackColor = SystemColors.GradientInactiveCaption;
            BC2.Location = new Point(1302, 64);
            BC2.Name = "BC2";
            BC2.Size = new Size(100, 50);
            BC2.TabIndex = 14;
            BC2.Tag = "72";
            BC2.Text = "C";
            BC2.UseVisualStyleBackColor = false;
            // 
            // GE1
            // 
            GE1.BackColor = SystemColors.GradientInactiveCaption;
            GE1.Location = new Point(902, 117);
            GE1.Name = "GE1";
            GE1.Size = new Size(100, 50);
            GE1.TabIndex = 10;
            GE1.Tag = "64";
            GE1.Text = "E";
            GE1.UseVisualStyleBackColor = false;
            // 
            // BB2
            // 
            BB2.BackColor = SystemColors.Info;
            BB2.Location = new Point(1202, 61);
            BB2.Name = "BB2";
            BB2.Size = new Size(100, 50);
            BB2.TabIndex = 13;
            BB2.Tag = "71";
            BB2.Text = "B";
            BB2.UseVisualStyleBackColor = false;
            // 
            // GEb1
            // 
            GEb1.BackColor = SystemColors.GradientInactiveCaption;
            GEb1.Location = new Point(802, 117);
            GEb1.Name = "GEb1";
            GEb1.Size = new Size(100, 50);
            GEb1.TabIndex = 9;
            GEb1.Tag = "63";
            GEb1.Text = "Eb";
            GEb1.UseVisualStyleBackColor = false;
            // 
            // BBb1
            // 
            BBb1.BackColor = SystemColors.GradientInactiveCaption;
            BBb1.Location = new Point(1102, 61);
            BBb1.Name = "BBb1";
            BBb1.Size = new Size(100, 50);
            BBb1.TabIndex = 12;
            BBb1.Tag = "70";
            BBb1.Text = "Bb";
            BBb1.UseVisualStyleBackColor = false;
            // 
            // GD1
            // 
            GD1.BackColor = SystemColors.GradientInactiveCaption;
            GD1.Location = new Point(702, 117);
            GD1.Name = "GD1";
            GD1.Size = new Size(100, 50);
            GD1.TabIndex = 8;
            GD1.Tag = "62";
            GD1.Text = "D";
            GD1.UseVisualStyleBackColor = false;
            // 
            // BA1
            // 
            BA1.BackColor = SystemColors.ControlLightLight;
            BA1.Location = new Point(1002, 61);
            BA1.Name = "BA1";
            BA1.Size = new Size(100, 50);
            BA1.TabIndex = 11;
            BA1.Tag = "69";
            BA1.Text = "A";
            BA1.UseVisualStyleBackColor = false;
            // 
            // GCs1
            // 
            GCs1.BackColor = SystemColors.GradientInactiveCaption;
            GCs1.Location = new Point(604, 117);
            GCs1.Name = "GCs1";
            GCs1.Size = new Size(100, 50);
            GCs1.TabIndex = 7;
            GCs1.Tag = "61";
            GCs1.Text = "C#";
            GCs1.UseVisualStyleBackColor = false;
            // 
            // BGs1
            // 
            BGs1.BackColor = SystemColors.GradientInactiveCaption;
            BGs1.Location = new Point(902, 61);
            BGs1.Name = "BGs1";
            BGs1.Size = new Size(100, 50);
            BGs1.TabIndex = 10;
            BGs1.Tag = "68";
            BGs1.Text = "G#";
            BGs1.UseVisualStyleBackColor = false;
            // 
            // GC1
            // 
            GC1.BackColor = SystemColors.ControlLightLight;
            GC1.Location = new Point(504, 117);
            GC1.Name = "GC1";
            GC1.Size = new Size(100, 50);
            GC1.TabIndex = 6;
            GC1.Tag = "60";
            GC1.Text = "C";
            GC1.UseVisualStyleBackColor = false;
            // 
            // BG1
            // 
            BG1.BackColor = SystemColors.GradientInactiveCaption;
            BG1.Location = new Point(802, 58);
            BG1.Name = "BG1";
            BG1.Size = new Size(100, 50);
            BG1.TabIndex = 9;
            BG1.Tag = "67";
            BG1.Text = "G";
            BG1.UseVisualStyleBackColor = false;
            // 
            // GB1
            // 
            GB1.BackColor = SystemColors.GradientInactiveCaption;
            GB1.Location = new Point(404, 117);
            GB1.Name = "GB1";
            GB1.Size = new Size(100, 50);
            GB1.TabIndex = 5;
            GB1.Tag = "59";
            GB1.Text = "B";
            GB1.UseVisualStyleBackColor = false;
            // 
            // BFs1
            // 
            BFs1.BackColor = SystemColors.GradientInactiveCaption;
            BFs1.Location = new Point(702, 58);
            BFs1.Name = "BFs1";
            BFs1.Size = new Size(100, 50);
            BFs1.TabIndex = 8;
            BFs1.Tag = "66";
            BFs1.Text = "F#";
            BFs1.UseVisualStyleBackColor = false;
            // 
            // GBb1
            // 
            GBb1.BackColor = SystemColors.GradientInactiveCaption;
            GBb1.Location = new Point(304, 117);
            GBb1.Name = "GBb1";
            GBb1.Size = new Size(100, 50);
            GBb1.TabIndex = 4;
            GBb1.Tag = "58";
            GBb1.Text = "Bb";
            GBb1.UseVisualStyleBackColor = false;
            // 
            // BF1
            // 
            BF1.BackColor = SystemColors.GradientInactiveCaption;
            BF1.Location = new Point(602, 58);
            BF1.Name = "BF1";
            BF1.Size = new Size(100, 50);
            BF1.TabIndex = 7;
            BF1.Tag = "65";
            BF1.Text = "F";
            BF1.UseVisualStyleBackColor = false;
            // 
            // GA1
            // 
            GA1.BackColor = SystemColors.GradientInactiveCaption;
            GA1.Location = new Point(204, 117);
            GA1.Name = "GA1";
            GA1.Size = new Size(100, 50);
            GA1.TabIndex = 3;
            GA1.Tag = "57";
            GA1.Text = "A";
            GA1.UseVisualStyleBackColor = false;
            // 
            // BE1
            // 
            BE1.BackColor = SystemColors.ControlLightLight;
            BE1.Location = new Point(502, 58);
            BE1.Name = "BE1";
            BE1.Size = new Size(100, 50);
            BE1.TabIndex = 6;
            BE1.Tag = "64";
            BE1.Text = "E";
            BE1.UseVisualStyleBackColor = false;
            // 
            // GGs1
            // 
            GGs1.BackColor = SystemColors.GradientInactiveCaption;
            GGs1.Location = new Point(104, 114);
            GGs1.Name = "GGs1";
            GGs1.Size = new Size(100, 50);
            GGs1.TabIndex = 2;
            GGs1.Tag = "56";
            GGs1.Text = "G#";
            GGs1.UseVisualStyleBackColor = false;
            // 
            // GG1
            // 
            GG1.BackColor = SystemColors.Info;
            GG1.Location = new Point(4, 114);
            GG1.Name = "GG1";
            GG1.Size = new Size(100, 50);
            GG1.TabIndex = 1;
            GG1.Tag = "55";
            GG1.Text = "G";
            GG1.UseVisualStyleBackColor = false;
            // 
            // BEb1
            // 
            BEb1.BackColor = SystemColors.GradientInactiveCaption;
            BEb1.Location = new Point(402, 58);
            BEb1.Name = "BEb1";
            BEb1.Size = new Size(100, 50);
            BEb1.TabIndex = 5;
            BEb1.Tag = "63";
            BEb1.Text = "Eb";
            BEb1.UseVisualStyleBackColor = false;
            // 
            // BD1
            // 
            BD1.BackColor = SystemColors.GradientInactiveCaption;
            BD1.Location = new Point(302, 58);
            BD1.Name = "BD1";
            BD1.Size = new Size(100, 50);
            BD1.TabIndex = 4;
            BD1.Tag = "62";
            BD1.Text = "D";
            BD1.UseVisualStyleBackColor = false;
            // 
            // BCs1
            // 
            BCs1.BackColor = SystemColors.GradientInactiveCaption;
            BCs1.Location = new Point(204, 58);
            BCs1.Name = "BCs1";
            BCs1.Size = new Size(100, 50);
            BCs1.TabIndex = 3;
            BCs1.Tag = "61";
            BCs1.Text = "C#";
            BCs1.UseVisualStyleBackColor = false;
            // 
            // BC1
            // 
            BC1.BackColor = SystemColors.GradientInactiveCaption;
            BC1.Location = new Point(104, 58);
            BC1.Name = "BC1";
            BC1.Size = new Size(100, 50);
            BC1.TabIndex = 2;
            BC1.Tag = "60";
            BC1.Text = "C";
            BC1.UseVisualStyleBackColor = false;
            // 
            // BB1
            // 
            BB1.BackColor = SystemColors.Info;
            BB1.Location = new Point(4, 58);
            BB1.Name = "BB1";
            BB1.Size = new Size(100, 50);
            BB1.TabIndex = 1;
            BB1.Tag = "59";
            BB1.Text = "B";
            BB1.UseVisualStyleBackColor = false;
            // 
            // HEB2
            // 
            HEB2.BackColor = SystemColors.GradientInactiveCaption;
            HEB2.Location = new Point(1900, 8);
            HEB2.Name = "HEB2";
            HEB2.Size = new Size(100, 50);
            HEB2.TabIndex = 20;
            HEB2.Tag = "83";
            HEB2.Text = "B";
            HEB2.UseVisualStyleBackColor = false;
            // 
            // HEBb2
            // 
            HEBb2.BackColor = SystemColors.GradientInactiveCaption;
            HEBb2.Location = new Point(1800, 8);
            HEBb2.Name = "HEBb2";
            HEBb2.Size = new Size(100, 50);
            HEBb2.TabIndex = 19;
            HEBb2.Tag = "82";
            HEBb2.Text = "Bb";
            HEBb2.UseVisualStyleBackColor = false;
            // 
            // HEA2
            // 
            HEA2.BackColor = SystemColors.GradientInactiveCaption;
            HEA2.Location = new Point(1700, 8);
            HEA2.Name = "HEA2";
            HEA2.Size = new Size(100, 50);
            HEA2.TabIndex = 18;
            HEA2.Tag = "81";
            HEA2.Text = "A";
            HEA2.UseVisualStyleBackColor = false;
            // 
            // HEGs2
            // 
            HEGs2.BackColor = SystemColors.GradientInactiveCaption;
            HEGs2.Location = new Point(1600, 8);
            HEGs2.Name = "HEGs2";
            HEGs2.Size = new Size(100, 50);
            HEGs2.TabIndex = 17;
            HEGs2.Tag = "80";
            HEGs2.Text = "G#";
            HEGs2.UseVisualStyleBackColor = false;
            // 
            // HEG2
            // 
            HEG2.BackColor = SystemColors.GradientInactiveCaption;
            HEG2.Location = new Point(1500, 8);
            HEG2.Name = "HEG2";
            HEG2.Size = new Size(100, 50);
            HEG2.TabIndex = 16;
            HEG2.Tag = "79";
            HEG2.Text = "G";
            HEG2.UseVisualStyleBackColor = false;
            // 
            // HEFs2
            // 
            HEFs2.BackColor = SystemColors.GradientInactiveCaption;
            HEFs2.Location = new Point(1400, 5);
            HEFs2.Name = "HEFs2";
            HEFs2.Size = new Size(100, 50);
            HEFs2.TabIndex = 15;
            HEFs2.Tag = "78";
            HEFs2.Text = "F#";
            HEFs2.UseVisualStyleBackColor = false;
            // 
            // HEF2
            // 
            HEF2.BackColor = SystemColors.GradientInactiveCaption;
            HEF2.Location = new Point(1300, 5);
            HEF2.Name = "HEF2";
            HEF2.Size = new Size(100, 50);
            HEF2.TabIndex = 14;
            HEF2.Tag = "77";
            HEF2.Text = "F";
            HEF2.UseVisualStyleBackColor = false;
            // 
            // HEE2
            // 
            HEE2.BackColor = SystemColors.Info;
            HEE2.Location = new Point(1200, 5);
            HEE2.Name = "HEE2";
            HEE2.Size = new Size(100, 50);
            HEE2.TabIndex = 13;
            HEE2.Tag = "76";
            HEE2.Text = "E";
            HEE2.UseVisualStyleBackColor = false;
            // 
            // HEEb1
            // 
            HEEb1.BackColor = SystemColors.GradientInactiveCaption;
            HEEb1.Location = new Point(1100, 5);
            HEEb1.Name = "HEEb1";
            HEEb1.Size = new Size(100, 50);
            HEEb1.TabIndex = 12;
            HEEb1.Tag = "75";
            HEEb1.Text = "Eb";
            HEEb1.UseVisualStyleBackColor = false;
            // 
            // HED1
            // 
            HED1.BackColor = SystemColors.ControlLightLight;
            HED1.Location = new Point(1000, 5);
            HED1.Name = "HED1";
            HED1.Size = new Size(100, 50);
            HED1.TabIndex = 11;
            HED1.Tag = "74";
            HED1.Text = "D";
            HED1.UseVisualStyleBackColor = false;
            // 
            // HECs1
            // 
            HECs1.BackColor = SystemColors.GradientInactiveCaption;
            HECs1.Location = new Point(902, 5);
            HECs1.Name = "HECs1";
            HECs1.Size = new Size(100, 50);
            HECs1.TabIndex = 10;
            HECs1.Tag = "73";
            HECs1.Text = "C#";
            HECs1.UseVisualStyleBackColor = false;
            // 
            // HEC1
            // 
            HEC1.BackColor = SystemColors.GradientInactiveCaption;
            HEC1.Location = new Point(802, 5);
            HEC1.Name = "HEC1";
            HEC1.Size = new Size(100, 50);
            HEC1.TabIndex = 9;
            HEC1.Tag = "72";
            HEC1.Text = "C";
            HEC1.UseVisualStyleBackColor = false;
            // 
            // HEB1
            // 
            HEB1.BackColor = SystemColors.GradientInactiveCaption;
            HEB1.Location = new Point(702, 5);
            HEB1.Name = "HEB1";
            HEB1.Size = new Size(100, 50);
            HEB1.TabIndex = 8;
            HEB1.Tag = "71";
            HEB1.Text = "B";
            HEB1.UseVisualStyleBackColor = false;
            // 
            // HEBb1
            // 
            HEBb1.BackColor = SystemColors.GradientInactiveCaption;
            HEBb1.Location = new Point(602, 5);
            HEBb1.Name = "HEBb1";
            HEBb1.Size = new Size(100, 50);
            HEBb1.TabIndex = 7;
            HEBb1.Tag = "70";
            HEBb1.Text = "Bb";
            HEBb1.UseVisualStyleBackColor = false;
            // 
            // HEA1
            // 
            HEA1.BackColor = SystemColors.ControlLightLight;
            HEA1.Location = new Point(502, 5);
            HEA1.Name = "HEA1";
            HEA1.Size = new Size(100, 50);
            HEA1.TabIndex = 6;
            HEA1.Tag = "69";
            HEA1.Text = "A";
            HEA1.UseVisualStyleBackColor = false;
            // 
            // HEGs1
            // 
            HEGs1.BackColor = SystemColors.GradientInactiveCaption;
            HEGs1.Location = new Point(402, 2);
            HEGs1.Name = "HEGs1";
            HEGs1.Size = new Size(100, 50);
            HEGs1.TabIndex = 5;
            HEGs1.Tag = "68";
            HEGs1.Text = "G#";
            HEGs1.UseVisualStyleBackColor = false;
            // 
            // HEG1
            // 
            HEG1.BackColor = SystemColors.GradientInactiveCaption;
            HEG1.Location = new Point(302, 2);
            HEG1.Name = "HEG1";
            HEG1.Size = new Size(100, 50);
            HEG1.TabIndex = 4;
            HEG1.Tag = "67";
            HEG1.Text = "G";
            HEG1.UseVisualStyleBackColor = false;
            // 
            // HEFs1
            // 
            HEFs1.BackColor = SystemColors.GradientInactiveCaption;
            HEFs1.Location = new Point(202, 2);
            HEFs1.Name = "HEFs1";
            HEFs1.Size = new Size(100, 50);
            HEFs1.TabIndex = 3;
            HEFs1.Tag = "66";
            HEFs1.Text = "F#";
            HEFs1.UseVisualStyleBackColor = false;
            // 
            // HEF1
            // 
            HEF1.BackColor = SystemColors.GradientInactiveCaption;
            HEF1.Location = new Point(102, 2);
            HEF1.Name = "HEF1";
            HEF1.Size = new Size(100, 50);
            HEF1.TabIndex = 2;
            HEF1.Tag = "65";
            HEF1.Text = "F";
            HEF1.UseVisualStyleBackColor = false;
            // 
            // HEE1
            // 
            HEE1.BackColor = SystemColors.Info;
            HEE1.Location = new Point(2, 2);
            HEE1.Name = "HEE1";
            HEE1.Size = new Size(100, 50);
            HEE1.TabIndex = 1;
            HEE1.Tag = "64";
            HEE1.Text = "E";
            HEE1.UseVisualStyleBackColor = false;
            // 
            // guitarRollPanel
            // 
            guitarRollPanel.Location = new Point(35, 27);
            guitarRollPanel.Name = "guitarRollPanel";
            guitarRollPanel.Size = new Size(2000, 600);
            guitarRollPanel.TabIndex = 6;
            guitarRollPanel.Paint += guitarRollPanel_Paint;
            guitarRollPanel.MouseClick += this.guitarRollPanel_MouseClick;
            // 
            // velocityPanel
            // 
            velocityPanel.Location = new Point(35, 664);
            velocityPanel.Name = "velocityPanel";
            velocityPanel.Size = new Size(2000, 500);
            velocityPanel.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.Controls.Add(guitarRollPanel);
            panel1.Controls.Add(velocityPanel);
            panel1.Location = new Point(128, 704);
            panel1.Name = "panel1";
            panel1.Size = new Size(2073, 1227);
            panel1.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(19F, 47F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2804, 2012);
            Controls.Add(panel1);
            Controls.Add(fretboard);
            Controls.Add(label2);
            Controls.Add(EnvelopeLengthSlider);
            Controls.Add(label1);
            Controls.Add(lowPassFilterAlpha);
            Controls.Add(buttonPlayMidi);
            Name = "Form1";
            Tag = "40";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)lowPassFilterAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)EnvelopeLengthSlider).EndInit();
            fretboard.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonPlayMidi;
        private TrackBar lowPassFilterAlpha;
        private Label label1;
        private TrackBar EnvelopeLengthSlider;
        private Label label2;
        private Panel fretboard;
        private Button HEFs1;
        private Button HEF1;
        private Button HEE1;
        private Button HEGs1;
        private Button HEG1;
        private Button HECs1;
        private Button HEC1;
        private Button HEB1;
        private Button HEBb1;
        private Button HEA1;
        private Button HEB2;
        private Button HEBb2;
        private Button HEA2;
        private Button HEGs2;
        private Button HEG2;
        private Button HEFs2;
        private Button HEF2;
        private Button HEE2;
        private Button HEEb1;
        private Button HED1;
        private Button LEB2;
        private Button AE2;
        private Button LEBb2;
        private Button AD2;
        private Button LEA2;
        private Button AEb2;
        private Button LEGS2;
        private Button DA2;
        private Button LEG2;
        private Button ACs2;
        private Button LEFS2;
        private Button DGs2;
        private Button LEF2;
        private Button AC2;
        private Button LEE2;
        private Button DG2;
        private Button LEEb1;
        private Button AB2;
        private Button LED1;
        private Button DFs2;
        private Button LECS1;
        private Button ABb2;
        private Button LEC1;
        private Button LEB1;
        private Button AA2;
        private Button LEBb1;
        private Button DF2;
        private Button LEA1;
        private Button AGs1;
        private Button LEGS1;
        private Button GCs2;
        private Button LEG1;
        private Button AG1;
        private Button LEFS1;
        private Button LEF1;
        private Button DE2;
        private Button LEE1;
        private Button AFs1;
        private Button GC2;
        private Button AF1;
        private Button DEb2;
        private Button AE1;
        private Button GB2;
        private Button AEb1;
        private Button DD2;
        private Button AD1;
        private Button BFs2;
        private Button ACs1;
        private Button DCs1;
        private Button AC1;
        private Button GBb2;
        private Button AB1;
        private Button DC1;
        private Button ABb1;
        private Button AA1;
        private Button BF2;
        private Button DB1;
        private Button GA2;
        private Button DBb1;
        private Button BE2;
        private Button DA1;
        private Button GGs2;
        private Button DGs1;
        private Button BEb2;
        private Button DG1;
        private Button GG2;
        private Button DFs1;
        private Button BD2;
        private Button DF1;
        private Button GFs1;
        private Button DE1;
        private Button Deb1;
        private Button BCs2;
        private Button DD1;
        private Button GF1;
        private Button BC2;
        private Button GE1;
        private Button BB2;
        private Button GEb1;
        private Button BBb1;
        private Button GD1;
        private Button BA1;
        private Button GCs1;
        private Button BGs1;
        private Button GC1;
        private Button BG1;
        private Button GB1;
        private Button BFs1;
        private Button GBb1;
        private Button BF1;
        private Button GA1;
        private Button BE1;
        private Button GGs1;
        private Button GG1;
        private Button BEb1;
        private Button BD1;
        private Button BCs1;
        private Button BC1;
        private Button BB1;
        private Button GD2;
        private Panel guitarRollPanel;
        private Panel velocityPanel;
        private Panel panel1;
    }
}