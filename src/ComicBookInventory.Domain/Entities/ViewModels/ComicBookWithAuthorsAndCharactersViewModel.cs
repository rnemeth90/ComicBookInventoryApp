using System.ComponentModel.DataAnnotations;

namespace ComicBookInventory.Shared
{
    public class ComicBookWithAuthorsAndCharactersViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }

        [Range(1, 10)]
        public int? Rating { get; set; }
        public string Genre { get; set; }

        [RegularExpression(@"/^(https:|http:|www\.)\S*/gm")]
        public string? CoverUrl { get; set; }

        /// <summary>
        /// Navigation properties
        /// </summary>
        /// a book can have many authors
        public ICollection<string>? AuthorNames { get; set; }
        public ICollection<string>? CharacterNames { get; set; }

    }
}
