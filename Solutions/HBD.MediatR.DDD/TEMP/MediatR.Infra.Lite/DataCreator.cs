using AutoBogus;
using AutoMapper;
using MediatR.AppServices.Models.Profiles;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataCreator
    {
        public static Profile CreateProfile(this IServiceProvider serviceProvider)
        {
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            var m = AutoFaker.Generate<ProfileModel>();
            m.UserId = "Duy";

            return mapper.Map<Profile>(m);
        }
    }
}