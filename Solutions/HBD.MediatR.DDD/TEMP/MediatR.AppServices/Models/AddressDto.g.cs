namespace MediatR.Domains.Features.Profiles.Entities
{
    public partial class AddressDto
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Line { get; set; }
        public string Postal { get; set; }
        public string State { get; set; }
    }
}