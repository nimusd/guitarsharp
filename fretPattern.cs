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

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public int BaseFret { get; set; }
        public bool IsActive { get; set; }
        private Form1 form1Instance;
        public List<Button> Buttons { get; private set; } = new List<Button>();
        public Button ActivateButton { get; private set; } = new Button { Text = "Activate" };
        public NumericUpDown BaseFretControl { get; private set; } = new NumericUpDown();

        public List<Button> FrettedNoteButtons { get; set; } = new();



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



        public void CreateFretboard(int numberOfStrings, int numberOfFrets, int baseFret, Point location, int patternNumber)
        {
            int buttonWidth = 50; // Width of each button
            int buttonHeight = 100; // Height of each button

            // Create open string buttons
            for (int stringIndex = 0; stringIndex < numberOfStrings; stringIndex++)
            {
                Button openStringButton = new Button();
                openStringButton.Width = buttonWidth;
                openStringButton.Height = buttonHeight;
                openStringButton.BackColor = Color.LightBlue;
                openStringButton.Tag = new Tuple<int, int>(stringIndex, -1); // -1 indicates open string
                openStringButton.Location = new Point((stringIndex * buttonWidth) + location.X, location.Y - buttonHeight - 10);
                openStringButton.Text = GetNoteName(stringIndex, 0); // Open string note
                openStringButton.Click += FretButton_Click;
                Buttons.Add(openStringButton);
            }

            // Create fret buttons
            for (int fretIndex = 0; fretIndex < numberOfFrets; fretIndex++)
            {
                for (int stringIndex = 0; stringIndex < numberOfStrings; stringIndex++)
                {
                    Button fretButton = new Button();
                    fretButton.Width = buttonWidth;
                    fretButton.Height = buttonHeight;
                    fretButton.BackColor = Color.Beige;
                    fretButton.Tag = new Tuple<int, int>(stringIndex, fretIndex);
                    fretButton.Location = new Point((stringIndex * buttonWidth) + location.X, (fretIndex * buttonHeight) + location.Y);
                    fretButton.Text = GetNoteName(stringIndex, (fretIndex + baseFret) + 1);
                    fretButton.Click += FretButton_Click;
                    Buttons.Add(fretButton);
                }
            }

            // Create and configure the activation button
            ActivateButton = new Button();
            ActivateButton.Width = 300;
            ActivateButton.Height = 50;
            ActivateButton.BackColor = Color.Beige;
            ActivateButton.Text = "Activate";
            ActivateButton.Tag = patternNumber;
            ActivateButton.Location = new Point(location.X, location.Y + (numberOfFrets * buttonHeight) + 25);
            ActivateButton.Click += ActivateButton_Click;

            // Create and configure the numeric up/down control for base fret
            BaseFretControl = new NumericUpDown();
            BaseFretControl.Location = new Point(location.X + 110, location.Y + (numberOfFrets * buttonHeight) + 100);
            BaseFretControl.Width = 100;
            BaseFretControl.Minimum = 1;
            BaseFretControl.Maximum = 24; // Assuming a maximum of 24 frets
            BaseFretControl.Value = baseFret > 0 ? baseFret : 1; // Ensure base fret is at least 1
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

                    if (fretIndex != -1) // Update note names only for non-open string buttons
                    {
                        string noteName = GetNoteName(stringIndex, fretIndex + newBaseFret);
                        button.Text = noteName;
                    }
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
                int fretIndex = tag.Item2;

                // Deselect other buttons on the same string
                foreach (Button button in Buttons)
                {
                    Tuple<int, int> buttonTag = (Tuple<int, int>)button.Tag;
                    if (buttonTag.Item1 == stringIndex && button != clickedButton)
                    {
                        button.BackColor = buttonTag.Item2 == -1 ? Color.LightBlue : Color.Beige; // Reset color based on fret index
                    }
                }

                // Toggle the selection state of the clicked button
                if (fretIndex == -1) // Open string button
                {
                    clickedButton.BackColor = clickedButton.BackColor == Color.LightBlue ? Color.Bisque : Color.LightBlue;
                }
                else // Regular fret button
                {
                    clickedButton.BackColor = clickedButton.BackColor == Color.Beige ? Color.Bisque : Color.Beige;
                }
            }
        }


        private void ActivateButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int patternNumber = (int)clickedButton.Tag;

            if (clickedButton != null)
            {
                Debug.WriteLine(clickedButton.Text);
                clickedButton.BackColor = Color.Red;
                clickedButton.Text = "Active";


                // Deactivate other patterns using Form1Instance
                foreach (var pattern in form1Instance.allFretPatterns)
                {
                    Debug.WriteLine("deactivate");

                    if (pattern.ActivateButton.Tag != clickedButton.Tag)
                        pattern.Deactivate();

                }

                // Activate the clicked pattern
                form1Instance.allFretPatterns[(int)ActivateButton.Tag].Activate();


            }
        }



        public string GetNoteName(int stringIndex, int fret)
        {
            // Assuming standard tuning (EADGBE)
            int[] stringNotes = { 4, 9, 2, 7, 11, 4 }; // E, A, D, G, B, E
            string[] noteNames = { "C", "#", "D", "#", "E", "F", "#", "G", "#", "A", "#", "B" };

            int noteIndex = (stringNotes[stringIndex] + fret) % 12;
            return noteNames[noteIndex];
        }

        public FretPatternData GetSerializableData()
        {
            var data = new FretPatternData(Name, Description, BaseFret, ActivateButton.Text, (int)ActivateButton.Tag, (int)ActivateButton.BackColor.ToArgb(), IsActive);



            foreach (var button in Buttons)
            {
                data.FretButtonBackColor.Add(button.BackColor.ToArgb());
                data.FretButtonName.Add(button.Text);

                // Assuming button.Tag is a Tuple<int, int> and FretTags is a List<Tuple<int, int>>
                if (button.Tag is Tuple<int, int> tag)
                {
                    // Assuming FretTag has a constructor that takes two ints
                    data.FretTags.Add(new FretTag(tag.Item1, tag.Item2));

                }

            }

            return data;
        }


    }




    [Serializable]
    public class FretPatternData
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BaseFret { get; set; }
        public bool IsActive { get; set; }
        public List<int> FretButtonBackColor { get; set; } = new();
        public List<string> FretButtonName { get; set; } = new();
        public string ActivateButtonText { get; set; } = string.Empty;
        public List<FretTag> FretTags { get; set; } = new();
        public int ActivateButtonTag { get; set; } = 0;
        public int ActivateButtonBackColor { get; set; } = 0;

        // Parameterless constructor for deserialization
        public FretPatternData() { }

        // Constructor with parameters
        public FretPatternData(string name, string description, int baseFret, string activateButtonText, int activateButtonTag, int activateButtonBackColor, bool isActive)
        {
            Name = name;
            Description = description;
            BaseFret = baseFret;
            IsActive = isActive;
            ActivateButtonText = activateButtonText;
            ActivateButtonTag = activateButtonTag;
            ActivateButtonBackColor = activateButtonBackColor;
        }
    }

    public class FretTag
    {
        public int StringIndex { get; set; }
        public int FretIndex { get; set; }

        public FretTag(int stringIndex, int fretIndex)
        {
            StringIndex = stringIndex;
            FretIndex = fretIndex;
        }

        // Parameterless constructor for deserialization
        public FretTag() { }
    }


}
