using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitarsharp
{
    [Serializable]
    public class FingeringPattern
    {
        public int fingeringPatternNumber;
        public List<Note> Notes { get; set; } = new List<Note>();

        public bool isActive { get; set; }


        //Ensure they have a parameterless default constructor, which is required for deserialization.  
        public FingeringPattern()
        {
            
        }
        // Methods to save and load the fingering pattern to and from a file
        public void SavePattern(string filePath)
        {
            // Implement serialization logic
        }

        public static FingeringPattern LoadPattern(string filePath)
        {
            // Implement deserialization logic
            return new FingeringPattern();
        }
    }
    public class FingeringPatternDTO
    {
        public List<NoteDTO> Notes { get; set; }

        public FingeringPatternDTO()
        {
            Notes = new List<NoteDTO>();
        }

        // Convert a FingeringPattern to a FingeringPatternDTO
        public static FingeringPatternDTO FromFingeringPattern(FingeringPattern fingeringPattern)
        {
            return new FingeringPatternDTO
            {
                Notes = fingeringPattern.Notes.Select(NoteDTO.FromNote).ToList()
            };
        }

        // Convert a FingeringPatternDTO back to a FingeringPattern
        public FingeringPattern ToFingeringPattern()
        {
            FingeringPattern fingeringPattern = new FingeringPattern
            {
                Notes = this.Notes.Select(dto => dto.ToNote()).ToList()
            };
            return fingeringPattern;
        }
    }

}
