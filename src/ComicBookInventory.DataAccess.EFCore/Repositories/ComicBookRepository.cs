using ComicBookInventory.Exceptions;
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
            try
            {
                var entities = DbContext.ComicBooks.Select(model => new ComicBookWithAuthorsAndCharactersViewModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    IsRead = model.IsRead,
                    DateRead = model.IsRead ? model.DateRead.Value : null,
                    Rating = model.IsRead ? model.Rating.Value : null,
                    Genre = model.Genre,
                    CoverUrl = model.CoverUrl,
                    AuthorNames = model.ComicBook_Authors.Select(n => n.Author.FullName).ToList(),
                    CharacterNames = model.ComicBook_Characters.Select(n => n.Character.FullName).ToList()
                }).ToList();
                return entities;
            }
            catch (ComicBookException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ComicBookWithAuthorsAndCharactersViewModel GetBookById(int id)
        {
            try
            {
                var entity = DbContext.ComicBooks.Where(n => n.Id == id).Select(model => new ComicBookWithAuthorsAndCharactersViewModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    IsRead = model.IsRead,
                    DateRead = model.IsRead ? model.DateRead.Value : null,
                    Rating = model.IsRead ? model.Rating.Value : null,
                    Genre = model.Genre,
                    CoverUrl = model.CoverUrl,
                    AuthorNames = model.ComicBook_Authors.Select(n => n.Author.FullName).ToList(),
                    CharacterNames = model.ComicBook_Characters.Select(n => n.Character.FullName).ToList()
                }).FirstOrDefault();
                return entity;
            }
            catch (ComicBookException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddBook(ComicBookWithAuthorsAndCharactersViewModel model)
        {
            // do we need to call .SaveChanges() 3x here?? Likely not. Need to test
            try
            {
                var entity = new ComicBook()
                {
                    Title = model.Title,
                    Description = model.Description,
                    IsRead = model.IsRead,
                    DateRead = model.IsRead ? model.DateRead.Value : null,
                    Rating = model.IsRead ? model.Rating.Value : null,
                    Genre = model.Genre,
                    CoverUrl = model.CoverUrl,
                    DateAdded = DateTime.Now,

                };
                DbContext.ComicBooks.Add(entity);
                DbContext.SaveChanges();


                foreach (var id in model.AuthorNames)
                {
                    // this does not seem efficient
                    // we shouldn't convert the Id to string here. 
                    var author = DbContext.Authors.FirstOrDefault(a => a.Id.ToString() == id);
                    if (author != null)
                    {
                        var _book_author = new ComicBook_Author()
                        {
                            ComicBookId = entity.Id,
                            AuthorId = author.Id
                        };
                        DbContext.ComicBooks_Authors.Add(_book_author);
                        DbContext.SaveChanges();
                    }
                }

                foreach (var id in model.CharacterNames)
                {
                    var character = DbContext.Characters.FirstOrDefault(c => c.Id.ToString() == id);
                    if (character != null)
                    {
                        var _book_character = new ComicBook_Character()
                        {
                            ComicBookId = entity.Id,
                            CharacterId = character.Id
                        };
                        DbContext.ComicBooks_Characters.Add(_book_character);
                        DbContext.SaveChanges();
                    }
                }
            }
            catch (ComicBookException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateBook(int id, ComicBookViewModel model)
        {
            try
            {
                var entity = DbContext.ComicBooks.Where(n => n.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    entity.Genre = model.Genre;
                    entity.CoverUrl = model.CoverUrl;
                    entity.IsRead = model.IsRead;
                    entity.Description = model.Description;
                    entity.DateRead = entity.DateRead == null ? entity.DateRead = DateTime.Now : entity.DateRead;
                    entity.Rating = model.Rating;
                    entity.CoverUrl = model.CoverUrl;
                    entity.Title = model.Title;
                    DbContext.SaveChanges();
                }
            }
            catch (ComicBookException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
