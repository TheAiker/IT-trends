using Microsoft.AspNetCore.Http;

namespace testapp.Models
{
    public class FileUploadModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public int StudentForeignKey { get; set; }
        public StudentModel Student { get; set; }
    }
}
