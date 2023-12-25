namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BaseController
    {
        private readonly IJwtService _jwtService;

        public TokenController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshToken(TokensModel tokensModel)
        {
            return GetResponse(await _jwtService.RefreshTokens(tokensModel));
        }
    }
}
