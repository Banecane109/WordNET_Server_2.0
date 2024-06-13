namespace WordNET_Server_2._0.DTOs
{
    public class StatisticsDTO
    {
        public int Id { get; set; }

        public int ManCount { get; set; }
        public double ManAverageAge { get; set; }

        public int WomanCount { get; set; }
        public double WomanAverageAge { get; set; }

        public int AssociatedWordId { get; set; }
    }
}
