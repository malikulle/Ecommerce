using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Hashing;
using ECommerce.Api.JWT;
using ECommerce.Api.Models;
using ECommerce.Business.Abstract;
using ECommerce.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.Api.Controllers
{
    /// <summary>
    /// Login Register Contoller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private IOptions<appSettings_json> _configuration { get; }
        private readonly IEmailService _emailService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="configuration"></param>
        public AuthController(IUserService userService, IOptions<appSettings_json> configuration, IEmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
        }


        /// <summary>
        /// Register User Apı
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseModel()
                {
                    Message = "Model State is not valid",
                    Result = false,
                    Status = 400
                });

            if (!IsValidEmail(model.Email))
                return BadRequest(new ResponseModel()
                {
                    Message = "Please provide a valid email",
                    Result = false,
                    Status = 404
                });


            if (_userService.IsEmailExist(model.Email))
                return BadRequest(new ResponseModel()
                {
                    Message = "This email address has been take it please try another one",
                    Status = 404,
                    Result = false
                });


            // Hash Password
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            User user = new User()
            {
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Address = model.Address,
                IsActive = false,
                Name = model.Name,
                Surname = model.Surname,
                Phone = model.Phone,
                Role = Role.User,
                ImageUrl = "profile.png"
            };

            await _userService.CreateAsync(user);



            // send Email

            TokenHelpers tokenHelpers = new TokenHelpers(_configuration);

            string VerificationToken = tokenHelpers.GenerateVerificationToken(model.Email);

            string url = _configuration.Value.FrontUrl + "/verifyAccount?token=" + VerificationToken;

            string body = $"Please click <a href = {url} >link</a> link to verify YourAccount";


            await _emailService.SendMail("Verify Account", body, model.Email);
            //
            return Ok(new
            {
                Message = "User has been created successfully",
                Result = true,
                Status = 200,
            });
        }

        /// <summary>
        /// Verify User
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("verifyAccount")]
        public async Task<IActionResult> VerifyAccount([FromQuery] string token)
        {

            if (string.IsNullOrEmpty(token))
                return BadRequest(new ResponseModel()
                {
                    Message = "Please Provide a token",
                    Result = false,
                    Status = 400
                });


            try
            {
                var jwt = new JwtSecurityToken(jwtEncodedString: token);

                var User = _userService.GetByEmail(jwt.Claims.FirstOrDefault(x => x.Type == ECommerceClaimTypes.Email)?.Value);

                if (User == null)
                    return BadRequest(new ResponseModel()
                    {
                        Message = "User Not Found",
                        Status = 404,
                        Result = false
                    });

                if (User.IsActive)
                    return BadRequest(new ResponseModel()
                    {
                        Message = "Your account has already been activated.",
                        Status = 404,
                        Result = false
                    });

                User.IsActive = true;

                await _userService.UpdateAsync(User);

                return Ok(new ResponseModel()
                {
                    Result = true,
                    Status = 200,
                    Message = "Account Activated"
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
        /// <summary>
        /// Login Web Site API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseModel()
                {
                    Message = "Model State is not valid",
                    Result = false,
                    Status = 400
                });


            if (!IsValidEmail(model.Email))
                return BadRequest(new ResponseModel()
                {
                    Message = "Please provide a valid email",
                    Result = false,
                    Status = 404
                });

            var user = _userService.GetByEmail(model.Email);

            if (user == null)
                return BadRequest(new ResponseModel()
                {
                    Message = "User Not Found",
                    Status = 404,
                    Result = false
                });
            TokenHelpers tokenHelpers = new TokenHelpers(_configuration);

            if (!user.IsActive)
            {

                string VerificationToken = tokenHelpers.GenerateVerificationToken(model.Email);

                string url = _configuration.Value.FrontUrl + "/token=" + VerificationToken;

                string body = $"Please click <a href = {url} >link</a> link to verify YourAccount";


                await _emailService.SendMail("Verify Account", body, model.Email);
                return BadRequest(new ResponseModel()
                {
                    Message = "Account is not active please active your account. New Activation link has been sent",
                    Status = 404,
                    Result = false
                });
            }
            
            if (!HashingHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest(new ResponseModel()
                {
                    Message = "Password is wrong. Try Again.",
                    Result = false,
                    Status = 404
                });


            TokenResultModel token =  tokenHelpers.GenerateToken(user);

            user.LastLoggedInDate = DateTime.Now;

            await _userService.UpdateAsync(user);

            return Ok(new
            {
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Surname,
                    user.Email,
                    user.Address,
                    role =  user.Role.ToString(),
                    user.Phone,
                    user.ImageUrl,
                    user.LastLoggedInDate
                },
                token
            });
        }


        /// <summary>
        /// Check Email is email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [NonAction]
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
