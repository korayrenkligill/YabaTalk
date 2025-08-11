namespace YabaTalk.Application.Dtos.Auth
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        public string? About { get; set; }
    }
}
