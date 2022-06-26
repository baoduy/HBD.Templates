namespace MediatR.AppServices.Share.Exceptions;

public sealed class BizCommandException : Exception
{
    public string[] Fields { get; }

    public BizCommandException(string message, params string[] fields) : this(message, fields, default)
    {
    }

    public BizCommandException(string message, string[] fields, Exception? innerException) : base(message,
        innerException)
    {
        Fields = fields;
    }
}