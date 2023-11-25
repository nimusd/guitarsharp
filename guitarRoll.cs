


namespace Guitarsharp
{
    using NAudio.Midi;
    using NAudio.Wave;
    using System.Diagnostics;

    public partial class Form1 : Form
    {
        private bool isDraggingVelocity = false;
        private List<Note> previouslySelectedNotes = new List<Note>();
        private Note selectedNoteForVelocity = null;
        private Note currentDraggingNote = null;
        private void guitarRollPanel_Paint(object sender, PaintEventArgs e)
        {
           int numberOfStrings = 7;
            if (activeFretPattern != null && activeFretPattern.IsActive)
            {
                // Logic to highlight the frets based on the active fret pattern
                // This could involve changing colors or styles of the frets
                // Example:
                // foreach (var fret in activeFretPattern.Pattern)
                // {
                //     // Draw or highlight the frets here
                // }
            }
             int laneHeight = (guitarRollPanel.Height-400) / numberOfStrings;          
                                 
            //int velocityLaneHeight = totalHeight + infoLaneHeight; // Height of the velocity lane (8th lane)

           float contentWidth = 0;

            // Clear the panel
            e.Graphics.Clear(guitarRollPanel.BackColor);

            // Draw the horizontal lines for each string
            for (int i = 1; i < numberOfStrings; i++)
            {
                e.Graphics.DrawLine(Pens.Gray, 0, i * laneHeight, guitarRollPanel.Width, i * laneHeight);
            }

            // Draw the horizontal line for the info lane
            e.Graphics.DrawLine(Pens.Gray, 0, (numberOfStrings * laneHeight) , guitarRollPanel.Width, (numberOfStrings * laneHeight) );


            // Determine which composition to use based on the fingering pattern mode
            var currentComposition = fingeringPatternMode ? fingerPatternComposition : composition;

            // Draw each note in the current composition
            foreach (Note note in currentComposition.Notes)
            {
                int noteWidth = (int)((note.EndTime - note.StartTime) * PixelsPerSecond);
                int noteStartX = (int)(note.StartTime * PixelsPerSecond) + guitarRollPanel.AutoScrollPosition.X;
                DrawNoteOnGuitarRoll(note, e.Graphics);

                // Draw velocity bar in the eighth lane
                int velocityBarHeight = (int)(400 * (note.Velocity / 127.0)); // Scale the height based on velocity 400 is lane height
                int velocityBarY = guitarRollPanel.Height - velocityBarHeight; // Position at the bottom of the panel
                e.Graphics.FillRectangle(Brushes.Gold, new Rectangle(noteStartX, velocityBarY, 4, velocityBarHeight));
            }

            // Calculate the position of the "Now Time" line based on currentTime and scroll position
            int nowTimeX = (int)(currentTime * PixelsPerSecond) + guitarRollPanel.AutoScrollPosition.X;

            // Draw the "Now Time" line
            using (Pen thickRedPen = new Pen(Color.Red, 3))
            {
                e.Graphics.DrawLine(thickRedPen, nowTimeX, 0, nowTimeX, guitarRollPanel.Height-400);
            }


            // Get the current horizontal scroll position
            int scrollPositionX = guitarRollPanel.AutoScrollPosition.X;

            // Calculate beat and bar durations in pixels
            float beatPixelSpacing = CalculateBeatDuration() * PixelsPerSecond;
            float barPixelSpacing = CalculateBarDuration() * PixelsPerSecond;

            // Adjust the starting position based on the scroll position
            float adjustedStartX = scrollPositionX - (scrollPositionX % beatPixelSpacing);

            // Create pens outside the loop for better performance
            using (Pen barPen = new Pen(Color.Black, 6.0f))
            using (Pen beatPen = new Pen(Color.Gray, 1.0f))
            {
                // Consider the entire content width of the guitarRollPanel
                 contentWidth = guitarRollPanel.HorizontalScroll.Maximum + guitarRollPanel.Width;

                for (float x = adjustedStartX; x < contentWidth; x += beatPixelSpacing)
                {
                    float drawX = x + scrollPositionX; // Adjust the drawing position based on the scroll position

                    if (x % barPixelSpacing == 0)
                    {
                        // Draw a thicker line for the bar
                        e.Graphics.DrawLine(barPen, drawX, 0, drawX, guitarRollPanel.Height);
                    }
                    else
                    {
                        // Draw a regular line for the beat
                        e.Graphics.DrawLine(beatPen, drawX, 0, drawX, guitarRollPanel.Height);
                    }
                }
            }

            // Draw bar numbers in the seventh lane
             barPixelSpacing = CalculateBarDuration() * PixelsPerSecond;
             scrollPositionX = guitarRollPanel.AutoScrollPosition.X;

            // Adjust the starting position based on the scroll position
             adjustedStartX = scrollPositionX - (scrollPositionX % barPixelSpacing);

            // Calculate the starting bar number based on scroll position
            int barNumber = 1 + (int)(adjustedStartX / barPixelSpacing);

            // Consider the entire content width of the guitarRollPanel
             contentWidth = guitarRollPanel.HorizontalScroll.Maximum + guitarRollPanel.Width;

            for (float x = adjustedStartX; x < contentWidth; x += barPixelSpacing)
            {
                float drawX = x + scrollPositionX; // Adjust the drawing position based on the scroll position

                // Position the bar number in the middle of the seventh lane
                float barNumberYPosition = (numberOfStrings - 0.5f) * laneHeight;
                e.Graphics.DrawString(barNumber.ToString(), new Font("Arial", 10), Brushes.Black, drawX, barNumberYPosition);

                barNumber++;
            }

        }



