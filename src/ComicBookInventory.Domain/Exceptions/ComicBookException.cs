namespace ComicBookInventory.Exceptions
{
    internal class ComicBookException : Exception
    {
        public string ComicBookName{ get; set; }

        public ComicBookException()
        {

        }

        public ComicBookException(string message) : base(message)
        {

        }

        public ComicBookException(string message, Exception inner) : base(message, inner)
        {

        }

        public ComicBookException(string message, string comicBookName) : base(message)
        {
            comicBookName = comicBookName;
        }
    }
}
