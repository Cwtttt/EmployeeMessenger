using System.ComponentModel.DataAnnotations;

namespace EmployeeMessenger.Api.Contracts.Requests.Workspace
{
    public class AddUserToWorkspaceRequest
    {
        [EmailAddress]
        public string UserEmail { get; set; }

    }
}