        private void guitarRollPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int numberOfStrings = 7; // Including the information lane
            int laneHeight = (guitarRollPanel.Height - 400) / numberOfStrings;
            float beatPixelSpacing = CalculateBeatDuration() * PixelsPerSecond;

            // Check if the click is in the seventh lane
            if (e.Y >= (numberOfStrings - 1) * laneHeight && e.Y < numberOfStrings * laneHeight)
            {
                // Calculate the nearest beat based on the mouse click position
                float adjustedClickX = e.X - guitarRollPanel.AutoScrollPosition.X;
                float nearestBeatStartTime = (float)Math.Round(adjustedClickX / beatPixelSpacing) * beatPixelSpacing;

                // Set the currentTime to the nearest beat
                // currentTime = nearestBeatStartTime / PixelsPerSecond;

                // Set the currentTime to the exact click position instead of the nearest beat
                currentTime = adjustedClickX / PixelsPerSecond;

                guitarRollPanel.Invalidate(); // Redraw to reflect the change in "Now Time"
                return; // Skip the rest of the method
            }

            // Handling clicks on notes
            // Determine which composition to use based on the fingering pattern mode
            var currentComposition = fingeringPatternMode ? fingerPatternComposition : composition;

            // Draw each note in the current composition
            foreach (Note note in currentComposition.Notes)
            {
                if (note.DrawingRectangle.Contains(e.Location))
                {
                    // Check if click is on the rightmost side
                    if (e.X >= note.DrawingRectangle.Right - 5 && e.X <= note.DrawingRectangle.Right)
                    {
                        note.EndTime += 0.5; // Extend by half a second, adjust as needed
                    }
                    // Check if click is on the leftmost side
                    else if (e.X >= note.DrawingRectangle.Left && e.X <= note.DrawingRectangle.Left + 5)
                    {
                        note.StartTime -= 0.5; // Reduce start time by half a second, adjust as needed
                    }
                    else
                    {
                        // Select the note for potential deletion
                        selectedNote = note;
                    }
                    // guitarRollPanel.Invalidate();
                    break;
                }
            }
        }



        private void guitarRollPanel_Scroll(object sender, ScrollEventArgs e)
        {
            guitarRollPanel.Refresh(); // Force a complete redraw
        }



        private int GetStringNumberFromButton(Button button)
        {
            // Extract the string number from the button's name
            string name = button.Name;
            string stringNumberPart = name.Substring(6, name.IndexOf("fret") - 6);
            if (int.TryParse(stringNumberPart, out int stringNumber))
            {
                return stringNumber;
            }
            else
            {
                throw new Exception("Invalid button name for string determination.");
            }
        }

