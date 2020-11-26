using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Infrastructure.Ioc;

namespace EmployeeMessenger.Infrastructure.Services.Interfaces
{
    public interface IUserService : IScopedService
    {
        ApplicationUser GetApplicationUser(string userId);

        bool CheckIfUserIsAdminOrOwner(string UserId, int workspaceId);

        string GetIdUserByEmail(string email);

        WorkspaceRole GetUserWorkspaceRole(string UserId, int workspaceId);
    }
}
