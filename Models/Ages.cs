namespace WordNET_Server_2._0.Models
{
    public class Ages
    {
        public int Id { get; set; }
        public bool IsMan { get; set; }
        public int Age { get; set; }

        public int StatisticsId { get; set; }
        public Statistics Statistics { get; set; } = null!;
    }
}
