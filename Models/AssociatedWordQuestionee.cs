namespace WordNET_Server_2._0.Models
{
    public class AssociatedWordQuestionee
    {
        public int AssociatedWordId { get; set; }
        public AssociatedWord AssociatedWord { get; set; } = null!;

        public int QuestioneeId { get; set; }
        public Questionee Questionee { get; set; } = null!;
    }
}
