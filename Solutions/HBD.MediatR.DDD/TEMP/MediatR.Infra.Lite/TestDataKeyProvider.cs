using HBD.EfCore.DataAuthorization;

namespace MediatR.Infra.Lite
{
    public class TestDataKeyProvider : IDataKeyProvider
    {
        #region Fields

        private readonly Guid _key;

        #endregion Fields

        #region Constructors

        public TestDataKeyProvider()
        {
            _key = Guid.NewGuid();
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<Guid> GetImpersonateKeys()
        {
            return new[] {_key};
        }

        public Guid GetOwnershipKey()
        {
            return _key;
        }

        #endregion Methods
    }
}