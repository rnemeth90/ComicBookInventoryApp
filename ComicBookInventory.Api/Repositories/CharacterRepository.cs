using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.DataAccess;
using ComicBookInventory.Shared;

namespace ComicBookInventory.Api.Services
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CharacterRepository
    {
        private ApiDbContext _dbContext;

        public CharacterRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<Character> GetAllCharacters()
        {
            return _dbContext.Characters.ToList();
        }

        [HttpPost]
        public void AddCharacter(CharacterViewModel model)
        {
            var _character = new Character()
            {
                FullName = model.FullName,
            };
            _dbContext.Characters.Add(_character);
            _dbContext.SaveChanges();
        }
    }
}
