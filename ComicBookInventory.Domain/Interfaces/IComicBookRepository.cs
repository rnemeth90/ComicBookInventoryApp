﻿using ComicBookInventory.Api.Models.ViewModels;

namespace ComicBookInventory.Shared
{
    public interface IComicBookRepository : IGenericRepository<ComicBookViewModel>
    {
        public void Update(int id, ComicBookViewModel entity);
    }
}
