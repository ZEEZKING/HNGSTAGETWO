using HNGSTAGETWO.Dtos;
using HNGSTAGETWO.Dtos.RequestModel;
using HNGSTAGETWO.Interfaces.IServices;
using HNGSTAGETWO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HNGSTAGETWO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly  IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CreateUserRequestModel model)
        {
            try
            {
                var user = await _userServices.RegisterUser(model);
                return Created("", new
                {
                    status = "success",
                    message = "Registration successful",
                    data = new
                    {
                        accessToken = user.AccessToken,
                        user = new
                        {
                            user.Data.UserId,
                            user.Data.FirstName,
                            user.Data.LastName,
                            user.Data.Email,
                            user.Data.PhoneNumber
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = "Bad request",
                    message = ex.Message,
                    statusCode = 400
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            try
            {
                var token = await _userServices.LoginUser(model);
                return Ok(new
                {
                    status = "success",
                    message = "Login successful",
                    data = new
                    {
                        accessToken = token.AccessToken,
                        user = new
                        {
                           token.Data.UserId,
                            token.Data.FirstName,
                            token.Data.LastName,
                            token.Data.Email,
                            token.Data.PhoneNumber
                        }

                    }
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new
                {
                    status = "Bad request",
                    message = "Authentication failed",
                    statusCode = 401
                });
            }
        }
    }

}

