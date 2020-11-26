namespace EmployeeMessenger.Api.Contracts.Requests.Message
{
    public class MessageRequest
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ChannelId { get; set; }
        public string Text { get; set; }
    }
}
