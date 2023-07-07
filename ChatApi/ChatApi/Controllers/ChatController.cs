using ChatApi.Dto;
using ChatApi.Interface;
using ChatApi.Models;
using ChatApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly chatappContext _context;
        private readonly ILogger<ChatController> _logger;
        private readonly MessageService _messageService;

        public ChatController(chatappContext context, ILogger<ChatController> logger, MessageService messageService)
        {
            _context = context;
            _logger=logger;
            _messageService = messageService;
        }
        [HttpPost]
        public IActionResult SendMessage(ChatDto messageDto)
        {
            // Encrypt the message content before storing it in the database
            string encryptedContent = MessageService.Encrypt(messageDto.Message);

            var message = new Chat
            {
                SenderId = messageDto.SenderId,
                ReceiverId = messageDto.ReceiverId,
                HashMessage = encryptedContent,
                SaltMessage="HELLO"

            };

            _context.Chats.Add(message);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{userId}")]
        public IActionResult GetMessages(int userId)
        {
            var messages = _context.Chats
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .OrderByDescending(m => m.MessageTime)
                .ToList();

            var decryptedMessages = messages.Select(m => new ChatDto
            {
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Message = MessageService.Decrypt(m.HashMessage),
            });

            return Ok(decryptedMessages);
        }

    }
}
