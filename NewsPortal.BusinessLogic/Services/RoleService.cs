namespace NewsPortal.BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IDistributedCache _cache;
        private readonly NewsPortalDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IResponseFactory _responseFactory;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(
            IResponseFactory responseFactory,
            IMapper mapper,
            NewsPortalDbContext dbContext,
            RoleManager<Role> roleManager,
            IDistributedCache cache)
        {
            _responseFactory = responseFactory;
            _mapper = mapper;
            _dbContext = dbContext;
            _roleManager = roleManager;
            _cache = cache;
        }

        public async Task<BaseResponse> CreateRole(RoleModel roleModel)
        {
            var role = new Role
            {
                Name = roleModel.Name,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return _responseFactory.BadResponse(result, RoleServiceConstants.FailedToCreateNewRole);
            }

            var removeFromCacheTask = _cache.RemoveAsync(CacheConstants.RolesRecord);
            var mappedRoleModel = _mapper.Map<RoleModel>(role);

            await removeFromCacheTask;

            return _responseFactory.SuccessResponse(mappedRoleModel);
        }

        public async Task<BaseResponse> UpdateById(Guid roleId, RoleModel roleModel)
        {
            var role = await GetRoleById(roleId);
            if (role == null)
            {
                return _responseFactory.NotFoundResponse(role, nameof(roleId), roleId.ToString());
            }

            role.Name = roleModel.Name;
            role.UpdatedDate = DateTime.UtcNow;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return _responseFactory.BadResponse(result, RoleServiceConstants.FailedToUpdateRole);
            }

            var removeFromCacheTask = _cache.RemoveAsync(CacheConstants.RolesRecord);
            var mappedRoleModel = _mapper.Map<RoleModel>(role);

            await removeFromCacheTask;

            return _responseFactory.SuccessResponse(mappedRoleModel);
        }

        public async Task<BaseResponse> DeleteById(Guid roleId)
        {
            var role = await GetRoleById(roleId);
            if (role == null)
            {
                return _responseFactory.NotFoundResponse(role, nameof(roleId), roleId.ToString());
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return _responseFactory.BadResponse(result, RoleServiceConstants.FailedToDeleteRole);
            }

            await _cache.RemoveAsync(CacheConstants.RolesRecord);

            return _responseFactory.SuccessResponse(result);
        }

        public async Task<BaseResponse> GetAll()
        {
            var roleModels = _mapper.Map<IReadOnlyCollection<Role>, IReadOnlyCollection<RoleModel>>(await GetAllRoles());

            return _responseFactory.SuccessResponse(roleModels);
        }

        public async Task<BaseResponse> GetById(Guid roleId)
        {
            var role = await GetRoleById(roleId);
            if (role == null)
            {
                return _responseFactory.NotFoundResponse(role, nameof(roleId), roleId.ToString());
            }

            var roleModel = _mapper.Map<RoleModel>(role);

            return _responseFactory.SuccessResponse(roleModel);
        }

        public async Task<Role?> GetDefaultRole()
        {
            var allRoles = await GetAllRoles();
            var defaultRole = allRoles.SingleOrDefault(r => r.IsDefault);

            return defaultRole;
        }

        public async Task<Role> GetRoleWithMaxWeightByUser(Guid userId)
        {
            var getRoleIdsTask = _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync();
            var getAllRolesTask = GetAllRoles();

            var roleIds = await getRoleIdsTask;
            var allRoles = await getAllRolesTask;

            var roleByMaxWeight = allRoles.Where(r => roleIds.Contains(r.Id)).MaxBy(x => x.RoleWeight);
            if (roleByMaxWeight == null)
            {
                throw new Exception(RoleServiceConstants.RoleNotFound);
            }

            return roleByMaxWeight;
        }


        private async Task<IReadOnlyCollection<Role>> GetAllRoles()
        {
            var rolesFromCache = await _cache.GetRecordAsync<IReadOnlyCollection<Role>>(CacheConstants.RolesRecord);
            if (rolesFromCache != null && rolesFromCache.Count > 0)
            {
                return rolesFromCache;
            }

            var rolesFromDatabase = await _roleManager.Roles.ToListAsync();
            await _cache.SetRecordAsync(CacheConstants.RolesRecord, rolesFromDatabase);

            return rolesFromDatabase;
        }

        private async Task<Role?> GetRoleById(Guid id)
        {
            var allRoles = await GetAllRoles();

            return allRoles.FirstOrDefault(r => r.Id == id);
        }
    }
}
