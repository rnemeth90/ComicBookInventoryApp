using System.ComponentModel.DataAnnotations;

namespace ComicBookInventory.Shared
{
    public class ComicBookWithCharactersViewModel
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

        /// <summary>
        /// Match any sanely formed URI
        /// Examples:
        /// https://www.google.com
        /// www.google.com/googler.pic
        /// http://www.google.com
        /// https://www.google.com/apiod.jpg
        /// </summary>

        [RegularExpression(@"(?:(())(www\.([^/?#\s]*))|((http(s)?|ftp):)(\/\/([^/?#\s]*)))([^?#\s]*)(\?([^#\s]*))?(#([^\s]*))?")]
        public string? CoverUrl { get; set; }

        /// <summary>
        /// Navigation properties
        /// </summary>
        /// a book can have many characters
        public ICollection<string> CharacterNames { get; set; }
    }
}
