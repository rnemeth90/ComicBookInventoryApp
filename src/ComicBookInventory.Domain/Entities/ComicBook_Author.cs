namespace ComicBookInventory.Shared
{
    public class ComicBook_Author
    {
        public int Id { get; set; }

        
        // navigation properties
        public int? ComicBookId { get; set; }
        public ComicBook ComicBook { get; set; }

        public int? AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
