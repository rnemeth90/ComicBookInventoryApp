namespace ComicBookInventory.Shared
{
    public class HomeIndexViewModel
    {
        private int _visitorCount;
        private IList<ComicBookWithAuthorsAndCharactersViewModel> _comicBooks;

        public int VisitorCount { get => _visitorCount; set => _visitorCount = value; }
        public IList<ComicBookWithAuthorsAndCharactersViewModel>? ComicBooks { get => _comicBooks; 
                                                                              set => _comicBooks = value; }
        public HomeIndexViewModel(int visitorCount, IList<ComicBookWithAuthorsAndCharactersViewModel> comicBooks)
        {
            _visitorCount = visitorCount;
            _comicBooks = comicBooks;   
        }
    }
}
