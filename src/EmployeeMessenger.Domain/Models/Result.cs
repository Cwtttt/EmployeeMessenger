using System.Collections.Generic;

namespace EmployeeMessenger.Domain.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
