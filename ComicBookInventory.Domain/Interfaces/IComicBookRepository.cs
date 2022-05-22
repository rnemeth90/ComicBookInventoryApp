namespace ComicBookInventory.Shared
{
    public interface IComicBookRepository : IGenericRepository<ComicBook>
    {
        void AddBookWithAuthors(ComicBookViewModel book);
        ComicBookWithAuthorsViewModel GetBookById(int bookId);
        void UpdateBook(int id, ComicBookViewModel book);
    }
}
