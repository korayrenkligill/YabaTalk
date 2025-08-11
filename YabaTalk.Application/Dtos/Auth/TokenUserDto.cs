using System.ComponentModel.DataAnnotations;

namespace YabaTalk.Application.Dtos.Auth
{
    public class TokenUserDto
    {
        public string Id { get; set; } = default!;
        public string PhoneE164 { get; set; } = default!;

    }
}
