using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using BLL;
using Common;
using DAL.Entity;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;


namespace UsersWallets.Controllers
{

  [Route("api/[controller]")]
  public class ValuesController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly IWalletService _walletService;
    private readonly IUsersWalletsService _usersWalletsService;

    public ValuesController(IUserService userService,
    IWalletService walletService,
    IUsersWalletsService usersWalletsService)
    {
      _userService = userService;
      _walletService = walletService;
      _usersWalletsService = usersWalletsService;
    }

    // GET api/values
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WalletModel>>> Get()
    {
      IEnumerable<WalletModel> wallets = await _walletService.GetAllWallets();
     
      return Ok(wallets);
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FormModel>> Get(string id)
    {
      try
      {
        FormModel model = await GetFormModel(id);
        return Ok(model);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    private async Task<FormModel> GetFormModel(string id)
    {
      UsersWalletsModel userWallet = await _usersWalletsService.GetUsersWalletsById(id);
      WalletModel wallet = await _walletService.GetWalletById(userWallet.WalletId.ToString());
      UserModel user = await _userService.GetUserById(userWallet.UserId);
      FormModel model = new FormModel()
      {
        Id = user.Id,
        FullName = user.FirstName + " " + user.LastName,
        Sent = userWallet.WalletId,
        Amount = wallet.Amount,
        Type = wallet.Type
      };
      return model;
    }

    // POST api/values
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] WalletModelPost value)
    {
      try
      {
        bool result = await _usersWalletsService.IsUserWallet(value);
        if (result)
        {
          await _usersWalletsService.TransferCrypto(value);
        }
        return Ok(value);
      }
      catch (AppException ex)
      {
        return BadRequest(ex.Message);
      }
    }
  
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody]WalletModel value)
    {
      try
      {
        bool result = await _walletService.Update(value);
        return Ok();
      }
      catch (AppException ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      bool result = await _walletService.Delete(id);
      return Ok();
    }
  }
 
  public class FormModel
  {
    public int Id { get; set; }

    public Guid Sent { get; set; }

    public string FullName { get; set; }

    public int Amount { get; set; }

    public string Type { get; set; }
  }
}
