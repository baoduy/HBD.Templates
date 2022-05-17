using System;
using System.Threading.Tasks;
using AutoBogus;
using FluentAssertions;
using HBD.EfCore.BizAction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEMP.AppServices.BizActions.Profiles;
using TEMP.AppServices.Models.Profiles;
using TEMP.Domains.Repositories;
using TEMP.Infras.EventHandlers;

namespace TEMP.AppServices.Tests
{
    [TestClass]
    public class ProfileTests
    {
        private Guid _id;
        [TestMethod]
        public async Task CreateProfileAsync()
        {
            ProfileCreatedHandler.Called = 0;
            var reader = Initialize.Provider.GetRequiredService<IProfileRepo>();
            var sv = Initialize.Provider.GetRequiredService<IActionServiceAsync<ICreateProfileAction>>();

            var m = AutoFaker.Generate<ProfileModel>();

            m.Title = m.Title[..10];
            m.Id = null;

            var rs = await sv.RunBizActionAsync<ProfileBasicView>(m).ConfigureAwait(false);
            sv.Status.HasErrors.Should().BeFalse(sv.Status.GetAllErrors());

            rs.Should().NotBeNull();
            rs.Id.Should().NotBeEmpty();

            (await reader.FindAsync(rs.Id).ConfigureAwait(false)).Should().NotBeNull();
            ProfileCreatedHandler.Called.Should().Be(1);
            _id = rs.Id;
        }

        [TestMethod]
        public async Task UpdateProfileAsync()
        {
            await CreateProfileAsync().ConfigureAwait(false);
            
            var sv = Initialize.Provider.GetRequiredService<IActionServiceAsync<IUpdateProfileAction>>();
            var m = AutoFaker.Generate<ProfileModel>();

            m.Title = m.Title[..10];
            m.Id = _id;

            var rs = await sv.RunBizActionAsync<ProfileBasicView>(m).ConfigureAwait(false);
            sv.Status.HasErrors.Should().BeFalse(sv.Status.GetAllErrors());

            rs.Name.Should().Be(m.Name.ToString());
        }
        
        [TestMethod]
        public async Task DeleteProfileAsync()
        {
            await CreateProfileAsync().ConfigureAwait(false);
            
            var reader = Initialize.Provider.GetRequiredService<IProfileRepo>();
            var sv = Initialize.Provider.GetRequiredService<IActionServiceAsync<IDeleteProfileAction>>();
            var m = new ProfileDeleteModel {Id = _id};

            var rs = await sv.RunBizActionAsync<ProfileBasicView>(m).ConfigureAwait(false);
            sv.Status.HasErrors.Should().BeFalse(sv.Status.GetAllErrors());
            rs.Should().NotBeNull();
            
            (await reader.FindAsync(_id).ConfigureAwait(false)).Should().BeNull();
        }
    }
}