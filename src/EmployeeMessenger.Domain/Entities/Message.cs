using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMessenger.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ChannelId { get; set; }
        public string Text { get; set; }
        public DateTime SendDate { get; set; }
    }
}
