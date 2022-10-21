
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using Repository_Layer;
using System.Security.Cryptography;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDBcontext _dataContext;
        public UserController(AppDBcontext dataContext)
        {
            _dataContext = dataContext;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_dataContext.Admin.Any(u => u.Username == request.UserName))
            {
                return BadRequest("User already exist");
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new AdminVerify
            {
                Username = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };
            _dataContext.Admin.Add(user);
            await _dataContext.SaveChangesAsync();

            return Ok("user Succesfully created");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLogin request)
        {

            var user = await _dataContext.Admin.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is Incorrect");
            }

            //if (user.VerifiedAt == null)
            //{
            //    return BadRequest("User not verified");
            //}


            return Ok($"Welcome, {user.Username}!:)");


        }
        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var user = await _dataContext.Admin.FirstOrDefaultAsync((u => u.VerificationToken == token));
            if (user == null)
            {
                return BadRequest("invalid token");
            }

            //user.VerifiedAt = DateTime.Now;
            await _dataContext.SaveChangesAsync();

            return Ok("User verified");

        }





        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.
                    ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.
                    ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);

            }
        }



        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

    }
}
