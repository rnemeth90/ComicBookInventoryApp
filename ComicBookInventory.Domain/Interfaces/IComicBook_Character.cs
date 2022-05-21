using ComicBookInventory.Api.Models;

namespace ComicBookInventory.Shared;

public interface IComicBook_Character
{
    public int Id { get; set; }

    int? ComicBookId { get; set; }
    ComicBook? ComicBook { get; set; }

    int? CharacterId { get; set; }
    Character? Character { get; set; }
}
