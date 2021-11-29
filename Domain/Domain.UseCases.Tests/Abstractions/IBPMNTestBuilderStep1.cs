namespace Domain.UseCases.Tests.Abstractions
{
    public interface IBPMNTestBuilderStep1
    {
        IBPMNTestBuilderStep2 SetState(object state);
    }
}