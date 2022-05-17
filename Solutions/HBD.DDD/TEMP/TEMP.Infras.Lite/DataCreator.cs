using System;
using AutoBogus;
using AutoMapper;
using TEMP.AppServices.Models.Profiles;
using Profile = TEMP.Domains.Aggregators.Profile;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataCreator
    {
        #region Methods

        public static Profile CreateProfile(this IServiceProvider serviceProvider)
        {
            var mapper = serviceProvider.GetService<IMapper>();

            var m = AutoFaker.Generate<ProfileModel>();
            m.UserId = "Duy";

            return mapper.Map<Profile>(m);
        }

        #endregion Methods
    }
}