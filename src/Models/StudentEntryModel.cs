using System.Collections.Generic;

namespace Tunts.Models
{
    public class StudentEntryModel
    {
        public StudentEntryModel()
        {
            Students = new List<Student>();            
        }

        public List<Student>  Students { get; set; }
        public ClassInfo ClasseInfo { get; set; }
    }
}