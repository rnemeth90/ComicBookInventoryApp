namespace ComicBookInventory.Shared
{
    public interface IComicBookRepository : IGenericRepository<ComicBook>
    {
        IEnumerable<ComicBookWithAuthorsAndCharactersViewModel> GetAllBooks();
        ComicBookWithAuthorsViewModel GetBookById(int bookId);
        void AddBookWithAuthors(ComicBookViewModel book);
        void UpdateBook(int id, ComicBookViewModel book);
    }
}
