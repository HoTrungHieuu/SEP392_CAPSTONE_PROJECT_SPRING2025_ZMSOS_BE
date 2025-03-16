using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService service, IConfiguration configuration)
        {
            this.service = service;
            _configuration = configuration;
        }
        [HttpGet("accounts")]
        public async Task<IActionResult> GetListAccount()
        {
            var result = await service.GetListAccount();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetListRole()
        {
            var result = await service.GetListRole();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("account/login")]
        public async Task<IActionResult> Login(AccountLogin key)
        {
            var result = await service.Login(key.AccountName, key.Password);
            if (result.Status == 200)
            {
                var account = (AccountView)result.Data;
                var token = GenerateJwtToken(account.Id, account.Role.RoleName);
                account.JwtToken = token;
            }
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("account/create")]
        public async Task<IActionResult> CreateAccount(AccountCreate key)
        {
            var result = await service.CreateAccount(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        private string GenerateJwtToken(int id, string role)
        {
            var key = _configuration["JwtSettings:Key"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("role", role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("account")]
        public async Task<IActionResult> UpdateAccount(AccountUpdate key)
        {
            var result = await service.UpdateAccount(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
