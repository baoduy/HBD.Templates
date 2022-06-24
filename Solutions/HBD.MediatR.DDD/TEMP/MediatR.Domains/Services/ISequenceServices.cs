namespace MediatR.Domains.Services;

public interface ISequenceServices : IDomainService
{
    #region Methods

    ValueTask<string> NextValueAsync();

    #endregion Methods
}