        private void DrawNoteOnGuitarRoll(Note note, Graphics g)
        {
            
            int laneHeight = (guitarRollPanel.Height - 400) / 7;
            int noteStartX = (int)(note.StartTime * PixelsPerSecond) + guitarRollPanel.AutoScrollPosition.X;
            int noteWidth = (int)((note.EndTime - note.StartTime) * PixelsPerSecond);

            // Calculate note height based on velocity
            int minNoteHeight = 70; // Minimum height so the note doesn't disappear
            int maxNoteHeight = laneHeight; // Maximum height
            int noteHeight = (int)(laneHeight * (note.Velocity / 127.0)); // Adjust height based on velocity
           

            // Calculate the Y position based on the string number and adjusted for the note's height
            int noteY = (note.StringNumber * laneHeight) + (laneHeight - noteHeight);
           // Debug.WriteLine(noteY.ToString());
            note.DrawingRectangle = new Rectangle(noteStartX, noteY, noteWidth, noteHeight);
            note.RectangleX = noteStartX;
            note.RectangleWidth = noteWidth;
            note.RectangleY = noteY;
            note.RectangleHeight = noteHeight;

            Brush noteBrush = note.IsSelected ? Brushes.Green : Brushes.Blue;

            g.FillRectangle(noteBrush, note.DrawingRectangle);
            g.DrawRectangle(Pens.Black, note.DrawingRectangle);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(note.MidiNoteNumber.ToString(), new Font("Arial", 12), Brushes.BlueViolet, note.DrawingRectangle, stringFormat);
        }




