using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MintNftBlazorApp.Shared.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MintNftBlazorApp.Server.Controllers
{
    public class AuthController : Controller
    {
        private string CreateJWT(UserModel user)
        {
            var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("NOT VERY LONG SECRET KEY"));
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var claims = new[] 
			{
                new Claim(ClaimTypes.Name, user.Username),
			};

            var token = new JwtSecurityToken(
                issuer: "mintnft.com", 
                audience: "mintnft.com", 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(60), 
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("api/auth/login")]
        public async Task<ResultModel> Post([FromBody] UserModel user)
        {            
            if (user != null)
                if (user.Username == "user" && user.Password == "password")
                    return new ResultModel { Message = "Login successful.",Username = user.Username, JwtBearer = CreateJWT(user), Success = true };
            return new ResultModel { Message = "User name / password is incorrect.", Success = false };
        }
    }
}
