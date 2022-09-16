using Million.Categories.Domain;

namespace Million.Test.Categories.Domain;

public static class CategoryMother
{
    public static Category Random()
    {
        var faker = new Faker();
        return Category.Create(faker.Random.Word(), faker.Random.Number(1, 10));
    }
}
