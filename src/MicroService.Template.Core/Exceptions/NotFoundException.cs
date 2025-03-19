namespace MicroService.Template.Core.Exceptions
{
    using System;

    public class NotFoundException : CustomException
    {
        public NotFoundException(string message)
            : base(404, message) // 404 Not Found
        {
        }

        public NotFoundException(string message, Exception inner)
            : base(404, message, inner)
        {
        }
    }



}
