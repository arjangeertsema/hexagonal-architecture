namespace Domain.UseCases.Tests.Steps;

[Binding]
public class ProcessStepDefintions
{
    private const string PROCESS_DEFINTION_FILE = "Domain.UseCases.Tests.Generated.Resources.process.bpmn";
    private readonly IBPMNTestBuilder testBuilder;
    private readonly Random random;
    private IBPMNTestBuilderStep1? step1;
    private IBPMNTestBuilderStep2? step2;
    private IBPMNTestBuilderStep3? step3;

    public ProcessStepDefintions(IBPMNTestBuilder testBuilder)
    {
        this.testBuilder = testBuilder ?? throw new System.ArgumentNullException(nameof(testBuilder));
        this.random = new Random();
    }

    [Given(@"an asked question")]
    public async Task GivenAnAskedQuestion()
    {
        this.step1.Should().BeNull();
        string definition = await GetEmbeddedResourceAsString(PROCESS_DEFINTION_FILE);
        definition.Should().NotBeNullOrWhiteSpace();
        this.step1 = testBuilder.FromDefinition(definition);
    }

    [When(@"the bot answers the question with a probability greater or equal the (.*)% and smaller then (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilityGreaterOrEqualTheAndSmallerThen(int p0, int p1)
    {
        this.step2.Should().BeNull();
        this.step2 = IsNotNull(this.step1).SetState(CreateState(random.Next(p0, p1 - 1)));
    }

    [When(@"the bot answers the question with a probability smaller then (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilitySmallerThen(int p0)
    {
        this.step2.Should().BeNull();
        this.step2 = IsNotNull(this.step1).SetState(CreateState(random.Next(0, p0 - 1)));
    }

    [When(@"the bot answers the question with a probability greater or equal then (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilityGreaterOrEqualThen(int p0)
    {
        this.step1.Should().NotBeNull();
        this.step2 = IsNotNull(this.step1).SetState(CreateState(random.Next(p0, 100)));
    }

    [When(@"the gateway ""(.*)"" is executed")]
    public void WhenTheGatewayIsExecuted(string p0)
    {
        this.step3.Should().BeNull();
        this.step3 = IsNotNull(this.step2).ExecuteGateway(p0);
    }

    [Then(@"the flow ""(.*)"" must be activated")]
    public void ThenTheFlowMustBeActivated(string p0)
    {
        var test = IsNotNull(this.step3).ExpectFlowActivated(p0).Build();
        test.Execute().Should().BeTrue();
    }

    #region Private

    private static object CreateState(int probability)
    {
        return new { AnswerCorrectProbability = probability };
    }

    private static T IsNotNull<T>(T? t, [CallerArgumentExpression("t")] string? message = null)
    {
        t.Should().NotBeNull();

        if (t == null)
            throw new ArgumentNullException(message);

        return t;
    }

    private Task<string> GetEmbeddedResourceAsString(string name)
    {
        var stream = this.GetType().Assembly.GetManifestResourceStream(name);
        using (var reader = new StreamReader(IsNotNull(stream), Encoding.UTF8))
        {
            return reader.ReadToEndAsync();
        }
    }

    #endregion
}
