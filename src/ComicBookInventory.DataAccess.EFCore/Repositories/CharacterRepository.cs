using ComicBookInventory.Exceptions;
using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class CharacterRepository : GenericRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(ApiDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<CharacterViewModel> GetAllCharacters()
        {
            var characters = DbContext.Characters.Select(c => new CharacterViewModel()
            {
                Id = c.Id,
                FullName = c.FullName
            }).ToList();
            return characters;
        }

        public CharacterViewModel GetCharacterById(int id)
        {
            try
            {
                var entity = DbContext.Characters.Where(a => a.Id == id)
                                        .Select(a => new CharacterViewModel()
                                        {
                                            Id =a.Id,
                                            FullName = a.FullName
                                        }).FirstOrDefault();
                return entity;
            }
            catch (CharacterException ex)
            {
                throw;
            }
        }

        public void AddCharacter(CharacterViewModel model)
        {
            var character = new Character()
            {
                FullName = model.FullName,
            };
            DbContext.Characters.Add(character);
            DbContext.SaveChanges();
        }


        public void UpdateCharacter(int id, CharacterViewModel model)
        {
            var entity = DbContext.Characters.Where(c => c.Id == id).FirstOrDefault();
            if (entity != null)
            {
                entity.FullName = model.FullName;
                DbContext.SaveChanges();
            }
            else
            {
                throw new CharacterException($"Character with id {id} not found");
            }
        }
    }
}
