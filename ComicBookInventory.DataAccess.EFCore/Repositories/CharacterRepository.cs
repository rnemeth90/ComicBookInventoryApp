using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class CharacterRepository : GenericRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(ApiDbContext dbContext) : base(dbContext)
        {

        }
    }
}
