namespace ComicBookInventory.Shared
{
    public class ComicBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rating { get; set; }
        public string Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }


        // navigation properties
        public List<ComicBook_Author> ComicBook_Authors { get; set; }
        public List<ComicBook_Character> ComicBook_Characters { get; set; } 
    }
}
