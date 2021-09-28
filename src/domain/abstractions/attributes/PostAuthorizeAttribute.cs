namespace example.domain.abstractions.attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class PostAuthorizeAttribute : System.Attribute 
    {
        public PostAuthorizeAttribute(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new System.ArgumentException($"'{nameof(method)}' cannot be null or whitespace.", nameof(method));
            }
            Method = method;
        }

        public string Method { get; }
    }

}