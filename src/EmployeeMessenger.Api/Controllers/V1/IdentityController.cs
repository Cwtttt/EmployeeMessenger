﻿using AutoMapper;
using EmployeeMessenger.Api.Contracts.Requests.Identity;
using EmployeeMessenger.Api.Contracts.Response.Identity;
using EmployeeMessenger.Api.Controllers.V1.Abstract;
using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Api.Controllers.V1
{
    public class IdentityController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public IdentityController(
            IIdentityService identityService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _identityService = identityService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            };

            NewUser newUser = _mapper.Map<NewUser>(request);
            var authResponse = await _identityService.RegisterAsync(newUser);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            await _identityService.GenerateEmailConfirmation(request.Email);

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserId = authResponse.UserId
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                FirstName = authResponse.FirstName,
                LastName = authResponse.LastName,
                UserId = authResponse.UserId
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpGet("verifyemail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailRequest verifyEmailRequest)
        {
            var user = await _userManager.FindByIdAsync(verifyEmailRequest.UserId);
            if (user == null)
            {
                return BadRequest(new { Error = "User doesn't exist :( " });
            }

            var result = await _userManager.ConfirmEmailAsync(user, verifyEmailRequest.Code);
            if (!result.Succeeded)
            {
                return BadRequest(new { Error = "Error during confirmation user email." });
            }

            return Ok(new { Message = "User email confirmated successfull" });
        }
    }
}
