using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Domain.Entities
{
    public class Workspace
    {
        public int WorkspaceId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OwnerId { get; set; }
        public ICollection<Channel> WorkspaceChannels { get; set; }
        public ICollection<WorkspaceUser> WorkspaceUsers { get; set; }
    }
}
