using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YabaTalk.Application.Dtos.Auth
{
    public class UpdateUserReqDto
    {
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? PhoneE164 { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        public string? About { get; set; }
    }
}
