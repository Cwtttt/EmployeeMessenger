using System.ComponentModel.DataAnnotations;

namespace EmployeeMessenger.Api.Contracts.Requests.Channel
{
    public class CreateChannelRequest
    {
        [Required]
        public int WorkspaceId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        public int ChannelType { get; set; }
    }
}
