using AutoBogus;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEMP.AppServices.Models.Profiles;
using TEMP.Domains.Events;
using Profile = TEMP.Domains.Aggregators.Profile;

namespace TEMP.AppServices.Tests;

[TestClass]
public class AutoMapperTests
{
    #region Methods

    [TestMethod]
    public void Create_Profile_FromModel()
    {
        var mapper = Initialize.Provider.GetService<IMapper>();

        var m = AutoFaker.Generate<ProfileModel>();
        var p = mapper.Map<Profile>(m);

        p.Should().NotBeNull();
        p.Email.Should().Be(m.Email);
        p.Phone.Should().Be(m.Phone);
        p.Name.Should().NotBeNull();
        p.CreatedBy.Should().NotBeEmpty();
    }

    [TestMethod]
    public void Create_Profile_ToViewModel()
    {
        var mapper = Initialize.Provider.GetService<IMapper>();

        var m = AutoFaker.Generate<Profile>();
        var p = mapper.Map<ProfileBasicView>(m);

        p.Should().NotBeNull();
        p.Id.Should().Be(m.Id);
    }

    [TestMethod]
    public void Create_Profile_ToEvents()
    {
        var mapper = Initialize.Provider.GetService<IMapper>();

        var m = AutoFaker.Generate<Profile>();
        var p = mapper.Map<ProfileCreatedEvent>(m);

        p.Should().NotBeNull();
        p.Id.Should().Be(m.Id);
    }

    #endregion Methods
}