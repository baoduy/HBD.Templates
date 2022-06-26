namespace MediatR.AppServices.Share;

public record PageableQuery
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 100;
}