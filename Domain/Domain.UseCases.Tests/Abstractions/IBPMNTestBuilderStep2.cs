namespace Domain.UseCases.Tests.Abstractions
{
    public interface IBPMNTestBuilderStep2
    {
        IBPMNTestBuilderStep3 ExecuteGateway(string gatewayId);
        IBPMNTestBuilderStep4 ExecuteDMN(string dmnId);
    }
}