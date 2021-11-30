namespace Adapters.Zeebe.Tests.Steps;

/// TODO: move this implementation to the living documentation build pipeline. Linting and Testing BPMN files is a business responsability.

[Binding]
public class ProcessStepDefintions
{
    private readonly IBPMNTestBuilder testBuilder;
    private IBPMNTestBuilderStep1 step1;
    private IBPMNTestBuilderStepGateway stepGateway;

    public ProcessStepDefintions(IBPMNTestBuilder testBuilder) => this.testBuilder = testBuilder ?? throw new System.ArgumentNullException(nameof(testBuilder));

    [Given(@"process ""(.*)""")]
    public void GivenProcess(string p0)
    {
        this.step1.Should().BeNull();

        this.step1 = testBuilder
            .FromUri(new Uri(p0));
    }


    [Given(@"process has state ""(.*)""")]
    public void GivenProcessHasState(string p0)
    {
        this.step1.Should().NotBeNull();

        this.step1.SetState(CreateState(p0));
    }

    [When(@"the gateway with id ""(.*)"" is executed")]
    public void WhenTheGatewayWithIdIsExecuted(string p0)
    {
        this.step1.Should().NotBeNull();
        this.stepGateway.Should().BeNull();

        this.stepGateway = this.step1
            .ExecuteGateway(p0);
    }

    [Then(@"the flow with id ""(.*)"" must be activated")]
    public void ThenTheFlowWithIdMustBeActivated(string p0)
    {
        this.stepGateway.Should().NotBeNull();

        var result = this.stepGateway
            .ExpectFlowActivated(p0)
            .Execute();

        result.Should().BeTrue();
    }

    #region Private

    private static object CreateState(string json)
    {
        return JsonSerializer.Deserialize<Object>(json);
    }

    #endregion
}
