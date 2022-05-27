namespace ComicBookInventory.Shared
{
    public interface ICharacterRepository : IGenericRepository<Character>
    {
        void AddCharacter(CharacterViewModel character);
        void UpdateCharacter(int id, CharacterViewModel character); 
    }
}
