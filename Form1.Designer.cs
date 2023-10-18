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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(19F, 47F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(3180, 2082);
            Controls.Add(buttonPlayMidi);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonPlayMidi;
    }
}