using System.ComponentModel.DataAnnotations;

namespace EmployeeMessenger.Api.Contracts.Requests.Identity
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
