namespace Million.Score.Domain;

public class Score
{
    public Score()
    {
        // For EF
    }

    [Key] public int Id { get; set; }

    [Required] public int Value { get; set; }

    [Required] public DateTime Date { get; set; }

    [Required] [MaxLength(50)] public string PlayerName { get; set; } = default!;

    public static Score Create(int value, string playerName)
    {
        return new Score
        {
            Value = value,
            Date = DateTime.Now,
            PlayerName = playerName
        };
    }
}
