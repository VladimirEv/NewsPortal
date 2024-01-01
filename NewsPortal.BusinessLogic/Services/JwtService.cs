namespace NewsPortal.BusinessLogic.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly NewsPortalDbContext _dbContext;
        private readonly IResponseFactory _responseFactory;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public JwtService(
            IResponseFactory responseFactory,
            NewsPortalDbContext dbContext,
            UserManager<User> userManager,
            IConfiguration config,
            IUserService userService)
        {
            _responseFactory = responseFactory;
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = config;
            _userService = userService;
        }

        public async Task<string> CreateToken(User user, string role)
        {
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            new(AuthorizationClaims.Role, role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[ConfigurationConstants.JwtSecret] ?? throw new ArgumentNullException()));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(ConvertExtensions.ConvertToInt32WithDefaultValue(_configuration[ConfigurationConstants.JwtExpirationInMinutes], DefaultValuesConstants.DefaultConvertValue)),
                SigningCredentials = credentials,
                Audience = _configuration[ConfigurationConstants.JwtValidAudience] ?? throw new ArgumentNullException(),
                Issuer = _configuration[ConfigurationConstants.JwtValidIssuer] ?? throw new ArgumentNullException()
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            if (userClaims.Count > 0)
            {
                await _userManager.RemoveClaimsAsync(user, userClaims);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            await _userManager.AddClaimsAsync(user, claims);

            return tokenHandler.WriteToken(token);
        }

        public async Task<BaseResponse> RefreshTokens(TokensModel tokensModel)
        {
            if (!ValidateToken(tokensModel.AccessToken))
            {
                return _responseFactory.BadResponse(tokensModel, JwtServiceConstants.AccessTokenIsNotValid);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokensModel.AccessToken);
            var userEmail = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return _responseFactory.BadResponse(tokensModel, JwtServiceConstants.UserEmailIsNotValid);
            }

            var role = token.Claims.FirstOrDefault(x => x.Type == AuthorizationClaims.Role)?.Value;
            if (string.IsNullOrEmpty(role))
            {
                return _responseFactory.BadResponse(tokensModel, JwtServiceConstants.RoleIdIsNotValid);
            }

            var user = await _userService.GetUserByEmail(userEmail);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(tokensModel, nameof(userEmail), userEmail);
            }

            var userRefreshToken = await _dbContext.UserTokens.SingleOrDefaultAsync(ut => ut.UserId == user.Id);
            if (userRefreshToken == null && userRefreshToken?.Value != tokensModel.RefreshToken)
            {
                return _responseFactory.BadResponse(tokensModel, JwtServiceConstants.RefreshTokenIsNotValid);
            }

            var accessToken = await CreateToken(user, role);
            var refreshToken = Guid.NewGuid().ToString();

            await UpdateRefreshTokenByUser(user, refreshToken);

            tokensModel = new TokensModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return _responseFactory.SuccessResponse(tokensModel);
        }

        public async Task UpdateRefreshTokenByUser(Guid userId, string refreshToken)
        {
            var userTokenByUser = await _dbContext.UserTokens.FirstOrDefaultAsync(ut => ut.UserId == userId);
            await UpdateRefreshToken(userTokenByUser, userId, refreshToken);
        }


        private bool ValidateToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            tokenHandler.ValidateToken(accessToken, validationParameters, out var validatedToken);
            if (validatedToken != null)
            {
                return true;
            }

            return false;
        }

        private async Task UpdateRefreshTokenByUser(User user, string refreshToken)
        {
            var userTokenByUser = await _dbContext.UserTokens.FirstOrDefaultAsync(ut => ut.UserId == user.Id);
            await UpdateRefreshToken(userTokenByUser, user.Id, refreshToken);
        }

        private async Task UpdateRefreshToken(UserToken? userTokenByUser, Guid userId, string refreshToken)
        {
            var userTokenEntity = new UserToken
            {
                LoginProvider = JwtServiceConstants.LoginProvider,
                Name = JwtServiceConstants.UserTokenEntityName,
                Value = refreshToken,
                UserId = userId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            if (userTokenByUser == null)
            {
                await AddUserToken(userTokenEntity);

                return;
            }

            userTokenByUser.Value = refreshToken;
            userTokenByUser.UpdatedDate = DateTime.UtcNow;
            await UpdateUserToken(userTokenByUser);
        }

        private async Task AddUserToken(UserToken userTokenEntity)
        {
            await _dbContext.UserTokens.AddAsync(userTokenEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateUserToken(UserToken userTokenEntity)
        {
            _dbContext.UserTokens.Update(userTokenEntity);
            await _dbContext.SaveChangesAsync();
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = _configuration[ConfigurationConstants.JwtValidAudience] ?? throw new ArgumentNullException(),
                ValidIssuer = _configuration[ConfigurationConstants.JwtValidIssuer] ?? throw new ArgumentNullException(),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[ConfigurationConstants.JwtSecret] ?? throw new ArgumentNullException())),
                RequireExpirationTime = true
            };
        }
    }
}
