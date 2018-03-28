namespace MyCoolWebServer.Server.Exeptions
{
    using System;

    public class InvalidResponseExeption : Exception
    {
        public InvalidResponseExeption(string message)
            :base(message)
        {         
        }
    }
}
