namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface IIdentityService
    {
        Task<BaseResponse> Login(LoginModel loginModel);
        Task<BaseResponse> Logout(IReadOnlyCollection<Claim> claims, string accessToken, int expirationInMinutes);
    }
}
