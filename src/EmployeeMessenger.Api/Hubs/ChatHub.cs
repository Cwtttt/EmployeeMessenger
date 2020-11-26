using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.EntityFramework;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DataContext _context;
        private readonly IMessageService _messageService;
        public ChatHub(
            DataContext context,
            IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }
        public async Task SendMessage(NewMessage messageRequest)
        {
            _messageService.AddMessage(messageRequest);
            List<Message> msgs = _messageService.GetAllChannelMessages(messageRequest.ChannelId);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            JsonDocument jsonResult = JsonDocument.Parse(JsonSerializer.Serialize(msgs, options));
            await Clients.All.SendAsync("ReceiveMessage", jsonResult);
        }
    }
}
