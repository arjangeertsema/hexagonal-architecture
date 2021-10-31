using System;
using Common.CQRS.Abstractions;
using Domain.Abstractions.ValueTypes;

namespace Domain.Abstractions.Ports
{
    public class SendMessagePort : ICommand
    {
        public SendMessagePort(Guid commandId, Message message)
        {
            CommandId = commandId;
            Message = message;
            
        }

        public Guid CommandId { get; }
        public Message Message { get; }
    }
}