namespace Million.Categories.Domain;

public class Category
{
    private Category()
    {
        // Ef Core
    }

    [Key] public int Id { get; set; }


    [Required] [MaxLength(50)] public string Name { get; set; } = default!;
    [Required] public int Difficulty { get; set; }

    public static Category Create(string name, int difficulty)
    {
        return new Category
        {
            Name = name,
            Difficulty = difficulty
        };
    }
}
