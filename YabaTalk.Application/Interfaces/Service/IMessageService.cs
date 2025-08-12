using YabaTalk.Application.Dtos;
using YabaTalk.Application.Dtos.Chat;
using YabaTalk.Application.Dtos.Message;

namespace YabaTalk.Application.Interfaces.Service
{
    public interface IMessageService
    {
        Task<ResponseDto<MessageResponseDto>> CreateMessage(CreateMessageDto dto, CancellationToken cancellationToken = default);
        Task<ResponseDto<List<string>>> GetChatParticipants(string chatId, CancellationToken cancellationToken = default);
        Task<ResponseDto<ChatResponseDto>> GetChatById(string id, CancellationToken ct);
        Task<ResponseDto<List<ChatResponseDto>>> GetChats(CancellationToken ct);
    }
}
