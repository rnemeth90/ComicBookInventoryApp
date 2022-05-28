namespace ComicBookInventory.Exceptions
{
    public class CharacterException : Exception
    {
        public string CharacterName { get; set; }

        public CharacterException()
        {

        }

        public CharacterException(string message) : base(message)
        {

        }

        public CharacterException(string message, Exception inner) : base(message, inner)
        {

        }

        public CharacterException(string message, string characterName) : base(message)
        {
            CharacterName = characterName;
        }
    }
}
