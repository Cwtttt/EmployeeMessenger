using EmployeeMessenger.Api.Contracts.Requests.Channel;
using EmployeeMessenger.Api.Controllers.V1.Abstract;
using EmployeeMessenger.Api.Extensions;
using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeMessenger.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChannelController : ApiControllerBase
    {
        private readonly ApplicationUser _user;
        private readonly IUserService _userService;
        private readonly IChannelService _channelService;
        private readonly IMessageService _messageService;
        public ChannelController(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IChannelService channelService,
            IMessageService messageService)
        {
            _userService = userService;
            _user = _userService.GetApplicationUser(httpContextAccessor.HttpContext.GetUserId());
            _channelService = channelService;
            _messageService = messageService;
        }

        [HttpPost]
        public IActionResult CreateChannel([FromBody] CreateChannelRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            if (_userService.CheckIfUserIsAdminOrOwner(_user.Id, request.WorkspaceId))
            {
                var result = _channelService.CreateChannel(request.Name, request.WorkspaceId, request.ChannelType);
                if (result.Success)
                {
                    return StatusCode(201, new { Meassage = "Channel added successful" });
                }
                return BadRequest(new { Error = result.Errors });
            }

            return StatusCode(403, new { Error = "User doesn't have permission to adding channels" });
        }

        [HttpGet("{channelId}/messages")]
        public IActionResult GetAllChannelMessages(int channelId)
        {
            var msgs = _messageService.GetAllChannelMessages(channelId);

            return Ok(msgs);
        }
    }
}
