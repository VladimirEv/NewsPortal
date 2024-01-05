namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        Task<BaseResponse> CreateRole(RoleModel roleModel);
        Task<BaseResponse> UpdateById(Guid roleId, RoleModel roleModel);
        Task<BaseResponse> DeleteById(Guid roleId);
        Task<BaseResponse> GetAll();
        Task<BaseResponse> GetById(Guid roleId);
        Task<Role?> GetDefaultRole();
        Task<Role?> GetRoleWithMaxWeightByUser(Guid userId);
    }
}
