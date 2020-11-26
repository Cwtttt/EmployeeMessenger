using EmployeeMessenger.Infrastructure.EntityFramework;

namespace EmployeeMessenger.Infrastructure.Services
{
    public class BaseService
    {
        protected readonly DataContext _context;
        public BaseService(DataContext context)
        {
            _context = context;
        }
    }
}
