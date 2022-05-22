using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookInventory.DataAccess
{
    public class ComicBookRepository : GenericRepository<ComicBook>, IComicBookRepository
    {
        public ComicBookRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }

        public ComicBookWithAuthorsViewModel GetBookById(int bookId)
        {
            var _bookWithAuthors = DbContext.ComicBooks.Where(n => n.Id == bookId).Select(book => new ComicBookWithAuthorsViewModel()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rating = book.IsRead ? book.Rating.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                AuthorNames = book.ComicBook_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public void AddBookWithAuthors(ComicBookViewModel book)
        {
            var _book = new ComicBook()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rating = book.IsRead ? book.Rating.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
            };
            DbContext.ComicBooks.Add(_book);
            DbContext.SaveChanges();

            foreach (var id in book.AuthorIds)
            {
                var _book_author = new ComicBook_Author()
                {
                    ComicBookId = _book.Id,
                    AuthorId = id
                };
                DbContext.ComicBooks_Authors.Add(_book_author);
                DbContext.SaveChanges();
            }
        }
    }
}
