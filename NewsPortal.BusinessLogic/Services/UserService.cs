using NewsPortal.BusinessLogic.Constants;

namespace NewsPortal.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper? _mapper;
        private readonly IResponseFactory _responseFactory;
        private readonly IRoleService _roleService;
        private readonly UserManager<User>? _userManager;

        public UserService(
            IMapper? mapper, 
            IResponseFactory responseFactory, 
            IRoleService roleService, 
            UserManager<User>? userManager)
        {
            _mapper = mapper;
            _responseFactory = responseFactory;
            _roleService = roleService;
            _userManager = userManager;
        }

        public async Task<BaseResponse> Register(RegisterModel registerModel)
        {
            var user = new User
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var defaultRole = await _roleService.GetDefaultRole();
            if (defaultRole == null)
            {
                return _responseFactory.BadResponse(defaultRole, UserServiceConstants.FailedWhenGetDefaultRole);
            }

            var createResult = await _userManager.CreateAsync(user, registerModel.Password);
            if (!createResult.Succeeded)
            {
                return _responseFactory.BadResponse(createResult, UserServiceConstants.FailedWhenCreateUser);
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, defaultRole.NormalizedName!);
            if (!addRoleResult.Succeeded)
            {
                return _responseFactory.BadResponse(addRoleResult, UserServiceConstants.FailedWhenAddRoleToUser);
            }

            var userModel = _mapper?.Map<UserModel>(user);

            return _responseFactory.SuccessResponse(userModel);
        }

        public async Task<BaseResponse> UpdateUserById(Guid userId, UserModel model)
        {
            var user = await GetUserById(userId);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(user, nameof(userId), userId.ToString());
            }

            user.UserName = model.UserName;
            user.UpdatedDate = DateTime.UtcNow;
            var result = await _userManager?.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return _responseFactory.BadResponse(result, UserServiceConstants.FailedWhenUpdateUser);
            }

            var userModel = _mapper?.Map<UserModel>(user);

            return _responseFactory.SuccessResponse(userModel);
        }

        public async Task<BaseResponse> ChangePasswordById(Guid userId, ChangePasswordModel model)
        {
            var user = await GetUserById(userId);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(user, nameof(userId), userId.ToString());
            }

            user.UpdatedDate = DateTime.UtcNow;
            var result = await _userManager?.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return _responseFactory.BadResponse(result, UserServiceConstants.FailedWhenChangePassword);
            }

            return _responseFactory.SuccessResponse(result);
        }

        public async Task<BaseResponse> ChangeEmailById(Guid userId, string newEmail, string token)
        {
            var user = await GetUserById(userId);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(user, nameof(userId), userId.ToString());
            }

            var result = await _userManager?.ChangeEmailAsync(user, newEmail, token);
            if (!result.Succeeded)
            {
                return _responseFactory.BadResponse(result, UserServiceConstants.FailedWhenChangeEmail);
            }

            return _responseFactory.SuccessResponse(result);
        }

        public async Task<BaseResponse> GenerateChangeEmailTokenById(Guid userId, string newEmail)
        {
            var user = await GetUserById(userId);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(user, nameof(userId), userId.ToString());
            }

            var result = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            if (string.IsNullOrEmpty(result))
            {
                return _responseFactory.BadResponse(result, UserServiceConstants.FailedWhenGenerateChangeEmailToken);
            }

            return _responseFactory.SuccessResponse(result);
        }

        public async Task<BaseResponse> BlockUserById(Guid userId, int hours = 24)
        {
            var user = await GetUserById(userId);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(user, nameof(userId), userId.ToString());
            }

            user.UpdatedDate = DateTime.UtcNow;
            var result = await _userManager?.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddHours(hours));
            if (!result.Succeeded)
            {
                return _responseFactory.BadResponse(result, UserServiceConstants.FailedWhenSetLockoutEndDate);
            }

            return _responseFactory.SuccessResponse(result);
        }

        public async Task<BaseResponse> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var userModels = _mapper?.Map<IReadOnlyCollection<User>, IReadOnlyCollection<UserModel>>(users);

            return _responseFactory.SuccessResponse(userModels);
        }

        public async Task<BaseResponse> GetById(Guid userId)
        {
            var user = await GetUserById(userId);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(user, nameof(userId), userId.ToString());
            }

            var userModel = _mapper?.Map<UserModel>(user);

            return _responseFactory.SuccessResponse(userModel);
        }

        public async Task<BaseResponse> GetByEmail(string email)
        {
            var user = await _userManager?.FindByEmailAsync(email);
            if (user == null)
            {
                return _responseFactory.NotFoundResponse(user, nameof(email), email);
            }

            var userModel = _mapper?.Map<UserModel>(user);

            return _responseFactory.SuccessResponse(userModel);
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            return await _userManager?.FindByIdAsync(userId.ToString());
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userManager?.FindByEmailAsync(email);
        }
    }
}
