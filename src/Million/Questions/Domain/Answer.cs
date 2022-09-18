using System.ComponentModel.DataAnnotations.Schema;

namespace Million.Questions.Domain;

/// <summary>
/// Represent a question answer.
/// </summary>
public class Answer
{
    private Answer()
    {
        // EF Core
    }

    [Key] public int Id { get; set; }

    [MaxLength(50)] public string Content { get; set; } = default!;
    public bool IsCorrect { get; set; }

    [ForeignKey("QuestionId")] public Question Question { get; set; } = default!;
    public int QuestionId { get; set; }

    public static Answer Create(string content, bool isCorrect, Question question)
    {
        return new Answer
        {
            Content = content,
            IsCorrect = isCorrect,
            Question = question
        };
    }
}
