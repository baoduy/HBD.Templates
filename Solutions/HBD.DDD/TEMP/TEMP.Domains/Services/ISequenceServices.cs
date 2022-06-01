using System.Threading.Tasks;

namespace TEMP.Domains.Services;

public interface ISequenceServices : IDomainService
{
    #region Methods

    ValueTask<string> NextValueAsync();

    #endregion Methods
}