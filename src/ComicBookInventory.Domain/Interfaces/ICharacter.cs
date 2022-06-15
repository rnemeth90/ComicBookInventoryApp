namespace ComicBookInventory.Shared
{
    public interface ICharacter
    {
        public string FullName { get; set; }
        public string PrimaryAbility { get; set; }
        public string? SecondaryAbility { get; set; }
        public string? Species { get; set; }
        public string? Alias { get; set; }
        public bool IsAlive { get; set; }
        public string? Weapon { get; set; }
    }
}
