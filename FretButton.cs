using System;
using System.Drawing;
using System.Windows.Forms;

namespace Guitarsharp
{
    public class FretButton : Button
    {
        public int StringIndex { get; set; }
        public int FretIndex { get; set; }

        // Constructor
        public FretButton(int stringIndex, int fretIndex)
        {
            StringIndex = stringIndex;
            FretIndex = fretIndex;
        }

        protected override void OnClick(EventArgs e)
        {
            // Your custom click handling logic here
            // For example, toggle the background color
            if (this.BackColor == Color.Beige)
                this.BackColor = Color.Bisque;
            else
                this.BackColor = Color.Beige;

            // Optionally, call the base class's OnClick if you want the standard behavior as well
            // base.OnClick(e);
        }
    }
}

