namespace WordNET_Server_2._0.DTOs
{
    public class AssociatedWordDTO
    {
        public int WordId { get; set; }

        public string Name { get; set; } = null!;
        
        public bool IsMan { get; set; }
        public int HumanAge { get; set; }
    }
}
