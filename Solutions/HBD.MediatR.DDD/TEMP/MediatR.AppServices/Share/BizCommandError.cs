using FluentResults;

namespace MediatR.AppServices.Share;

public class BizError : Error
{
    public BizError(string message,string fileName) : base(message) => Metadata.Add(fileName, message);
}