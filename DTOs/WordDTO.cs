namespace WordNET_Server_2._0.DTOs
{
    public class WordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public IEnumerable<AssociatedWordDTO> AssociatedWords { get; set; } = [];
    }
}
