using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{
    [Serializable]
    public class Composition
    {
        public string Title { get; set; }
     
        public List<Note> Notes { get; set; } = new List<Note>();
        public int Tempo { get; set; }
        public int TimeSignatureNumerator { get; set; }
        public int TimeSignatureDenominator { get; set; }

        //Ensure they have a parameterless default constructor, which is required for deserialization.  
        public Composition()
        {

        }
    }
}
