using System.Text;
using Million.Api.Test.Shared;
using Million.Questions.Application;
using Million.Questions.Application.Create;
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

        // Cleanup
        dbContext.Questions.Remove(question);
        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task ValidateAnswer()
    {
        // Arrange
        var question = QuestionMother.Random(1);
        var dbContext = GetDbContext();
        await dbContext.Questions.AddAsync(question);
        await dbContext.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"/api/questions/validate/{question.Answers.First().Id}");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var answer = JsonConvert.DeserializeObject<bool>(responseString);

        // Assert
        answer.Should().BeFalse();

        // Act
        response = await Client.GetAsync($"/api/questions/validate/{question.Answers.First(x => x.IsCorrect).Id}");
        response.EnsureSuccessStatusCode();

        responseString = await response.Content.ReadAsStringAsync();
        answer = JsonConvert.DeserializeObject<bool>(responseString);

        // Assert
        answer.Should().BeTrue();

        // Cleanup
        dbContext.Questions.Remove(question);
        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateQuestion()
    {
        // Arrange
        var question = QuestionMother.Random(1);
        var answers = question.Answers
            .Select(x => new CreateAnswerCommand(x.Content, x.IsCorrect))
            .ToList();
        var command = new CreateQuestionCommand(question.Content, question.Reward, answers);

        // Act
        var response = await Client.PostAsync("/api/questions",
            new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        var questionId = JsonConvert.DeserializeObject<int>(responseString);
        
        // Assert
        questionId.Should().BeGreaterThan(0);
    }
}
