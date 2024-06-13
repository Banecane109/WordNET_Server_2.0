namespace WordNET_Server_2._0.Models
{
    public class AssociatedWord
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Count { get; set; }

        public int WordId { get; set; }
        public Word Word { get; set; } = null!;

        public int StatisticsId { get; set; }
        public Statistics Statistics { get; set; } = null!;
    }
}
