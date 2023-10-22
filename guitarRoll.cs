


namespace Guitarsharp
{
    using NAudio.Midi;
    using NAudio.Wave;
   
    public partial class Form1 : Form
    {
        private void guitarRollPanel_Paint(object sender, PaintEventArgs e)
        {
            int numberOfStrings = 6;
            int laneHeight = guitarRollPanel.Height / numberOfStrings;

            // Clear the panel
            e.Graphics.Clear(guitarRollPanel.BackColor);

            // Draw the horizontal lines for each string
            for (int i = 1; i < numberOfStrings; i++)
            {
                e.Graphics.DrawLine(Pens.Gray, 0, i * laneHeight, guitarRollPanel.Width, i * laneHeight);
            }

            // Draw each note in the composition
            foreach (Note note in composition.Notes)
            {
                DrawNoteOnGuitarRoll(note, e.Graphics);
            }

            // Calculate the position of the "Now Time" line based solely on currentTime
            
            int nowTimeX = (int)(currentTime * PixelsPerSecond) + guitarRollPanel.AutoScrollPosition.X;


            // Draw the "Now Time" line
            e.Graphics.DrawLine(Pens.Red, nowTimeX, 0, nowTimeX, guitarRollPanel.Height);
        }






        private void guitarRollPanel_Scroll(object sender, ScrollEventArgs e)
        {
            guitarRollPanel.Refresh(); // Force a complete redraw
        }

        private int GetStringNumberFromButtonY(int yPosition)
        {
            if (yPosition >= 0 && yPosition < 50) return 0;   // High E string
            if (yPosition >= 50 && yPosition < 100) return 1; // B string
            if (yPosition >= 100 && yPosition < 150) return 2; // G string
            if (yPosition >= 150 && yPosition < 200) return 3; // D string
            if (yPosition >= 200 && yPosition < 250) return 4; // A string
            if (yPosition >= 250 && yPosition < 300) return 5; // Low E string

            throw new Exception("Invalid Y position for string determination.");
        }

        private void DrawNoteOnGuitarRoll(Note note, Graphics g)
        {
            int laneHeight = guitarRollPanel.Height / 6; // Assuming 6 strings

            // Convert the note's start and end times to pixel values using the scale
            int noteStartX = (int)(note.StartTime * PixelsPerSecond) + guitarRollPanel.AutoScrollPosition.X;

            //int noteStartX = (int)(note.StartTime * PixelsPerSecond);
            int noteWidth = (int)((note.EndTime - note.StartTime) * PixelsPerSecond);

            // Calculate the position and size of the rectangle
            note.DrawingRectangle = new Rectangle(noteStartX, note.StringNumber * laneHeight, noteWidth, laneHeight);

            // Draw the rectangle on the guitarRollPanel
            g.FillRectangle(Brushes.Blue, note.DrawingRectangle); // Fill the rectangle with a color
            g.DrawRectangle(Pens.Black, note.DrawingRectangle); // Draw the rectangle border

            // Draw the note number inside the rectangle
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(note.MidiNoteNumber.ToString(), new Font("Arial", 12), Brushes.White, note.DrawingRectangle, stringFormat);
        }

        private void FretboardButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Tag.ToString(), out int midiNoteNumber))
            {
                // Determine the fret and string from the button's name or another property
                int stringNumber = GetStringNumberFromButtonY(button.Top);

                // Create a new Note object with the current time as the start time
                Note newNote = new Note(stringNumber, 0, currentTime, currentTime + 1.0f, 0, 0, midiNoteNumber); // Adjust the parameters as needed

                // Add the note to the composition
                composition.Notes.Add(newNote);

                // Update the currentTime to the end time of the new note
                currentTime = (float)newNote.EndTime;
               // MessageBox.Show(currentTime.ToString() + "fret ");
                // Draw the note on the guitarRollPanel
                using (Graphics g = guitarRollPanel.CreateGraphics())
                {
                    DrawNoteOnGuitarRoll(newNote, g);
                }

                // Play the note (if you want this functionality)
                PlayFromFretboard(midiNoteNumber);
                guitarRollPanel.Invalidate(true);
            }
        }



        private List<Note> notes = new List<Note>();
       
        // This is just a placeholder. You'll need to implement this method based on your requirements.
        private int GetFretNumber(int xPosition)
        {
            // Implement logic to determine the fret number based on the X position
            return 0;
        }




        private void ShiftNotesByDurationWithoutDrawing()
        {
            // Determine the shift amount based on the currently selected note duration
            float noteDurationInSeconds = 1.0f; // Adjust based on your current note duration
            float shiftAmount = noteDurationInSeconds * PixelsPerSecond;

            // Increment the currentTime by the note duration
            currentTime += noteDurationInSeconds;
           // MessageBox.Show(currentTime.ToString() + " shift");
            foreach (Note note in composition.Notes)
            {
                note.StartTime -= shiftAmount / PixelsPerSecond; // Convert pixels to seconds
                note.EndTime -= shiftAmount / PixelsPerSecond;   // Convert pixels to seconds
            }

            // Update the AutoScrollPosition to center around the currentTime
            int currentPixelPosition = (int)(currentTime * PixelsPerSecond);
            int newScrollPosition = Math.Max(0, currentPixelPosition - guitarRollPanel.Width / 2);
            guitarRollPanel.AutoScrollPosition = new Point(newScrollPosition, 0);

        }




    }



}



