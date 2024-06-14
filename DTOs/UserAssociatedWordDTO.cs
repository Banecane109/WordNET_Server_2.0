namespace WordNET_Server_2._0.DTOs
{
    public class UserAssociatedWordListDTO
    {
        public bool IsMan { get; set; }
        public int Age { get; set; }

        public Dictionary<int, string> KeyWordValueAssociatedWord { get; set; } = [];
    }
}
