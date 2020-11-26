using System.ComponentModel.DataAnnotations;

namespace EmployeeMessenger.Api.Contracts.Requests.Workspace
{
    public class CreateWorkspaceRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
