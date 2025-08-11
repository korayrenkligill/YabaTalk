using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YabaTalk.Domain.Entity;

namespace YabaTalk.Application.Dtos.Auth
{
    public class UserReqDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneE164 { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        public string? About { get; set; }
    }
}
