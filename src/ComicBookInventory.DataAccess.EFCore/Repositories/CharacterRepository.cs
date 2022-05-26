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

        public void AddCharacter(CharacterViewModel character)
        {
            var _character = new Character()
            {
                FullName = character.FullName,
            };
            DbContext.Characters.Add(_character);
            DbContext.SaveChanges();
        }
    }
}
