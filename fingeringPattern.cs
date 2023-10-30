using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{
    [Serializable]
    public class fingeringPattern
    {
        public string name;
       
        public string fingerPatternName;

        public string fingerPatternDescription;

        //Ensure they have a parameterless default constructor, which is required for deserialization.  
        public fingeringPattern()
        {

        }
    }
}
