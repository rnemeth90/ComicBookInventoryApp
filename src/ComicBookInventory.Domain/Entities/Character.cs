using System.Text.Json.Serialization;

namespace ComicBookInventory.Shared
{
    public class Character : ICharacter
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PrimaryAbility { get; set; }
        public string? SecondaryAbility { get; set; }
        public string? Species { get; set; }
        public string? Alias { get; set; }
        public bool IsAlive { get; set; }
        public string? Weapon { get; set; }

        // navigation properties
        [JsonIgnore]
        public ICollection<ComicBook_Character> ComicBook_Characters { get; set; }
    }
}
