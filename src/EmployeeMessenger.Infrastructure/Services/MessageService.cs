using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.EntityFramework;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeMessenger.Infrastructure.Services
{
    public class MessageService : BaseService, IMessageService
    {
        public MessageService(DataContext context) : base(context) { }

        public void AddMessage(NewMessage messageRequest)
        {
            Message msg = new Message()
            {
                UserId = messageRequest.UserId,
                FirstName = messageRequest.FirstName,
                LastName = messageRequest.LastName,
                ChannelId = messageRequest.ChannelId,
                Text = messageRequest.Text,
                SendDate = DateTime.Now
            };

            _context.Messages.Add(msg);
            _context.SaveChanges();
        }

        public List<Message> GetAllChannelMessages(int channelId)
        {
            return _context.Messages
                .Where(m => m.ChannelId == channelId)
                .OrderBy(m => m.SendDate)
                .ToList();
        }
    }
}
