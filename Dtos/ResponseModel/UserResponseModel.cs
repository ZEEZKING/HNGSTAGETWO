using HNGSTAGETWO.Models;

namespace HNGSTAGETWO.Dtos.ResponseModel
{
    public class UserResponseModel : BaseResponse
    {
        public UserDto? Data { get; set; }
        public string? AccessToken { get; set; }

    }
}
