using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TEMP.Domains.Abstracts;
using TEMP.Domains.ValueObjects;

namespace TEMP.Domains.Aggregators
{
    [Table("Profiles", Schema = DomainSchemas.Profile)]
    public class Profile : AggregateRoot
    {
        //private HashSet<Account> _accounts = new HashSet<Account>();

        #region Constructors

        public Profile([NotNull] PersonName name, string memberShipNo, Guid adAccountId, string email, string phone,
            string userId)
            : base(userId)
        {
            Email = email;
            Phone = phone;
            MembershipNo = memberShipNo;
            AdAccountId = adAccountId;

            UpdateName(name ?? new PersonName(), userId);
        }

        private Profile()
        {
        }

        #endregion Constructors

        //public IReadOnlyCollection<Account> Accounts => _accounts;

        #region Properties

        public Guid AdAccountId { get; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [MaxLength(50)] public string Avatar { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Column(TypeName = "Date")] public DateTime? BirthDay { get; private set; }

        [MaxLength(150)]
        [EmailAddress]
        [Required]
        public string Email { get; }

        [MaxLength(50)] [Required] public string MembershipNo { get; }

        public PersonName Name { get; private set; }

        [Phone] [MaxLength(50)] public string Phone { get; }

        #endregion Properties

        #region Methods

        public void Update(string avatar, DateTime? birthday, string userId)
        {
            Avatar = avatar;
            BirthDay = birthday;
            SetUpdatedBy(userId);
        }

        public void UpdateName(PersonName name, string userId)
        {
            Name = name;
            SetUpdatedBy(userId);
        }

        #endregion Methods
    }
}