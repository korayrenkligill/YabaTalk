using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YabaTalk.Domain.Enums;

namespace YabaTalk.Application.Dtos.Chat
{
    public class CreateChatDto
    {
        public ChatType Type { get; set; } = ChatType.Direct;
        public string? Title { get; set; }
        public string? CreatedByUserId { get; set; }
    }
}
