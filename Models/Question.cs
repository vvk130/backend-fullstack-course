namespace GameModel{
public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string QuestionSentence { get; set; }
    public ICollection<Option> Options { get; set; } = new List<Option>();
    public int Difficulty { get; set; } = 1;
}
[Owned]
public class Option
{
    public string Text { get; set; }
    public bool IsRightAnswer { get; set; }
}
}

