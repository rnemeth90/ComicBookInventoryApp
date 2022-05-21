using ComicBookInventory.Shared;

namespace ComicBookInventory.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IComicBookRepository ComicBooks { get; }
        ICharacterRepository Characters { get; }
        IComicBookCharacterRepository ComicBook_Characters { get; }
        int Complete();
    }
}
