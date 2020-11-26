using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Enums;
using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.EntityFramework;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Infrastructure.Services
{
    public class WorkspaceService : BaseService, IWorkspaceService
    {
        public WorkspaceService(DataContext context) : base(context) { }

        public bool CreateWorkspace(string Name, string userId)
        {
            try
            {
                Workspace newWorkspace = new Workspace()
                {
                    Name = Name,
                    CreatedDate = DateTime.Now,
                    OwnerId = userId
                };

                _context.Workspaces.Add(newWorkspace);
                _context.SaveChanges();

                Channel channel = new Channel()
                {
                    Name = "General",
                    WorkspaceId = newWorkspace.WorkspaceId,
                    ChannelTypeId = (int)ChannelTypeEnum.Public
                };

                _context.Channels.Add(channel);
                _context.SaveChanges();

                WorkspaceUser workspaceUser = new WorkspaceUser()
                {
                    WorkspaceId = newWorkspace.WorkspaceId,
                    UserId = userId,
                    RoleId = (int)WorkspaceRoleEnum.Owner
                };

                _context.WorkspaceUsers.Add(workspaceUser);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<Workspace> GetAllUserWorkspaces(string userId)
        {
            try
            {
                var workspacesDbo = _context.WorkspaceUsers
                    .Include(wu => wu.Workspace)
                    .Where(wu => wu.UserId == userId)
                    .Select(wu => wu.Workspace)
                    .ToList();

                return workspacesDbo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool AddMemberToWorkspace(string userId, int workspaceId, int roleId)
        {
            try
            {
                WorkspaceUser workspaceUser = new WorkspaceUser()
                {
                    WorkspaceId = workspaceId,
                    UserId = userId,
                    RoleId = roleId
                };

                _context.WorkspaceUsers.Add(workspaceUser);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<Channel> GetWorkspaceChannels(int workspaceId)
        {
            return _context.Channels.Where(x => x.WorkspaceId == workspaceId).ToList();
        }

        public List<WorkspaceUser> GetWorkspaceUsers(int workspaceid)
        {
            var workspaceUsers = _context.WorkspaceUsers
                .Include(wu => wu.User)
                .Where(wu => wu.WorkspaceId == workspaceid).ToList();

            if (workspaceUsers == null || workspaceUsers.Count == 0)
            {
                return new List<WorkspaceUser>();
            }

            return workspaceUsers;
        }

        public async Task<Result> DeleteUserFromWorkspaceAsync(int workspaceId, string userId)
        {
            var user = _context.WorkspaceUsers.FirstOrDefault(wu => wu.UserId == userId && wu.WorkspaceId == workspaceId);

            if (user == null)
            {
                return new Result()
                {
                    Success = false,
                    Errors = new[] { "User doesn't exist." }
                };
            }

            if (user.RoleId == (int)WorkspaceRoleEnum.Owner)
            {
                return new Result()
                {
                    Success = false,
                    Errors = new[] { "You can't delete owner of workspace." }
                };
            }

            _context.WorkspaceUsers.Remove(user);
            await _context.SaveChangesAsync();

            return new Result() { Success = true };
        }
    }
}
