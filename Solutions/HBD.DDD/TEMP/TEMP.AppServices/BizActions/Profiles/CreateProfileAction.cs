using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TEMP.AppServices.Models.Profiles;
using TEMP.Domains.Events;
using TEMP.Domains.Repositories;
using TEMP.Domains.Services;
using Profile = TEMP.Domains.Aggregators.Profile;

namespace TEMP.AppServices.BizActions.Profiles
{
    internal sealed class CreateProfileAction : BizActionAsync<Profile>, ICreateProfileAction
    {
        private readonly IMembershipService _membershipProvider;

        public CreateProfileAction(IMembershipService membershipProvider,
            IPrincipalProvider principalProvider,
            IMapper mapper,
            IProfileRepo repository)
            : base(principalProvider, mapper,repository)
        {
            _membershipProvider = membershipProvider;
        }

        public async Task<Profile> BizActionAsync(ProfileModel inputData, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(inputData.MembershipNo))
                inputData.MembershipNo = await _membershipProvider.NextValueAsync().ConfigureAwait(false);

            var profile = new Profile(inputData.Name,
                inputData.MembershipNo,
                inputData.AdAccountId,
                inputData.Email,
                inputData.Phone,
                inputData.UserId);

            //Event
            profile.AddEvent(new ProfileCreatedEvent(profile.Id, profile.Name));

            await Repository.AddAsync(profile, cancellationToken);

            return profile;
        }
    }
}