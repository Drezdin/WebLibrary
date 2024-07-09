using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebLibrary.Application.Services;
using WebLibrary.Core.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;

        public AuthorizeController(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn, IOptions<JWTSettings> optAccess)
        {
            _userManager = user;
            _signInManager = signIn;
            _options = optAccess.Value;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDop userS)
        {
            var user = new IdentityUser { UserName = userS.UserName, Email = userS.Email };

            var result = await _userManager.CreateAsync(user, userS.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim("Role", userS.Role.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, userS.Email));

                await _userManager.AddClaimsAsync(user, claims);
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }

        private string GetToken(IdentityUser user, IEnumerable<Claim> prinicpal)
        {

            var claims = prinicpal.ToList();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                        issuer: _options.Issuer,
                        audience: _options.Audience,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                 );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost("SingIn")]
        public async Task<IActionResult> SingIn(UserS userS)
        {
            var user = await _userManager.FindByEmailAsync(userS.Email);

            var result = await _signInManager.PasswordSignInAsync(user, userS.Password, false, false);

            if (result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                var token = GetToken(user, claims);

                return Ok(token);
            }

            return BadRequest();
        }

        [HttpDelete("Delete")]
        [Authorize("AdminOnly")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("UserDeleted");
            }

            return BadRequest();
        }

        [HttpGet("GetUsers")]
        [Authorize("AdminOnly")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            return View(users);
        }

        [HttpPut("ChangePassword")]
        [Authorize("AdminOnly")]
        public async Task<IActionResult> ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return Ok("UserDeleted");
            }

            return BadRequest();
        }
    }
}
