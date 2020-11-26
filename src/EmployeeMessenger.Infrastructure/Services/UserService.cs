using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Enums;
using EmployeeMessenger.Infrastructure.EntityFramework;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EmployeeMessenger.Infrastructure.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager, DataContext context)
            : base(context)
        {
            _userManager = userManager;
        }

        public ApplicationUser GetApplicationUser(string userId)
        {
            try
            {
                return _userManager.FindByIdAsync(userId).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool CheckIfUserIsAdminOrOwner(string UserId, int workspaceId)
        {
            try
            {
                WorkspaceUser workspaceUser = _context.WorkspaceUsers
                    .FirstOrDefault(wu => wu.UserId == UserId && wu.WorkspaceId == workspaceId);

                if (workspaceUser == null)
                {
                    return false;
                }

                if (workspaceUser.RoleId == (int)WorkspaceRoleEnum.Admin ||
                    workspaceUser.RoleId == (int)WorkspaceRoleEnum.Owner)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public string GetIdUserByEmail(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            return user is null ? null : user.Id;
        }

        public WorkspaceRole GetUserWorkspaceRole(string UserId, int workspaceId)
        {
            try
            {
                return _context.WorkspaceUsers
                    .Include(wu => wu.Role)
                    .FirstOrDefault(wu => wu.UserId == UserId && wu.WorkspaceId == workspaceId).Role;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
