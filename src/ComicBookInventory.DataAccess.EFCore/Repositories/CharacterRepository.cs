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
                FullName = c.FullName
            }).ToList();
            return characters;
        }

        public void AddCharacter(CharacterViewModel model)
        {
            var _character = new Character()
            {
                FullName = model.FullName,
            };
            DbContext.Characters.Add(_character);
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
