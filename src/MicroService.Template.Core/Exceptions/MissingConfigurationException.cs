namespace MicroService.Template.Core.Exceptions
{
    using System;

    public class MissingConfigurationException : CustomException
    {
        public MissingConfigurationException()
            : base("A required configuration is missing.")
        {
        }

        public MissingConfigurationException(string message)
            : base(404, message)
        {
        }

        public MissingConfigurationException(string message, Exception inner)
            : base(404, message, inner)
        {
        }
    }

}
