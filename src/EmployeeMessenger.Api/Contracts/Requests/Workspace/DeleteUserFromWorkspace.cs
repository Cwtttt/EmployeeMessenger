namespace EmployeeMessenger.Api.Contracts.Requests.Workspace
{
    public class DeleteUserFromWorkspace
    {
        public int WorkspaceId { get; set; }
        public string UserId { get; set; }
    }
}
