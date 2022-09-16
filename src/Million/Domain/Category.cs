namespace Million.Domain;

public class Category
{
    private Category()
    {
        // Ef Core
    }

    [Key] public int Id { get; set; }


    [Required] [MaxLength(50)] public string Name { get; set; }
    [Required] public int Difficulty { get; set; }

    public static Category Create(int difficulty)
    {
        return new Category
        {
            Difficulty = difficulty
        };
    }
}
