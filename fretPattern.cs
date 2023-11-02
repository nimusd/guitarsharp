using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Guitarsharp
{
    [Serializable]
    public class FretPattern
    {
        public int[][] Pattern { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseFret { get; set; }
        public bool IsActive { get; private set; }
        private Form1 form1Instance;
        public List<Button> Buttons { get; private set; } = new List<Button>();
        public Button ActivateButton { get; private set; } = new Button { Text = "Activate" };
        public NumericUpDown BaseFretControl { get; private set; } = new NumericUpDown();

       // private string[] notes = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };

        public FretPattern(string name, int baseFret, Form1 form1Instance)
        {
            Name = name;
            BaseFret = baseFret;
            IsActive = false;
            this.form1Instance = form1Instance;
        }

        public void Activate()
        {
            IsActive = true;
            ActivateButton.BackColor = Color.Red; // Change the background color to red

            // Update fretboard display logic here
        }


        public void Deactivate()
        {
            IsActive = false;
            ActivateButton.BackColor = Color.Beige; // Reset the background color

            // Update fretboard display logic here
        }


        public void LoadPattern(string filePath)
        {
            // Load pattern from file
        }

        public void SavePattern(string filePath)
        {
            // Save pattern to file
        }

        public void CreateFretboard(int numberOfStrings, int numberOfFrets, int baseFret, Point location, int fretboardPatternNumber)
        {
            int buttonWidth = 50; // Width of each button
            int buttonHeight = 100; // Height of each button
            int controlSpacing = 20; // Spacing between fret buttons and controls

            // Create fret buttons
            for (int fretIndex = 0; fretIndex < numberOfFrets; fretIndex++)
            {
                for (int stringIndex = 0; stringIndex < numberOfStrings; stringIndex++)
                {
                    Button fretButton = new Button();
                    fretButton.Width = buttonWidth;
                    fretButton.Height = buttonHeight;
                    fretButton.BackColor = Color.Beige;
                    fretButton.Tag = new Tuple<int, int>(stringIndex, fretIndex + baseFret); // Tag stores string and fret information
                    fretButton.Location = new Point((stringIndex * buttonWidth) + location.X, (fretIndex * buttonHeight) + location.Y);
                    fretButton.Text = GetNoteName(stringIndex, fretIndex + baseFret);
                    fretButton.Click += FretButton_Click;
                    Buttons.Add(fretButton);
                }
            }

            // Create activation button
            ActivateButton.Location = new Point(location.X, (numberOfFrets * buttonHeight) + location.Y + controlSpacing);
            ActivateButton.Width = numberOfStrings * buttonWidth;
            ActivateButton.Height = buttonHeight / 2;
            ActivateButton.BackColor = Color.Beige;
            ActivateButton.Click += ActivateButton_Click;
            ActivateButton.Tag = fretboardPatternNumber;
            // Configure the numeric up/down control for base fret
            BaseFretControl.Location = new Point(location.X, location.Y + (numberOfFrets * buttonHeight) + ActivateButton.Height+50);
            BaseFretControl.Width = 100;
            BaseFretControl.Minimum = 0;
            BaseFretControl.Maximum = 24; // Assuming a maximum of 24 frets
            BaseFretControl.Value = baseFret;
            BaseFretControl.ValueChanged += BaseFretUpDown_ValueChanged;
        

    }
        private void BaseFretUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown baseFretUpDown = sender as NumericUpDown;
            if (baseFretUpDown != null)
            {
                int newBaseFret = (int)baseFretUpDown.Value;

                foreach (Button button in Buttons)
                {
                    Tuple<int, int> tag = (Tuple<int, int>)button.Tag;
                    int stringIndex = tag.Item1;
                    int fretIndex = tag.Item2;

                    string noteName = GetNoteName(stringIndex, fretIndex + newBaseFret );
                    button.Text = noteName;
                }
            }
        }

        private void FretButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Tuple<int, int> tag = (Tuple<int, int>)clickedButton.Tag;
                int stringIndex = tag.Item1;

                // Deselect other buttons on the same string
                foreach (Button button in Buttons)
                {
                    Tuple<int, int> buttonTag = (Tuple<int, int>)button.Tag;
                    if (buttonTag.Item1 == stringIndex && button != clickedButton)
                    {
                        button.BackColor = Color.Beige;
                    }
                }

                // Toggle the selection state of the clicked button
                clickedButton.BackColor = clickedButton.BackColor == Color.Beige ? Color.Bisque : Color.Beige;
            }
        }

        private void ActivateButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int patternNumber = (int) clickedButton.Tag;

            if (clickedButton != null)
            {
                Debug.WriteLine(clickedButton.Text);
                clickedButton.BackColor = Color.Red;
                clickedButton.Text = "Active";
                // Activate the clicked pattern
                // clickedPattern.Activate();

                
               // FretPattern clickedPattern = clickedButton.Tag as FretPattern;

                    
                    // Deactivate other patterns using Form1Instance
                    foreach (var pattern in form1Instance.allFretPatterns)
                    {
                        Debug.WriteLine("deactivate");
 
                           if (pattern.ActivateButton.Tag != clickedButton.Tag)
                            pattern.Deactivate();
                        
                    }

                // Activate the clicked pattern
                form1Instance.allFretPatterns[(int) ActivateButton.Tag].Activate();

            
            }
        }



        private string GetNoteName(int stringIndex, int fret)
        {
            // Assuming standard tuning (EADGBE)
            int[] stringNotes = { 4, 9, 2, 7, 11, 4 }; // E, A, D, G, B, E
            string[] noteNames = { "C", "#", "D", "#", "E", "F", "#", "G", "#", "A", "#", "B" };

            int noteIndex = (stringNotes[stringIndex] + fret) % 12;
            return noteNames[noteIndex];
        }
    }
}
