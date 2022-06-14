namespace ComicBookInventory.Shared
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        AuthorViewModel GetAuthorById (int id);
        void AddAuthor(AuthorViewModel author);


        void UpdateAuthor(int id, AuthorViewModel author);
    }
}
