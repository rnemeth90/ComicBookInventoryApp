namespace ComicBookInventory.Shared
{
    public class ComicBook_Character
    {
        public int Id { get; set; }
        
        // navigation properties
        public int? ComicBookId { get; set; }
        public ComicBook ComicBook { get; set; }

        public int? CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
