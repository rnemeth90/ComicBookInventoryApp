namespace ComicBookInventory.Shared
{
    public interface IComicBookRepository : IGenericRepository<ComicBook>
    {
        IEnumerable<ComicBookWithAuthorsAndCharactersViewModel> GetAllBooks();
        ComicBookWithAuthorsAndCharactersViewModel GetBookById(int bookId);
        void AddBook(ComicBookViewModel book);
        void UpdateBook(int id, ComicBookViewModel book);
    }
}
