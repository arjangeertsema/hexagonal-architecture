using Synion.CQRS.Abstractions.Ports;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class GetIdentityPort : IOutputPort<GetIdentityPort.Response>
    {
        public GetIdentityPort() { }

        public class Response
        {
            public Response(string id)
            {
                Id = id;
            }

            public string Id { get; }
        }
    }
}