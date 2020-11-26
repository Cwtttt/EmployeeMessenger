using Microsoft.AspNetCore.Mvc;

namespace EmployeeMessenger.Api.Controllers.V1.Abstract
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
    }
}
