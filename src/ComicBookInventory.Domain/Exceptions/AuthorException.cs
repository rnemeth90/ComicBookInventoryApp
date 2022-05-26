namespace ComicBookInventory.Exceptions
{
    public class AuthorException : Exception
    {
        public string AuthorName { get; set; }

        public AuthorException()
        {

        }

        public AuthorException(string message) : base(message)
        {

        }

        public AuthorException(string message, Exception inner) : base(message, inner)
        {

        }

        public AuthorException(string message, string authorName) : base(message)
        {
            AuthorName = authorName;
        }
    }
}
