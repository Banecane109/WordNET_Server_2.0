namespace WordNET_Server_2._0.Models
{
    public class Statistics
    {
        public int Id { get; set; }

        public int ManCount { get; set; }
        public double ManAvarageAge { get; set; }

        public int WomanCount { get; set; }
        public double WomanAvarageAge { get; set; }

        public int? AssociatedWordId { get; set; }
    }
}
