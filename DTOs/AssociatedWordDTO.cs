namespace WordNET_Server_2._0.DTOs
{
    public class AssociatedWordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Count { get; set; }

        public IEnumerable<AgesDTO> Ages { get; set; } = [];

        public int WordId { get; set; } 
    }
}
