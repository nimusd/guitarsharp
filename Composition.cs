using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{
    internal class Composition
    {
        public string Title { get; set; }
        public List<GuitarString> Strings { get; set; } = new List<GuitarString>();
        public int Tempo { get; set; }
        public string TimeSignature { get; set; }

        public  Composition()
        {

        }
    }
}
