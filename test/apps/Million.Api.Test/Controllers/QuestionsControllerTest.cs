using Million.Api.Test.Shared;
using Million.Questions.Application;
using Million.Test.Questions.Domain;
using Newtonsoft.Json;

namespace Million.Api.Test.Controllers;

public class QuestionsControllerTest : ApplicationContextTestCase
{
    public QuestionsControllerTest(ApplicationTestCase applicationTestCase) : base(applicationTestCase)
    {
    }

    [Fact]
    public async Task GetQuestions()
    {
        // Act
        var response = await Client.GetAsync("/api/questions/1");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var questions = JsonConvert.DeserializeObject<List<QuestionResponse>>(responseString);

        // Assert
        questions.Should().BeEmpty();

        // Arrange
        var question = QuestionMother.Random(1);
        var dbContext = GetDbContext();
        await dbContext.Questions.AddAsync(question);
        await dbContext.SaveChangesAsync();

        // Act
        response = await Client.GetAsync("/api/questions/1");
        response.EnsureSuccessStatusCode();

        responseString = await response.Content.ReadAsStringAsync();
        questions = JsonConvert.DeserializeObject<List<QuestionResponse>>(responseString);

        // Assert
        questions.Should().HaveCount(1);
        questions.First().Answers.Should().HaveCount(4);
    }
}
