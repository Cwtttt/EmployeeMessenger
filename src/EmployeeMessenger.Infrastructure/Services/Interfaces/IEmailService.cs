using EmployeeMessenger.Domain.Models;
using EmployeeMessenger.Infrastructure.Ioc;
using System.Threading.Tasks;

namespace EmployeeMessenger.Infrastructure.Services.Interfaces
{
    public interface IEmailService : IScopedService
    {
        Task SendEmailAsync(Mail mailRequest);
    }
}
