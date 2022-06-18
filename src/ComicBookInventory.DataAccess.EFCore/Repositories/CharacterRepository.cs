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
                FullName = c.FullName,
                PrimaryAbility = c.PrimaryAbility,
                SecondaryAbility = c.SecondaryAbility,
                Species = c.Species,
                Alias = c.Alias,
                IsAlive = c.IsAlive,
                Weapon = c.Weapon
            }).ToList();
            return characters;
        }

        public CharacterViewModel GetCharacterById(int id)
        {
            try
            {
                var entity = DbContext.Characters.Where(a => a.Id == id)
                                        .Select(c => new CharacterViewModel()
                                        {
                                            Id = c.Id,
                                            FullName = c.FullName,
                                            PrimaryAbility = c.PrimaryAbility,
                                            SecondaryAbility = c.SecondaryAbility,
                                            Species = c.Species,
                                            Alias = c.Alias,
                                            IsAlive = c.IsAlive,
                                            Weapon = c.Weapon
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
                PrimaryAbility = model.PrimaryAbility,
                SecondaryAbility = model.SecondaryAbility,
                Species = model.Species,
                Alias = model.Alias,
                IsAlive = model.IsAlive,
                Weapon = model.Weapon
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
                entity.PrimaryAbility = model.PrimaryAbility;
                entity.SecondaryAbility = model.SecondaryAbility;
                entity.Species = model.Species;
                entity.Alias = model.Alias;
                entity.IsAlive = model.IsAlive;
                entity.Weapon = model.Weapon;
                DbContext.SaveChanges();
            }
            else
            {
                throw new CharacterException($"Character with id {id} not found");
            }
        }
    }
}
