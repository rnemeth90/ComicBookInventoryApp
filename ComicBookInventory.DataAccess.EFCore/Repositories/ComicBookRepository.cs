using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookInventory.DataAccess
{
    public class ComicBookRepository : GenericRepository<ComicBook>, IComicBookRepository
    {
        public ComicBookRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ComicBookWithAuthorsAndCharactersViewModel> GetAllBooks()
        {
            var books = DbContext.ComicBooks.Select(book => new ComicBookWithAuthorsAndCharactersViewModel()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rating = book.IsRead ? book.Rating.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                AuthorNames = book.ComicBook_Authors.Select(n => n.Author.FullName).ToList(),
                CharacterNames = book.ComicBook_Characters.Select(n => n.Character.FullName).ToList()   
            }).ToList();
            return books;
        }

        public ComicBookWithAuthorsAndCharactersViewModel GetBookById(int bookId)
        {
            var _book = DbContext.ComicBooks.Where(n => n.Id == bookId).Select(book => new ComicBookWithAuthorsAndCharactersViewModel()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rating = book.IsRead ? book.Rating.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                AuthorNames = book.ComicBook_Authors.Select(n => n.Author.FullName).ToList(),
                CharacterNames = book.ComicBook_Characters.Select(n => n.Character.FullName).ToList()
            }).FirstOrDefault();

            return _book;
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
            DbContext.Dispose();
        }

        public void AddBook(ComicBookViewModel book)
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

            foreach (var id in book.CharacterIds)
            {
                var _book_character = new ComicBook_Character()
                {
                    ComicBookId = _book.Id,
                    CharacterId = id
                };
                DbContext.ComicBooks_Characters.Add(_book_character);
                DbContext.SaveChanges();
            }
            DbContext.Dispose();
        }

        public void UpdateBook(int id, ComicBookViewModel model)
        {
            var book = DbContext.ComicBooks.Where(n => n.Id == id).FirstOrDefault();

            if (book != null)
            { 
                book.Genre = model.Genre;
                book.CoverUrl = model.CoverUrl;
                book.IsRead = model.IsRead;
                book.Description = model.Description;
                book.DateRead = book.DateRead == null ? book.DateRead = DateTime.Now : book.DateRead;
                book.Rating = model.Rating;
                book.CoverUrl = model.CoverUrl;
                book.Title = model.Title;
            }
        }
    }
}