        private void FretboardButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Tag is Tuple<int, int> tagData)
                {
                    int midiNoteNumber = tagData.Item1;
                    int stringNumber = tagData.Item2;

                    int fretWidth = 100; // Each button has a width of 100
                    int fretNumber = button.Left / fretWidth; // not +1 because fret numbers typically start from 1 and first button is open string
                    float beatDuration = CalculateBeatDuration();
                    float nearestBeatStartTime = (float)Math.Round(currentTime / beatDuration) * beatDuration;
                    float frequency = (float)MidiUtilities.GetFrequencyFromMidiNote(midiNoteNumber);
                    int Midichannel = midiChannelPerString[stringNumber];


                    // Create a new Note object with the nearest beat start time
                    // Note newNote = new Note(stringNumber, fretNumber, nearestBeatStartTime, nearestBeatStartTime + activeNoteDuration, velocitySlider.Value, frequency, midiNoteNumber, Midichannel);
                    Note newNote = new Note(stringNumber, fretNumber, currentTime, currentTime + activeNoteDuration,staccatoMode, velocitySlider.Value, frequency, midiNoteNumber, Midichannel);

                    // Determine which composition to add the note to based on the fingering pattern mode
                    var currentComposition = fingeringPatternMode ? fingerPatternComposition : composition;
                    
                    // Add the note to the current composition's Notes list
                    currentComposition.Notes.Add(newNote);



                    // Draw the note on the guitarRollPanel
                    using (Graphics g = guitarRollPanel.CreateGraphics())
                    {
                        DrawNoteOnGuitarRoll(newNote, g);
                    }

                    // Play the note (if you want this functionality)
                    PlayFromFretboard(midiNoteNumber);
                    guitarRollPanel.Invalidate(true);
                }
                else
                {
                    // Handle the case where the Tag is not in the expected format
                    MessageBox.Show("Button tag data is not in the expected format.");
                }
            }
        }





        private void ShiftNotesByDurationWithoutDrawing()
        {
            // Determine the shift amount based on the currently selected note duration
            float noteDurationInSeconds  = activeNoteDuration; 
            float shiftAmount = noteDurationInSeconds * PixelsPerSecond;

            // Increment the currentTime by the note duration
            currentTime += noteDurationInSeconds;

            // Determine which composition to use based on the fingering pattern mode
            var currentComposition = fingeringPatternMode ? fingerPatternComposition : composition;

            // Draw each note in the current composition
            foreach (Note note in currentComposition.Notes)
            { 
                note.StartTime -= shiftAmount / PixelsPerSecond; // Convert pixels to seconds
                note.EndTime -= shiftAmount / PixelsPerSecond;   // Convert pixels to seconds
            }

            // Update the AutoScrollPosition to center around the currentTime
            int currentPixelPosition = (int)(currentTime * PixelsPerSecond);
            int newScrollPosition = Math.Max(0, currentPixelPosition - guitarRollPanel.Width / 2);
            guitarRollPanel.AutoScrollPosition = new Point(newScrollPosition, 0);

        }


        


        private void velocitySlider_ValueChanged(object sender, EventArgs e)
        {

            if (selectedNote != null )
            {
               
                selectedNote.Velocity = velocitySlider.Value;
                guitarRollPanel.Invalidate(); // Redraw to reflect the velocity change
            }
        }
        private void guitarRollPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Note newlySelectedNote = null; // To track the note that was clicked on

            // Determine which composition to use based on the fingering pattern mode
            var currentComposition = fingeringPatternMode ? fingerPatternComposition : composition;

            // Draw each note in the current composition
            foreach (Note note in currentComposition.Notes)
            {
                if (note.DrawingRectangle.Contains(e.Location))
                {
                    newlySelectedNote = note;
                    break;
                }
            }

            // If an actual note was clicked on
            if (newlySelectedNote != null)
            {
                // Deselect all previously selected notes
                foreach (Note prevNote in previouslySelectedNotes)
                {
                    prevNote.IsSelected = false;
                }
                previouslySelectedNotes.Clear();

                newlySelectedNote.IsSelected = true;
                selectedNote = newlySelectedNote;
                previouslySelectedNotes.Add(newlySelectedNote);

                // Set the noteToDelete variable
                noteToDelete = newlySelectedNote;

                // Check if click is on the rightmost side
                if (e.X >= newlySelectedNote.DrawingRectangle.Right - 15 && e.X <= newlySelectedNote.DrawingRectangle.Right)
                {
                    isDraggingRight = true;
                }
                // Check if click is on the leftmost side
                else if (e.X >= newlySelectedNote.DrawingRectangle.Left && e.X <= newlySelectedNote.DrawingRectangle.Left + 15)
                {
                    isDraggingLeft = true;
                }
                else
                {
                    // Set initial position for potential dragging
                    initialMousePosition = e.Location;
                }
            }
            // If clicked on empty space, deselect the current note
            else if (selectedNote != null)
            {
                selectedNote.IsSelected = false;
                selectedNote = null;
                previouslySelectedNotes.Clear();
                //noteToDelete = null; // Clear the noteToDelete variable
            }

            // Check if the click is in the eighth lane
            if (e.Y > guitarRollPanel.Height - 400) // Assuming 400 is the height of the eighth lane
            {
               
                isDraggingVelocity = true;
                
            }

            guitarRollPanel.Invalidate(); // Redraw to reflect the selection/deselection and color change
        }

        private void guitarRollPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedNote != null)
            {
                // Calculate the area that needs to be redrawn before moving the note
                Rectangle oldArea = selectedNote.DrawingRectangle;

                if (isDraggingRight)
                {
                    float newEndTime = e.X / PixelsPerSecond;
                    selectedNote.EndTime = newEndTime;
                }
                else if (isDraggingLeft)
                {
                    float newStartTime = e.X / PixelsPerSecond;
                    selectedNote.StartTime = newStartTime;
                }
                else if (initialMousePosition != Point.Empty)
                {
                    // Move the note earlier or later in time
                    float dragDistanceInSeconds = (e.X - initialMousePosition.X) / PixelsPerSecond;
                    selectedNote.StartTime += dragDistanceInSeconds;
                    selectedNote.EndTime += dragDistanceInSeconds;
                    initialMousePosition = e.Location; // Update the initial position for continuous dragging
                }

                // Calculate the new area after the note has been moved
                Rectangle newArea = selectedNote.DrawingRectangle;

                // Expand the update area slightly to ensure complete clearing
                oldArea.Inflate(2, 2);
                newArea.Inflate(2, 2);

                // Combine the old and new areas to get the total area that needs to be redrawn
                Rectangle updateArea = Rectangle.Union(oldArea, newArea);

                // Invalidate only the area that needs to be redrawn
                guitarRollPanel.Invalidate(updateArea);
            }

            if (isDraggingVelocity)
            {
                selectedNoteForVelocity = FindNoteAtPosition(e.X);
               
                if (selectedNoteForVelocity != null)
                {
                   // Debug.WriteLine("Note selected for velocity: " + selectedNoteForVelocity.MidiNoteNumber);
                    int newVelocity = CalculateVelocityFromPosition(e.Y);
                    int noteStartX = (int)(selectedNoteForVelocity.StartTime * PixelsPerSecond);
                    selectedNoteForVelocity.Velocity = newVelocity;
                    // Redraw only the velocity bar of the current dragging note

                    RedrawVelocityBar(selectedNoteForVelocity, e.X,e.Y);
                }
            }
        }
        private void RedrawVelocityBar(Note note, int x, int y)
        {
            if (note == null) return;
            // Adjust the x-coordinate for the horizontal scroll offset
            int noteStartX = x;
            int adjustedX = noteStartX + guitarRollPanel.AutoScrollOffset.X;

            using (Graphics g = guitarRollPanel.CreateGraphics())
            {
                
                int velocityBarHeight = (int)(400 * (note.Velocity / 127.0)); // Scale the height based on velocity
                int velocityBarY = guitarRollPanel.Height - velocityBarHeight; // Position at the bottom of the panel
                // Clear the previous velocity bar
                g.FillRectangle(new SolidBrush(guitarRollPanel.BackColor), new Rectangle(adjustedX, guitarRollPanel.Height - 400, 4, 400));
                // Draw the new velocity bar
                g.FillRectangle(Brushes.Gold, new Rectangle(adjustedX, velocityBarY, 4, velocityBarHeight));
            }
        }



        private void guitarRollPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedNote != null)
            {
                if (isDraggingRight)
                {
                    float newEndTime = (e.X - guitarRollPanel.AutoScrollPosition.X) / PixelsPerSecond;
                    selectedNote.EndTime = Math.Max(selectedNote.StartTime, newEndTime); // Ensure end time is not before start time
                }
                else if (isDraggingLeft)
                {
                    float newStartTime = (e.X - guitarRollPanel.AutoScrollPosition.X) / PixelsPerSecond;
                    selectedNote.StartTime = Math.Min(selectedNote.EndTime, newStartTime); // Ensure start time is not after end time
                }

                // Reset dragging flags
                isDraggingLeft = false;
                isDraggingRight = false;
                initialMousePosition = Point.Empty;
                
                

            }
            isDraggingVelocity = false;
            selectedNoteForVelocity = null;
           // Debug.WriteLine("mouse up");
        }

        private int CalculateVelocityFromPosition(int y)
        {
            int velocityLaneHeight = 400; // Height of the velocity lane
            int relativeY = guitarRollPanel.Height - y; // Y position relative to the bottom of the panel
           //Debug.WriteLine("velocity from position" + ((int)((relativeY / (float)velocityLaneHeight) * 127)));
            return (int)((relativeY / (float)velocityLaneHeight) * 127); // Scale to MIDI velocity range
        }
        private Note FindNoteAtPosition(int x)
        {
            // Adjust the x-coordinate for the horizontal scroll offset
            int adjustedX = x + guitarRollPanel.AutoScrollOffset.X;
            Console.WriteLine("Adjusted Mouse X: " + adjustedX); // Debugging: Print adjusted mouse x-coordinate
            if(fingeringPatternMode)
            {
                foreach (Note note in fingerPatternComposition.Notes)
                {
                    int stripStartX = note.DrawingRectangle.X;
                    int stripEndX = stripStartX + 4; // 4 pixels wide for the velocity strip

                    Console.WriteLine($"Note Strip X Range: {stripStartX} to {stripEndX}"); // Debugging

                    if (adjustedX >= stripStartX && adjustedX <= stripEndX)
                    {
                        return note;
                    }
                }
            }
            else
            {
                foreach (Note note in composition.Notes)
                {
                    int stripStartX = note.DrawingRectangle.X;
                    int stripEndX = stripStartX + 4; // 4 pixels wide for the velocity strip

                    Console.WriteLine($"Note Strip X Range: {stripStartX} to {stripEndX}"); // Debugging

                    if (adjustedX >= stripStartX && adjustedX <= stripEndX)
                    {
                        return note;
                    }
                }
            }



            return null;
        }






        private float CalculateBeatDuration()

        {

            // If the denominator is 4 (like in 3/4 or 4/4), a beat is a quarter note.
            if (timeSignatureDenominator == 4)
            {
                return 1f;// 60.0f / tempo;
            }
            // If the denominator is 8 (like in 3/8 or 6/8), a beat is an eighth note.
            else if (timeSignatureDenominator == 8)
            {
                return .25f;// (60.0f / tempo) / 2; // Half the duration of a quarter note
            }
            // If the denominator is 2 (like in 3/2), a beat is a half note.
            else if (timeSignatureDenominator == 2)
            {
                return 2f;// (60.0f / tempo) * 2; // Twice the duration of a quarter note
            }
            // ... add more conditions for other time signatures as needed

            // Default to quarter note duration if no matching time signature is found
            return 1f;// 60.0f / tempo;
        }


        private float CalculateBarDuration()
        {
            return CalculateBeatDuration() * timeSignatureNumerator;
        }


    }




}



