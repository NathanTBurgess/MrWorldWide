namespace MrWorldwide.WebApi.Features.Shared.DataContracts;

public interface IUnique<T> where T : IEquatable<T>
{
    public T Id { get; set; }
}