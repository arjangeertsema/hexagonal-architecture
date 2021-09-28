namespace example.domain.abstractions.attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class PreAuthorizeAttribute : System.Attribute 
    {
        public PreAuthorizeAttribute(string method)
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