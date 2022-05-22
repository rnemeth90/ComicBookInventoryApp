using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class CharacterRepository : GenericRepository<CharacterViewModel>, ICharacterRepository
    {
        public CharacterRepository(ApiDbContext dbContext) : base(dbContext)
        {

        }
    }
}
