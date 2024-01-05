namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        Task<string> CreateToken(User user, string role);
        Task<BaseResponse> RefreshTokens(TokensModel tokensModel);
        Task UpdateRefreshTokenByUser(Guid userId, string refreshToken);
    }
}
