namespace ComicBookInventory.Shared
{
    public class ComicBookWithAuthorsAndCharactersViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rating { get; set; }
        public string Genre { get; set; }
        public string? CoverUrl { get; set; }

        /// <summary>
        /// Navigation properties
        /// </summary>
        /// a book can have many authors
        public ICollection<string>? AuthorName { get; set; }
        public ICollection<string>? CharacterNames { get; set; }

    }
}
