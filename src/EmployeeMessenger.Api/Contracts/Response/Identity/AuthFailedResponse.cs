﻿using System.Collections.Generic;

namespace EmployeeMessenger.Api.Contracts.Response.Identity
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
