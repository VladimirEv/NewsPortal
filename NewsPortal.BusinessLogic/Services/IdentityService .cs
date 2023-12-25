namespace NewsPortal.BusinessLogic.Services
{
    public class IdentityService : IIdentityService
    {
        private const bool LockoutOnFailure = false;
        private readonly IDistributedCache _cache;
        private readonly NewsPortalDbContext _dbContext;
        private readonly IJwtService _jwtService;
        private readonly IResponseFactory _responseFactory;
        private readonly IRoleService _roleService;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public IdentityService(
            IResponseFactory responseFactory,
            IDistributedCache cache,
            SignInManager<User> signInManager,
            NewsPortalDbContext dbContext,
            IUserService userService,
            IJwtService jwtService,
            IRoleService roleService)
        {
            _responseFactory = responseFactory;
            _cache = cache;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _userService = userService;
            _jwtService = jwtService;
            _roleService = roleService;
        }

        public async Task<BaseResponse> Login(LoginModel loginModel)
        {
            var user = await _userService.GetUserByEmail(loginModel.Email);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(loginModel, nameof(loginModel.Email), loginModel.Email);
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, LockoutOnFailure);
            if (!signInResult.Succeeded)
            {
                return _responseFactory.BadResponse(loginModel, IdentityServiceConstants.PasswordOrEmailAreIncorrect);
            }

            var role = await _roleService.GetRoleWithMaxWeightByUser(user.Id);
            var accessToken = await _jwtService.CreateToken(user, role.Name);
            var refreshToken = Guid.NewGuid().ToString();
            var tokensModel = new TokensModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            await _jwtService.UpdateRefreshTokenByUser(user.Id, refreshToken);
            return _responseFactory.SuccessResponse(tokensModel);
        }

        public async Task<BaseResponse> Logout(IReadOnlyCollection<Claim> claims, string accessToken, int expirationInMinutes)
        {
            var userId = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return _responseFactory.UnauthorizedResponse();
            }

            var user = await _userService.GetUserById(new Guid(userId));
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(user, nameof(userId), userId);
            }

            var userToken = await _dbContext.UserTokens.SingleOrDefaultAsync(ut => ut.UserId == user.Id);
            if (userToken == null)
            {
                return _responseFactory.NotFoundResponse(userToken, nameof(user.Id), user.Id.ToString());
            }

            await _cache.SetRecordAsync(accessToken, accessToken, TimeSpan.FromMinutes(expirationInMinutes));

            _dbContext.UserTokens.Remove(userToken);
            await _dbContext.SaveChangesAsync();

            await _signInManager.SignOutAsync();

            return _responseFactory.SuccessResponse(userId, IdentityServiceConstants.SuccessfullyLogout);
        }
    }
}
