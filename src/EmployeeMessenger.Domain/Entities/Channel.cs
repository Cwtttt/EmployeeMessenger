using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Domain.Entities
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
        public int ChannelTypeId { get; set; }

        [ForeignKey(nameof(ChannelTypeId))]
        public ChannelType ChannelType { get; set; }
        public ICollection<PermissionToChannel> ParmissionsToChannels { get; set; }
    }
}
