namespace YabaTalk.Application.Interfaces
{
    public interface IActiveUserAccessor
    {
        bool IsAuthenticated { get; }
        string? UserId { get; }
        string? Phone { get; }
    }
}
