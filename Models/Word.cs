namespace WordNET_Server_2._0.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<AssociatedWord> AssociatedWords { get; set; } = [];
    }
}
