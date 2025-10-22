namespace GameModel{
public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string QuestionSentence { get; set; }
    public required ICollection<Option> Options { get; set; } = new List<Option>();
    public required int Difficulty { get; set; } = 1;
}
[Owned]
public class Option
{
    public required string Text { get; set; }
    public required bool IsRightAnswer { get; set; }
}
}

