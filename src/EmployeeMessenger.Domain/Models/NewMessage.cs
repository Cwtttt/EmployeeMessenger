using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMessenger.Domain.Models
{
    public class NewMessage
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ChannelId { get; set; }
        public string Text { get; set; }
    }
}
