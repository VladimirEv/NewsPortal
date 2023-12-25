namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;

        public IdentityController(
            IConfiguration configuration,
            IIdentityService identityService)
        {
            _configuration = configuration;
            _identityService = identityService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return GetResponse(await _identityService.Login(loginModel));
        }

        [HttpPost("Logout")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            return GetResponse(await _identityService.Logout(
                User.Claims.ToList(),
                HttpContext.Request.Headers.Authorization.ToString(),
                ConvertExtensions.ConvertToInt32WithDefaultValue(_configuration[ConfigurationConstants.JwtExpirationInMinutes], DefaultValuesConstants.DefaultConvertValue)));
        }

    }
}
