namespace LibraryManagementApp.Core.Models
{
    public class AuthorDTO
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BookDTO>? Books { get; set; }
    }
}