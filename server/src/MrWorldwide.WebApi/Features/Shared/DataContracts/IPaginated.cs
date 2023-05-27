namespace MrWorldwide.WebApi.Features.Shared.DataContracts;

public interface IPaginated
{
    public int Page { get; set; }

    public int PageSize { get; set; }
}