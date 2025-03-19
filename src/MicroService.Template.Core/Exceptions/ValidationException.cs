namespace MicroService.Template.Core.Exceptions
{
    using System;

    public class ValidationException : CustomException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(string message)
            : base(400, message) // 400 Bad Request
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string message, IDictionary<string, string[]> errors)
            : base(400, message)
        {
            Errors = errors;
        }

        public ValidationException(string message, Exception inner)
            : base(400, message, inner)
        {
            Errors = new Dictionary<string, string[]>();
        }
    }



}
