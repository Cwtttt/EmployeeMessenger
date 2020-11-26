using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.Ioc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeMessenger.Infrastructure.Services.Interfaces
{
    public interface IWorkspaceService : IScopedService
    {
        bool CreateWorkspace(string Name, string userId);
        List<Workspace> GetAllUserWorkspaces(string userId);
        bool AddMemberToWorkspace(string UserEmail, int workspaceId, int roleId);
        List<Channel> GetWorkspaceChannels(int workspaceId);
        List<WorkspaceUser> GetWorkspaceUsers(int workspaceChannelsRequest);
        Task<Result> DeleteUserFromWorkspaceAsync(int workspaceId, string userId);
    }
}
