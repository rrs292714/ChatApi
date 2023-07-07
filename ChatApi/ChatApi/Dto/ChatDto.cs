using Microsoft.EntityFrameworkCore;

namespace ChatApi.Dto
{
    [Keyless]
    public class ChatDto
    {
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? Message { get; set; }
    }
}
