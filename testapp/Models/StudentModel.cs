using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testapp.Models
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public int GroupForeignKey { get; set; }
        
        [NotMapped]
        public IFormFile Image { get; set; }

        public FileUploadModel ImgId { get; set; }
        public string ImgFile { get; set; }
        [Required]
        public GroupModel Group { get; set; }
    }
}
