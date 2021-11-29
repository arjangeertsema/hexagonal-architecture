namespace Adapters.Zeebe.Tests.Steps;

/// TODO: move this implementation to the living documentation build pipeline. Linting and Testing BPMN files is a business responsability.

[Binding]
public class ProcessStepDefintions
{
    private readonly IBPMNTestBuilder testBuilder;
    private readonly Random random;
    private IBPMNTestBuilderStep1? step1;    private IBPMNTestBuilderStepGateway? stepGateway;
    
    public ProcessStepDefintions(IBPMNTestBuilder testBuilder)
    {
        this.testBuilder = testBuilder ?? throw new System.ArgumentNullException(nameof(testBuilder));
        this.random = new Random();
    }

    [Given(@"an asked question")]
    public void GivenAnAskedQuestion()
    {
        this.step1.Should().BeNull();
        this.step1 = testBuilder.FromUri(Adapters.Zeebe.ZeebeService.BPMN_FILE);
    }

    [When(@"the bot answers the question with a probability greater or equal to (.*)% and smaller than (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilityGreaterOrEqualToAndSmallerThan(int p0, int p1)
    {
        this.step1.Should().NotBeNull();
        this.step1 = IsNotNull(this.step1)
            .SetState(CreateState(random.Next(p0, p1 - 1)));
    }

    [When(@"the bot answers the question with a probability smaller than (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilitySmallerThan(int p0)
    {
        this.step1.Should().NotBeNull();
        this.step1 = IsNotNull(this.step1)
            .SetState(CreateState(random.Next(0, p0 - 1)));
    }

    [When(@"the bot answers the question with a probability greater or equal to (.*)%")]
    public void WhenTheBotAnswersTheQuestionWithAProbabilityGreaterOrEqualTo(int p0)
    {
        this.step1.Should().NotBeNull();
        this.step1 = IsNotNull(this.step1)
            .SetState(CreateState(random.Next(p0, 100)));
    }

    [When(@"the gateway with id ""(.*)"" is executed")]
    public void WhenTheGatewayWithIdIsExecuted(string p0)
    {
        this.step1.Should().NotBeNull();
        this.stepGateway.Should().BeNull();
        this.stepGateway = IsNotNull(this.step1)
            .ExecuteGateway(p0);
    }

    [Then(@"the flow with id ""(.*)"" must be activated")]
    public void ThenTheFlowWithIdMustBeActivated(string p0)
    {
        var result = IsNotNull(this.stepGateway)
            .ExpectFlowActivated(p0)
            .Execute();

        result.Should().BeTrue();
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

    #endregion
}
