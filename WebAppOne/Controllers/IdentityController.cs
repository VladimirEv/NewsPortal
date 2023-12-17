namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        public readonly IConfiguration? _configuration;
        
    }
}
