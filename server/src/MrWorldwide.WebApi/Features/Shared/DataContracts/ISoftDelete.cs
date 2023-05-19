namespace MrWorldwide.WebApi.Features.Shared.DataContracts;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }

    public DateTime? Deleted { get; set; }
}