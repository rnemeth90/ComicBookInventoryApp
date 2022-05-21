namespace ComicBookInventory.Shared
{
    public interface IComicBookRepository : IGenericRepository<ComicBookViewModel>
    {
        public void Update(int id, ComicBookViewModel entity);
    }
}
