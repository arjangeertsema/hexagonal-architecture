namespace Domain.UseCases.Tests.Abstractions
{
    public interface IBPMNTestBuilderStep3
    {
        IBMNTestStepFinal ExpectFlowActivated(string routeId);
    }
}