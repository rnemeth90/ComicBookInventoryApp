using System.ComponentModel.DataAnnotations;

namespace ComicBookInventory.Shared
{
    public class ComicBookViewModel : IComicBook
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
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
        /// a book can have many authors and characters
        public ICollection<int>? AuthorIds { get; set; }
        public ICollection<int>? CharacterIds { get; set; }

    }
}
