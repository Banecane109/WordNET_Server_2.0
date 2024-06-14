namespace WordNET_Server_2._0.Models
{
    public class Statistics
    {
        public int Id { get; set; }

        public ICollection<Ages> Ages { get; set; } = [];

        public int? AssociatedWordId { get; set; }
        public AssociatedWord AssociatedWord { get; set; } = null!;
    }
}
