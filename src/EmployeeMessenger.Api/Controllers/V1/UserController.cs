using AutoMapper;
using EmployeeMessenger.Api.Contracts.Response.Workspace;
using EmployeeMessenger.Api.Controllers.V1.Abstract;
using EmployeeMessenger.Api.Extensions;
using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeMessenger.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ApiControllerBase
    {
        private ApplicationUser _user;
        private readonly IUserService _userService;
        private readonly IWorkspaceService _workspaceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public UserController(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IWorkspaceService workspaceService,
            IMapper mapper)
        {
            _workspaceService = workspaceService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _user = _userService.GetApplicationUser(_httpContextAccessor.HttpContext.GetUserId());
            _mapper = mapper;
        }

        [HttpGet("me/workspaces")]
        public IActionResult GetUserWorkspaces()
        {
            List<Workspace> userWorkspaces = _workspaceService.GetAllUserWorkspaces(_user.Id);

            if (userWorkspaces == null || userWorkspaces.Count() == 0)
            {
                return NotFound(new { Message = "User doesn't have permission to any workspace" });
            }
            List<UserWorkspacesResponse> response = _mapper.Map<List<UserWorkspacesResponse>>(userWorkspaces);
            return Ok(response);
        }
    }
}
