namespace ComicBookInventory.Api.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // navigation properties
        public List<ComicBook_Author> ComicBook_Authors { get; set; }
    }
}
