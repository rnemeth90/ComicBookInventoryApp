using System.Text.Json.Serialization;

namespace ComicBookInventory.Shared
{
    public class Author : IAuthor
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // navigation properties
        [JsonIgnore]
        public ICollection<ComicBook_Author> ComicBook_Authors { get; set; }
    }
}
