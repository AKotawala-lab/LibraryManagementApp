namespace LibraryManagementApp.Core.Models
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public AuthorDTO? Author { get; set; }
    }
}