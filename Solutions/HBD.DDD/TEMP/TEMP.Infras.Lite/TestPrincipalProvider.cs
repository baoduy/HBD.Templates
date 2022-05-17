using System;
using System.Collections.Generic;
using TEMP.AppServices;

namespace TEMP.Infras.Lite
{
    public class TestPrincipalProvider : IPrincipalProvider
    {
        #region Constructors

        public TestPrincipalProvider()
        {
            ProfileId = new Guid("F86A2EB5-7AF4-4D8C-A634-542D32CAB547");
            UserName = "Unit Test";
            Email = "UnitTest@cloudcoders.net";
        }

        #endregion Constructors

        #region Properties

        public string Email { get; }

        public Guid ProfileId { get; }

        public string UserName { get; }

        #endregion Properties

        #region Methods

        public IEnumerable<Guid> GetImpersonateKeys()
        {
            yield return ProfileId;
        }

        public Guid GetOwnershipKey()
        {
            return ProfileId;
        }

        #endregion Methods
    }
}