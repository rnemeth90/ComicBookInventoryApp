namespace ComicBookInventory.Shared
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        void AddAuthor(AuthorViewModel author);
    }
}
