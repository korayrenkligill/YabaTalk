using MassTransit;
using MassTransit.Initializers;
using System.Diagnostics;
using YabaTalk.Application.Dtos;
using YabaTalk.Application.Dtos.Chat;
using YabaTalk.Application.Dtos.ChatParticipant;
using YabaTalk.Application.Dtos.Message;
using YabaTalk.Application.Interfaces;
using YabaTalk.Application.Interfaces.Repository;
using YabaTalk.Application.Interfaces.Service;
using YabaTalk.Domain.Entity;
using YabaTalk.Domain.Enums;

namespace YabaTalk.Infrastructure.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IBus _bus;
        private readonly IMessageRepository _mr;
        private readonly IChatParticipantRepository _cpr;
        private readonly IChatRepository _cr;
        private readonly IUserRepository _ur;
        private readonly IActiveUserAccessor _activeUserAccessor;

        public MessageService(IBus bus, IMessageRepository mr, IChatParticipantRepository cpr, IChatRepository cr, IUserRepository ur, IActiveUserAccessor activeUserAccessor)
        {
            _bus = bus;
            _mr = mr;
            _cpr = cpr;
            _cr = cr;
            _ur = ur;
            _activeUserAccessor = activeUserAccessor;
        }

        public async Task<ResponseDto<MessageResponseDto>> CreateMessage(CreateMessageDto dto, CancellationToken cancellationToken = default)
        {
            var senderUserId = _activeUserAccessor.UserId!;
            var senderUser = await _ur.GetByIdAsync(senderUserId);
            if (senderUser == null)
                return ResponseDto<MessageResponseDto>.Fail("Mesaj gönderen kullanıcı sistemde kayıtlı değil.");

            var receiverUser = await _ur.GetByPhoneAsync(dto.ReceiverPhone);
            if (receiverUser == null)
                return ResponseDto<MessageResponseDto>.Fail("Mesaj gönderilen kullanıcı sistemde kayıtlı değil.");

            var betweenChat = await _cr.FindChatBetweenUsers(senderUserId, receiverUser.Id);
            string chatId;
            if (betweenChat == null)
            {
                //Daha önce konuşmamışlar yeni chat oluştur
                var newChatEntity = new Chat
                {
                    Type = ChatType.Direct,
                    CreatedByUserId = senderUserId,
                };
                var newChat = await _cr.CreateChatAsync(newChatEntity);
                chatId = newChat.Id;

                //Yeni chat'e kullanıcıları ekle
                var senderParticipant = new ChatParticipant
                {
                    ChatId = newChat.Id,
                    UserId = senderUserId,
                    IsAdmin = true,
                };

                await _cpr.CreateChatParticipantAsync(senderParticipant);

                var receiverParticipant = new ChatParticipant
                {
                    ChatId = newChat.Id,
                    UserId = receiverUser.Id,
                    IsAdmin = false,
                };

                await _cpr.CreateChatParticipantAsync(receiverParticipant);
            }
            else
            {
                chatId = betweenChat.Id;
            }

            //Yeni chat'e mesajı ekle
            var message = new YabaTalk.Domain.Entity.Message
            {
                ChatId = chatId,
                SenderUserId = senderUserId,
                Body = dto.Body,
                Status = MessageStatus.Sent,
            };
            await _mr.CreateMessageAsync(message);
            Debug.WriteLine($"Mesaj alındı kaydedildi tek tık: {dto.Body}");

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:message-service"));
            await endpoint.Send(dto, cancellationToken);

            var messageResponse = new MessageResponseDto
            {
                Id = message.Id,
                ChatId = message.ChatId,
                SenderUserId = message.SenderUserId,
                Body = message.Body,
                SentAt = message.SentAt,
                Status = message.Status
            };

            return ResponseDto<MessageResponseDto>.Ok(messageResponse, "Mesaj oluşturuldu");
        }

        public async Task<ResponseDto<List<string>>> GetChatParticipants(string chatId, CancellationToken cancellationToken = default)
        {
            var participants = await _cpr.GetChatParticipantsByChatIdAsync(chatId, cancellationToken);
            var participantIds = participants.Select(cp => cp.UserId).Distinct().ToList();

            return ResponseDto<List<string>>.Ok(participantIds, "Katılımcılar başarıyla getirildi.");
        }

        public async Task<ResponseDto<ChatResponseDto>> GetChatById(string id, CancellationToken ct)
        {
            var chat = await _cr.GetChatByIdAsync(id, ct);
            if (chat == null)
                return ResponseDto<ChatResponseDto>.Fail("Chat bulunamadı",404);
            var resp = new ChatResponseDto
            {
                Id = chat.Id,
                Type = chat.Type,
                CreatedAt = chat.CreatedAt,
                Messages = chat.Messages.Select(c => new MessageResponseDto
                {
                    Id = c.Id,
                    ChatId = c.Id,
                    SenderUserId = c.SenderUserId,
                    Body = c.Body,
                    SentAt = c.SentAt,
                    Status = c.Status
                }).ToList(),
            };

            return ResponseDto<ChatResponseDto>.Ok(resp,"Chat getirildi");
        }
        public async Task<ResponseDto<List<ChatResponseDto>>> GetChats( CancellationToken ct)
        {
            var chats = await _cr.GetChatsAsync(ct);
           
            var resp = chats.Select(chat => new ChatResponseDto
            {
                Id = chat.Id,
                Type = chat.Type,
                CreatedAt = chat.CreatedAt,
                Messages = chat.Messages.Select(c => new MessageResponseDto
                {
                    Id = c.Id,
                    ChatId = c.Id,
                    SenderUserId = c.SenderUserId,
                    Body = c.Body,
                    SentAt = c.SentAt,
                    Status = c.Status
                }).ToList(),
            }).ToList();

            return ResponseDto<List<ChatResponseDto>>.Ok(resp, "Chat getirildi");
        }
    }
}
