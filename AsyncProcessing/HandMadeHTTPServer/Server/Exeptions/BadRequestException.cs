namespace _08.HandMadeHTTPServer.Server.Exeptions
{
    using System;
    public class BadRequestException : ArgumentNullException
    {
        public string Message { get; set; }

        public BadRequestException(string message)
        {
            this.Message = message;
        }
    }
}
