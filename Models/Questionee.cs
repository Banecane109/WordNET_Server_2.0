namespace WordNET_Server_2._0.Models
{
    public class Questionee
    {
        public int Id { get; set; }

        public bool IsMan { get; set; }
        public int Age { get; set; }

        public ICollection<AssociatedWordQuestionee> AssociatedWordQuestionees { get; set; } = [];
    }
}
