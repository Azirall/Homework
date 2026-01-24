using System;

public class NewsLoadException : Exception
{
    public NewsLoadException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
