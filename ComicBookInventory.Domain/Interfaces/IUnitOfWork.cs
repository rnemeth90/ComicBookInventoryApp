using ComicBookInventory.Shared;

namespace ComicBookInventory.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        ICharacterRepository Characters { get; }   
        IComicBookRepository ComicBooks { get; }   

        int Complete();
        void Dispose();
    }
}
