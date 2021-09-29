using System;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public interface IGetIdentityPort : IOutputPort<IGetIdentityPort.Query, IGetIdentityPort.Response>
    {
        public class Query : IQuery<IGetIdentityPort.Response>
        {
            public Query() { }
        }

        public class Response
        {
            public Response(string id)
            {
                if (id is null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                Id = id;
            }

            public string Id { get; }
        }
    }
}