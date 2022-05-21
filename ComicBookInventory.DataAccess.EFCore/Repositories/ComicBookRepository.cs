using ComicBookInventory.Shared;
using System;
using System.Linq.Expressions;

namespace ComicBookInventory.DataAccess
{
    internal class ComicBookRepository : GenericRepository<ComicBookViewModel>, IComicBookRepository
    {


        [HttpPost]
        public void AddBook(ComicBookViewModel model)
        {
            var _book = new ComicBook()
            {
                Title = model.Title,
                IsRead = model.IsRead,
                Genre = model.Genre,
                Rating = model.Rating,
                DateAdded = DateTime.Now,
                Description = model.Description,
                CoverUrl = model.CoverUrl,
            };
            _dbContext.Add(_book);
            _dbContext.SaveChanges();

            // add the relation
            foreach (var id in model.AuthorIds)
            {
                var _book_author = new ComicBook_Author()
                {
                    ComicBookId = _book.Id,
                    AuthorId = id
                };
                _dbContext.ComicBooks_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }
        }
        public void Update(int id, ComicBookViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
