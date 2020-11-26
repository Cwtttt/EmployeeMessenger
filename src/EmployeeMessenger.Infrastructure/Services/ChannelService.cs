using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.EntityFramework;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using System;
using System.Linq;

namespace EmployeeMessenger.Infrastructure.Services
{
    public class ChannelService : BaseService, IChannelService
    {
        public ChannelService(DataContext context) : base(context) { }

        public Result CreateChannel(string name, int workspaceId, int channelType)
        {
            try
            {
                if (_context.Channels.Any(x => x.WorkspaceId == workspaceId && x.Name == name))
                {
                    return new Result
                    {
                        Success = false,
                        Errors = new[] { "Channel with this name already exist" }
                    };
                }

                Channel newChannel = new Channel()
                {
                    Name = name,
                    WorkspaceId = workspaceId,
                    ChannelTypeId = channelType
                };

                _context.Channels.Add(newChannel);
                _context.SaveChanges();

                return new Result()
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Result()
                {
                    Success = true,
                    Errors = new[] { ex.ToString() }
                };
            }
        }
    }
}
