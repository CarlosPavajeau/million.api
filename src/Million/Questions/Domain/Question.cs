using System.ComponentModel.DataAnnotations.Schema;
using Million.Categories.Domain;

namespace Million.Questions.Domain;

/// <summary>
/// Represent a game question with a category and reward
/// </summary>
public class Question
{
    public Question()
    {
        // Ef core
    }

    [Key] public int Id { get; set; }

    [Required] [MaxLength(120)] public string Content { get; set; } = default!;

    /// <summary>
    /// Reward for answering the question, can be points or money
    /// </summary>
    [Required]
    public float Reward { get; set; }

    [Required] public int CategoryId { get; set; }

    [ForeignKey("CategoryId")] public Category Category { get; set; } = default!;

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();


    public static Question Create(string content, float reward, int categoryId)
    {
        return new Question
        {
            Content = content,
            Reward = reward,
            CategoryId = categoryId
        };
    }
}
