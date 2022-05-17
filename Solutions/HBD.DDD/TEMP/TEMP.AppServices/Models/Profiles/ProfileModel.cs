using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TEMP.AppServices.Abstracts;
using TEMP.Domains.ValueObjects;
using Profile = TEMP.Domains.Aggregators.Profile;

namespace TEMP.AppServices.Models.Profiles
{
    [AutoMap(typeof(Profile), ReverseMap = true)]
    public class ProfileModel : ModelBase
    {
        #region Properties

        [Required] public string Email { get; set; }

        [StringLength(100)] [Required] public string Firstname { get; set; }

        [StringLength(100)] [Required] public string LastName { get; set; }

        [Phone] public string Phone { get; set; }

        [StringLength(10)] [Required] public string Title { get; set; }

        internal Guid AdAccountId { get; set; }

        internal string MembershipNo { get; set; }

        //[BindNever]
        internal PersonName Name => new PersonName(Title, Firstname, LastName);

        #endregion Properties
    }
}