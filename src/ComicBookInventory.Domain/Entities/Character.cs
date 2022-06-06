using System.Text.Json.Serialization;

namespace ComicBookInventory.Shared
{
    public class Character
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // navigation properties
        [JsonIgnore]
        public ICollection<ComicBook_Character> ComicBook_Characters { get; set; }
    }
}
