using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.Ioc;
using System.Collections.Generic;

namespace EmployeeMessenger.Infrastructure.Services.Interfaces
{
    public interface IMessageService : IScopedService
    {
        List<Message> GetAllChannelMessages(int channelId);
        void AddMessage(NewMessage messageRequest);
    }
}
