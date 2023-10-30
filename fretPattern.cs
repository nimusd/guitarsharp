using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{
    [Serializable]
    public class fretPattern
    {
        public string pattern;
        public string fretPatterName;
        public string description;

        //Ensure they have a parameterless default constructor, which is required for deserialization.    
        public fretPattern()
        {

        }
    }
}
