using System;

namespace Reference.Domain.Abstractions.Ports.Output
{
    public class GetIdentity : IOutputPort<GetIdentity.Response>
    {
        public GetIdentity() { }

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