using Million.Questions.Domain;

namespace Million.Test.Questions.Domain;

public static class QuestionMother
{
    public static Question Random(int categoryId)
    {
        var faker = new Faker();
        
        var question = Question.Create(faker.Random.Words(10), faker.Random.Float(1, 100), categoryId);

        question.Answers.Add(Answer.Create(faker.Random.Word(), false, question));
        question.Answers.Add(Answer.Create(faker.Random.Word(), false, question));
        question.Answers.Add(Answer.Create(faker.Random.Word(), false, question));
        question.Answers.Add(Answer.Create(faker.Random.Word(), true, question));

        return question;
    }
}
