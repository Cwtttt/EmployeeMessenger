namespace EmployeeMessenger.Api.Contracts.Response.Workspace
{
    public class WorkspaceUsersResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int WorkspaceId { get; set; }
        public int RoleId { get; set; }
    }
}
