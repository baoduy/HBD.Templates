using FluentResults;

namespace MediatR.AppServices.Share;

public class BizCommandError : Error
{
    public BizCommandError(string message,string fileName) : base(message) => Metadata.Add(fileName, message);
}