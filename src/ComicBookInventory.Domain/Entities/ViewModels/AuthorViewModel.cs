using System.ComponentModel.DataAnnotations;

namespace ComicBookInventory.Shared
{
    public class AuthorViewModel : IAuthor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string FullName { get; set; }
    }
}
