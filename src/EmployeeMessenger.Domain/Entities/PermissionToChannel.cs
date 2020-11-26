using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMessenger.Domain.Entities
{
    public class PermissionToChannel
    {
        public int PermissionToChannelId { get; set; }
        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
        public bool Active { get; set; }

        public int WorkspaceId { get; set; }
        [ForeignKey(nameof(WorkspaceId))]
        public Workspace Workspace { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}