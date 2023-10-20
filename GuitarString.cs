using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{
    internal class GuitarString
    {
        public int StringNumber { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public GuitarString() { 
        
        }

    }
}
