using HNGSTAGETWO.Dtos.RequestModel;
using HNGSTAGETWO.Dtos.ResponseModel;
using HNGSTAGETWO.Models;

namespace HNGSTAGETWO.Interfaces.IServices
{
    public interface IUserServices
    {
        Task<UserResponseModel> RegisterUser(CreateUserRequestModel model);
        Task<UserResponseModel> LoginUser(LoginRequestModel model);
        Task<UserResponseModel> GetById(string id);
        string GenerateJwtToken(User user);
    }
}
