using NewsPortal.BusinessLogic.Models;
using NewsPortal.BusinessLogic.Models.RequestModels;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastucture.Common.Response.ResponseModels.Base;

namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse> Register(RegisterModel registerModel);
        Task<BaseResponse> UpdateUserById(Guid userId, UserModel model);
        Task<BaseResponse> ChangeEmailById(Guid userId, string newEmail, string token);
        Task<BaseResponse> ChangePasswordById(Guid userId, ChangePasswordModel model);
        Task<BaseResponse> GenerateChangeEmailTokenById(Guid userId, string newEmail);
        Task<BaseResponse> BlockUserById(Guid userId, int hours = 24);
        Task<BaseResponse> GetAll();
        Task<BaseResponse> GetById(Guid userId);
        Task<BaseResponse> GetByEmail(string email);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid userId);
    }
}
