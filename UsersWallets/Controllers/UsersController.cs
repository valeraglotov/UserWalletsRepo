using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Http;
using BLL;
using Common;
using DAL.Entity;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace UsersWallets.Controllers
{
  [Authorize]
  [Route("[controller]")]
  public class UsersController : Controller
  {
    private readonly AppSettings _appSettings;
    private readonly IUserService _userService;
    //string url = "https://localhost:44349/api/values";
    public UsersController(IUserService userService, IOptions<AppSettings> appSettings)
    {
      _userService = userService;
      _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserModel userModel)
    {
      User user = (User)await _userService.Authenticate(userModel.Username, userModel.Password);
      if (user == null)
      {
        return BadRequest("Username or password is incorrect");
      }
      
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString())
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials
          (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return Ok(new
      {
        Id = user.Id,
        UserName = user.Username,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Token = tokenString
      });
    }

    //[OverrideAuthentication]
    [HttpGet("{id}")]
    public IActionResult Wallets(int id)
    {
      try
      {
        return Ok();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserModel user)
    {
      //return BadRequest("public IActionResult Register([FromBody] UserModel user)");
      try
      {
        await _userService.Create(user);
        return Ok();
      }
      catch (AppException ex)
      {
        return BadRequest(ex.Message);
      }
    }

    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      //bool result = await _walletService.Delete(id);
      return Ok();
    }
  }

  public class TokenModel
  {
    public string Id { get; set; }
    public string userName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token  { get; set; }
}
}