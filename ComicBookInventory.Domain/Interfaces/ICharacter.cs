using System.ComponentModel.DataAnnotations;

namespace ComicBookInventory.Shared
{
    public interface ICharacter
    {
        string? FullName { get; set; }
    
    }
}
