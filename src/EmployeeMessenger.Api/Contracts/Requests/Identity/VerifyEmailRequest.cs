﻿namespace EmployeeMessenger.Api.Contracts.Requests.Identity
{
    public class VerifyEmailRequest
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
