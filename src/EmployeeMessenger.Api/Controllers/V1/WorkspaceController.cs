using AutoMapper;
using EmployeeMessenger.Api.Contracts.Requests.Workspace;
using EmployeeMessenger.Api.Contracts.Response.Workspace;
using EmployeeMessenger.Api.Controllers.V1.Abstract;
using EmployeeMessenger.Api.Extensions;
using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Enums;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkspaceController : ApiControllerBase
    {
        private ApplicationUser _user;
        private readonly IUserService _userService;
        private readonly IWorkspaceService _workspaceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public WorkspaceController(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IWorkspaceService workspaceService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _workspaceService = workspaceService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _user = _userService.GetApplicationUser(_httpContextAccessor.HttpContext.GetUserId());
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult CreateWorkspace([FromBody] CreateWorkspaceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            bool result = _workspaceService.CreateWorkspace(request.Name, _user.Id);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = "Server error" });
            }
            return Ok();
        }

        [HttpPost("{workspaceId}/users")]
        public IActionResult AddUserToWorkspace([FromBody] AddUserToWorkspaceRequest request, int workspaceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            if (_userManager.FindByEmailAsync(request.UserEmail.DeleteWhiteSpaces()).Result == null)
            {
                return BadRequest(new { Error = "User with the e-mail doesn't exist" });
            }

            if (_userService.CheckIfUserIsAdminOrOwner(_user.Id, workspaceId))
            {
                var userId = _userService.GetIdUserByEmail(request.UserEmail);
                if (userId == null)
                {
                    return BadRequest(new { Error = "User doesn't exist" });
                }
                var result = _workspaceService.AddMemberToWorkspace(userId, workspaceId, (int)WorkspaceRoleEnum.User);
                if (result)
                {
                    return Ok(new { Meassage = "Member added successful" });
                }
                return BadRequest(new { Error = "Member added fail" });
            }

            return BadRequest(new { Error = "User doeasn't have permission to adding members" });
        }

        [HttpGet("{workspaceId}/channels")]
        public IActionResult GetWorkspaceChannels(int workspaceId)
        {
            List<Channel> workspaceChannels = _workspaceService.GetWorkspaceChannels(workspaceId);

            if (workspaceChannels == null || workspaceChannels.Count() == 0)
            {
                return NotFound(new { Message = "Workspace doesn't have any channels" });
            }

            List<ChannelResponse> response = _mapper.Map<List<Channel>, List<ChannelResponse>>(workspaceChannels);
            return Ok(response);
        }

        [HttpGet("{workspaceId}/users")]
        public IActionResult GetWorkspaceUsers(int workspaceId)
        {
            List<WorkspaceUser> workspaceUsers = _workspaceService.GetWorkspaceUsers(workspaceId);

            if (workspaceUsers == null || workspaceUsers.Count() == 0)
            {
                return NotFound(new { Message = "Users not found" });
            }
            List<WorkspaceUsersResponse> response = _mapper.Map<List<WorkspaceUser>, List<WorkspaceUsersResponse>>(workspaceUsers);
            return Ok(response);
        }

        [HttpDelete("{workspaceId}/users/{userId}")]
        public async Task<IActionResult> DeleteUserFromWorkspace(int workspaceId, string userId)
        {
            var result = await _workspaceService.DeleteUserFromWorkspaceAsync(workspaceId, userId);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User has been deleted successful." });
        }
    }
}
