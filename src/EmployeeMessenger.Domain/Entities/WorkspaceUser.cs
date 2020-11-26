using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Domain.Entities
{
    public class WorkspaceUser
    {
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public WorkspaceRole Role { get; set; }
    }
}
