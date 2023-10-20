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

            for (int i = 1; i < numberOfStrings; i++)
            {
                e.Graphics.DrawLine(Pens.Gray, 0, i * laneHeight, guitarRollPanel.Width, i * laneHeight);
            }
            foreach (Note note in notes)
            {
                laneHeight = guitarRollPanel.Height / 6;
                Rectangle noteRect = new Rectangle((int)note.StartTime, note.StringNumber * laneHeight, (int)note.EndTime - (int)note.StartTime, laneHeight);
                e.Graphics.FillRectangle(Brushes.Blue, noteRect);
            }
        }


        private List<Note> notes = new List<Note>();
        private void guitarRollPanel_MouseClick(object sender, MouseEventArgs e)
        {
            

            int numberOfStrings = 6;
            int laneHeight = guitarRollPanel.Height / numberOfStrings;
            int clickedString = e.Y / laneHeight;

            // Assuming you have a method or a way to get the fretNumber based on the X position
            int fretNumber = GetFretNumber(e.X);

            // Create a new note
            Note newNote = new Note(
                stringNumber: clickedString,
                fretNumber: fretNumber,
                startTime: e.X,
                endTime: e.X + 50, // default width for the note
                velocity: 100, // default velocity, you can adjust this
                frequency: 0.0f, // You can set this based on the clickedString and fret
                midiNoteNumber: 0 // You can set this based on the clickedString and fret
            );

            notes.Add(newNote);

            // Redraw the panel to show the new note
            guitarRollPanel.Invalidate();

        }
        // This is just a placeholder. You'll need to implement this method based on your requirements.
        private int GetFretNumber(int xPosition)
        {
            // Implement logic to determine the fret number based on the X position
            return 0;
        }






    }



}