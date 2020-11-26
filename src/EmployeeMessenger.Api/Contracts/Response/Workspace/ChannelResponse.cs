namespace EmployeeMessenger.Api.Contracts.Response.Workspace
{
    public class ChannelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorkspaceId { get; set; }
        public int ChannelTypeId { get; set; }
    }
}
