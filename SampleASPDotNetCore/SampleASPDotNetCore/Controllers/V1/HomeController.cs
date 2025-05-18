using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleASPDotNetCore.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleASPDotNetCore.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        [Route("item")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Based on this scheme it will check the token in the header and validate it
        public IActionResult Index()
        {
             return Ok("string");
        }
        /// <summary>
        /// Added this for apply Rolebased Authorization. This will check if the user is having Admin role or not.
        /// Authentication scheme is already added in the above method. So it will check for the token in the header and validate it.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getEmaildetails")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy ="Admin")]   
        public IActionResult EmailDetails()
        {
            LoginModel user = new LoginModel();
            var userEmailAddress = user.FetchUserDetails().Select(x=>x.EmailAddress).ToList();
            return Ok(userEmailAddress);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        private string GenerateJSONWebToken(LoginModel userInfo)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

                var claims = new[] {                               
                                new Claim("UserID",userInfo.UserID),// these are the claims which will be visible in jwt.io so not passing sensitive data
                                new Claim("custom_role", userInfo.Role),// these are the claims which will be visible in jwt.io so not passing sensitive data
                            };

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
        private LoginModel AuthenticateUser(LoginModel login)
        {
            LoginModel user = new LoginModel();
            
            return user.FetchUserDetails().FirstOrDefault(x=>x.UserName==login.UserName);
            
            
        }
    }
}
