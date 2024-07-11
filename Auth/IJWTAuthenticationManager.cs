using HNGSTAGETWO.Dtos;

namespace HNGSTAGETWO.Auth
{
    public interface IJWTAuthenticationManager
    {
        string GenerateToken(string key, string issuer, UserDto user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
