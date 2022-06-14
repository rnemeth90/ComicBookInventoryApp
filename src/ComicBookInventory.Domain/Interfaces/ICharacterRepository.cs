namespace ComicBookInventory.Shared
{
    public interface ICharacterRepository : IGenericRepository<Character>
    {
        CharacterViewModel GetCharacterById(int id);
        void AddCharacter(CharacterViewModel character);
        void UpdateCharacter(int id, CharacterViewModel character); 
    }
}
