namespace MediatR.Domains.Features.Profiles.Entities
{
    public partial class CompanyDto
    {
        public string? ABN { get; set; }
        public string? ARBN { get; set; }
        public string? CAN { get; set; }
        public string Name { get; set; }
        public string UEN { get; set; }
    }
}