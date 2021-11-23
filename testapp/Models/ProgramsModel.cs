namespace testapp.Models
{
    public class ProgramsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public GroupModel Group { get; set; }
    }
}
