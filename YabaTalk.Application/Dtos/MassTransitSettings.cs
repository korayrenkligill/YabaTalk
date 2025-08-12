using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YabaTalk.Application.Dtos
{
    public class MassTransitSettings
    {
        public string Host { get; set; } = "localhost"; // veya "rabbitmq"
        public string VirtualHost { get; set; } = "/";   // varsayılan vhost
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
    }
}
