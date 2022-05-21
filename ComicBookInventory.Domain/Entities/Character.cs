namespace ComicBookInventory.Api.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // navigation properties
        public List<ComicBook_Character> ComicBook_Characters { get; set; }
    }
}
