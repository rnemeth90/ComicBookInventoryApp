using ComicBookInventory.Shared;

namespace ComicBookInventory.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        IComicBookRepository ComicBooks { get; }
        ICharacterRepository Characters { get; }
        IComicBookCharacterRepository ComicBook_Characters { get; }
        int Complete();
    }
}
