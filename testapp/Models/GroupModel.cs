using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace testapp.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
    
        public string GroupName { get; set; }
    
        public int Year { get; set; }
    
        public string President { get; set; }

        public int GroupYear { get; set; }

        public ProgramsModel Program { get; set; }
        public List<StudentModel> Students { get; set; }

        //public virtual ICollection<StudentModel> Students{ get; set; }
    }
}
