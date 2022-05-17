using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TEMP.Domains.ValueObjects
{
    [Owned]
    public class Address
    {
        #region Constructors

        public Address(string line, string state, string city, string country, string postal)
        {
            Line = line;
            State = state;
            City = city;
            Country = country;
            Postal = postal;
        }

        internal Address()
        {
        }

        #endregion Constructors

        #region Properties

        [MaxLength(50)] public string City { get; }

        [MaxLength(50)] public string Country { get; }

        [MaxLength(50)] public string Line { get; }

        [MaxLength(50)] public string Postal { get; }

        [MaxLength(50)] public string State { get; }

        #endregion Properties
    }
}