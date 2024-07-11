using HNGSTAGETWO.Dtos;
using HNGSTAGETWO.Dtos.RequestModel;
using HNGSTAGETWO.Dtos.ResponseModel;
using HNGSTAGETWO.Interfaces.IRepository;
using HNGSTAGETWO.Interfaces.IServices;
using HNGSTAGETWO.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HNGSTAGETWO.Implementations.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IConfiguration _config;

        public UserServices(IUserRepository userRepository, IOrganizationRepository organizationRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
            _config = config;
        }

        /*  public string GenerateJwtToken(User user)
          {
              var tokenHandler = new JwtSecurityTokenHandler();
              var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
              var tokenDescriptor = new SecurityTokenDescriptor
              {
                  Subject = new ClaimsIdentity(new[]
                  {
                  new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(ClaimTypes.NameIdentifier, user.ID)
              }),
                  Expires = DateTime.UtcNow.AddDays(7),
                  SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
              };
              var token = tokenHandler.CreateToken(tokenDescriptor);
              return  tokenHandler.WriteToken(token);
          }*/
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.ID)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<UserResponseModel> GetById(string id)
        {
            var userId = await _userRepository.GetUserById(id);
            if (userId != null)
            {
                return new UserResponseModel
                {
                    Message = "User found Successfully",
                    Sucesss = true,
                    Data = new UserDto
                    {
                        UserId = userId.ID,
                        FirstName = userId.FirstName,
                        LastName = userId.LastName,
                        Email = userId.Email,
                        PhoneNumber = userId.PhoneNumber,

                    },
                };
            }
            return new UserResponseModel
            {
                Message = "User Not Found",
                Sucesss = false,
            };
        }

        public  async Task<UserResponseModel> LoginUser(LoginRequestModel model)
        {
            var user = await _userRepository.GetAsync(u => u.Email == model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                throw new Exception("Invalid login credentials.");
            }

            return new UserResponseModel
            {
                Message = "User Login  Successfully",
                Sucesss = true,
                AccessToken = GenerateJwtToken(user),
                Data = new UserDto
                {
                    UserId = user.ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,

                },
            };
        }

        public async  Task<UserResponseModel> RegisterUser(CreateUserRequestModel model)
        {
            var exist = await _userRepository.ExistsAsync(x=>x.Email==model.Email);
            if (exist) { throw new Exception("User With this email Already Exist"); }
            var organisation = new Organisation
            {
                Name = $"{model.FirstName}'s Organisation",
                Description =  null ,
                DateCreated = DateTime.UtcNow,
                
            };
            var user = new User
            {
                DateCreated = DateTime.UtcNow,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                PhoneNumber = model.PhoneNumber,
                OrganizationId  =  organisation.ID
            };
           await _userRepository.CreateAsync(user);
           
           await _organizationRepository.CreateAsync(organisation);
            return new UserResponseModel
            {
                Message = "User Created Successfully",
                Sucesss = true,
                AccessToken=GenerateJwtToken(user),
                Data = new UserDto
                { 
                    UserId  =  user.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email   =  user.Email,
                    PhoneNumber = user.PhoneNumber,

                },

            };

        }
    }
}
