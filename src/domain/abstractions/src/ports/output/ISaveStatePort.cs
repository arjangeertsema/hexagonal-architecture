using System;
using System.Collections.Generic;

namespace example.domain.abstractions.ports.output
{
    public interface ISaveStatePort : IOutputPort<ISaveStatePort.Command>
    {
        public class Command : ICommand
        {
            public Command(Guid commandId, IEnumerable<KeyValuePair<string, string>> state)
            {
                if (state is null)
                {
                    throw new ArgumentNullException(nameof(state));
                }

                CommandId = commandId;
                State = state;
            }

            public Guid CommandId { get; }
            public IEnumerable<KeyValuePair<string, string>> State { get; }
        }
    }
}