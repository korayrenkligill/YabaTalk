using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YabaTalk.Application.interfaces
{
    public interface IActiveUserAccessor
    {
        bool IsAuthenticated { get; }
        string? UserId { get; }
        string? Phone { get; }
    }
}
