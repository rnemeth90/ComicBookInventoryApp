using System.ComponentModel.DataAnnotations;

namespace ComicBookInventory.Shared
{
    public class CharacterViewModel : ICharacter
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string FullName { get; set; }
        [Required]
        public string PrimaryAbility { get; set; }
        public string? SecondaryAbility { get; set; }
        public string? Species { get; set; }
        public string? Alias { get; set; }
        public bool IsAlive { get; set; }
        public string? Weapon { get; set; }
    }
}
