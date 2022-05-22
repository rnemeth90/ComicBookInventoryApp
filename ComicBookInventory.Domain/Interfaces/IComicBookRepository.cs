namespace ComicBookInventory.Shared
{
    public interface IComicBookRepository : IGenericRepository<ComicBook>
    {
        void AddBookWithAuthors(ComicBookViewModel book);
        ComicBookWithAuthorsViewModel GetBookById(int bookId);
    }
}
