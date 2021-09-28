using System;

namespace example.domain.abstractions.ports.input
{
    public interface IAcceptAnswerUseCase : IInputPort<IAcceptAnswerUseCase.Command>
    {
        public class Command : ICommand
        {
            public Guid CommandId => throw new NotImplementedException();
        }
    }
}
