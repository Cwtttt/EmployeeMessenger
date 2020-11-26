using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.Ioc;

namespace EmployeeMessenger.Infrastructure.Services.Interfaces
{
    public interface IChannelService : IScopedService
    {
        Result CreateChannel(string name, int workspaceId, int channelType);
    }
}
