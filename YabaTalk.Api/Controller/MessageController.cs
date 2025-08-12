using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using YabaTalk.Api.Hubs;
using YabaTalk.Application.Dtos.Message;
using YabaTalk.Application.Interfaces.Service;

namespace YabaTalk.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService _ms;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IBus _bus;


        public MessageController(IMessageService ms, IHubContext<ChatHub> hubContext, IBus bus)
        {
            _hubContext = hubContext;
            _ms = ms;
            _bus = bus;
        }


        [HttpPost("messages/send")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto req, CancellationToken ct)
        {
            var senderId = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            // 1) Kaydet (chat oluşturma + participant ekleme + mesaj ekleme)
            var result = await _ms.CreateMessage(req, ct);
            if (!result.Success || result.Data is null)
                return BadRequest(result.Message);

            var m = result.Data;

            // (opsiyonel) istemiyorsan kapat: ChatUpdated yayını
            // var uids = await _ms.GetChatParticipants(m.ChatId, ct);
            // foreach (var uid in uids) await _hubContext.Clients.Group($"user:{uid}").SendAsync("ChatUpdated", new { ChatId = m.ChatId }, ct);

            // (opsiyonel) kuyruk için sadece "event" gönder (persist değil):
            await _bus.Publish(new MessageCreatedEvent(m.Id, m.ChatId, m.SenderUserId, m.Body, m.SentAt), ct);

            return Ok(result);
        }

        [HttpGet("chat")]
        [Authorize]
        public async Task<IActionResult> GetChatById([FromQuery] string id, CancellationToken ct)
        {
            var response = await _ms.GetChatById(id,ct);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("all-chat")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChats(CancellationToken ct)
        {
            var response = await _ms.GetChats(ct);
            return StatusCode(response.StatusCode, response);
        }
    }
}
