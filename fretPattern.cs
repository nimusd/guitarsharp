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
        public int BaseFret { get; set; } = 0;
        public bool IsActive { get; set; }
        private Form1 form1Instance;
        public List<Button> Buttons { get; private set; } = new List<Button>();
        

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
            

            
        }


        public void Deactivate()
        {
            IsActive = false;
           

            
        }



        public void CreateFretboard(int numberOfStrings, int numberOfFrets, int baseFret, Point location, int patternNumber)
        {
            int buttonWidth = 50; // Width of each button
            int buttonHeight = 100; // Height of each button


            // Create fret buttons
            for (int fretIndex = 0; fretIndex < numberOfFrets; fretIndex++)
            {
                for (int stringIndex = 0; stringIndex < numberOfStrings; stringIndex++)
                {
                    Button fretButton = new Button();
                    fretButton.Width = buttonWidth;
                    fretButton.Height = buttonHeight;
                    fretButton.BackColor = Color.Beige;
                    //fretButton.Tag = new Tuple<int, int>(stringIndex, fretIndex);
                    // Invert stringIndex for the UI representation
                    fretButton.Tag = new Tuple<int, int>(numberOfStrings - 1 - stringIndex, fretIndex);

                    fretButton.Location = new Point((stringIndex * buttonWidth) + location.X, (fretIndex * buttonHeight) + location.Y);
                    fretButton.Text = GetNoteName(stringIndex, (fretIndex + baseFret) );
                    fretButton.Click += FretButton_Click;
                    Buttons.Add(fretButton);
                }
            }


        }

        private void BaseFretUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown baseFretUpDown = sender as NumericUpDown;
            if (baseFretUpDown != null)
            {
                int newBaseFret = (int)baseFretUpDown.Value;
                int fretChange = newBaseFret - BaseFret; // Calculate the change in frets

                foreach (Button button in Buttons)
                {
                    Tuple<int, int> tag = (Tuple<int, int>)button.Tag;
                    int stringIndex = tag.Item1;
                    int fretIndex = tag.Item2 + fretChange; // Update the fret index based on the change

                    // Ensure fretIndex stays within valid range
                    fretIndex = Math.Max(0, fretIndex);
                    fretIndex = Math.Min(fretIndex, 23); // Assuming a maximum of 24 frets

                    // Update the button tag and text with the new fret index
                    button.Tag = new Tuple<int, int>(stringIndex, fretIndex);
                    button.Text = GetNoteName(stringIndex, fretIndex);
                }

                BaseFret = newBaseFret; // Update the BaseFret property to the new value
            }
        }

        public void BaseFretValueChanged(int newBaseFret)
        {
            
          
               
                int fretChange = newBaseFret - BaseFret; // Calculate the change in frets

                foreach (Button button in Buttons)
                {
                    Tuple<int, int> tag = (Tuple<int, int>)button.Tag;
                    int stringIndex = tag.Item1;
                    int fretIndex = tag.Item2 + fretChange; // Update the fret index based on the change

                    // Ensure fretIndex stays within valid range
                    fretIndex = Math.Max(0, fretIndex);
                    fretIndex = Math.Min(fretIndex, 23); // Assuming a maximum of 24 frets

                    // Update the button tag and text with the new fret index
                    button.Tag = new Tuple<int, int>(stringIndex, fretIndex);
                    button.Text = GetNoteName(stringIndex, fretIndex);
                }

                BaseFret = newBaseFret; // Update the BaseFret property to the new value
            
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
                        button.BackColor =  Color.Beige; // Reset color
                    }
                }

                // Toggle the selection state of the clicked button            
                
                    clickedButton.BackColor = clickedButton.BackColor == Color.Beige ? Color.Bisque : Color.Beige;
                
            }
        }


      



        public string GetNoteName(int stringIndex, int fret)
        {
            // Assuming standard tuning (EADGBE)
            int[] stringNotes = { 4, 9, 2, 7, 11, 4 }; // E, A, D, G, B, E
            string[] noteNames = {  "C" + Environment.NewLine + " ",
                                    "C" + Environment.NewLine + "#", 
                                    "D"+ Environment.NewLine + " ", 
                                    "D"+ Environment.NewLine + "#", 
                                    "E" + Environment.NewLine + " ", 
                                    "F" + Environment.NewLine + " ", 
                                    "F"+ Environment.NewLine + "#", 
                                    "G" + Environment.NewLine + " ", 
                                    "G"+ Environment.NewLine + "#", 
                                    "A" + Environment.NewLine + " ", 
                                    "A"+ Environment.NewLine + "#", 
                                    "B" + Environment.NewLine + " " };

            int noteIndex = (stringNotes[stringIndex] + fret) % 12;
            return noteNames[noteIndex];
        }

        public FretPatternData GetSerializableData()
        {
            var data = new FretPatternData(Name, Description, BaseFret,  IsActive);



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
       
        public List<FretTag> FretTags { get; set; } = new();
        
       

        // Parameterless constructor for deserialization
        public FretPatternData() { }

        // Constructor with parameters
        public FretPatternData(string name, string description, int baseFret,  bool isActive)
        {
            Name = name;
            Description = description;
            BaseFret = baseFret;
            IsActive = isActive;
           
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
