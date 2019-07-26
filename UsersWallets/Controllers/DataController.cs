using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using AttributeRouting.Web.Http;
using BLL;
using Common;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace UsersWallets.Controllers
{
  /// <summary>
  ///asdasdasdas
  /// </summary>
  [Route("[controller]")]
  public class DataController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly IWalletService _walletService;
    private readonly IUsersWalletsService _usersWalletsService;

    public DataController(IUserService userService,
      IWalletService walletService,
      IUsersWalletsService usersWalletsService)
    {
      _userService = userService;
      _walletService = walletService;
      _usersWalletsService = usersWalletsService;
    }

    // GET /data
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WalletModel>>> Get()
    {
      IEnumerable<WalletModel> wallets = await _walletService.GetAllWallets();
      return Ok(wallets);
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<WalletModel>>> GetSorted([FromBody] SortModel data)
    {
      IEnumerable<WalletModel> queryable = await _walletService.GetSortedSearch(data);
      return Ok(queryable);
    }

    [HttpPost]
    [Route("pager")]
    public ActionResult<IEnumerable<PagerModel>> GetPagerModel([FromBody] PagerInitModel pager)
    {
      PagerModel model = new PagerModel();
      model.PageSize = pager.PageSize;
      model.TotalItems = pager.TotalItems;
      model.CurrentPage = pager.CurrentPage;
      model.TotalPages = model.TotalItems / model.PageSize;

      if (model.TotalPages <= 10)
      {
        model.StartPage = 1;
        model.EndPage = model.TotalPages;
      }
      model.StartIndex = (model.CurrentPage - 1) * model.PageSize;
      model.EndIndex = Math.Min(model.StartIndex + model.PageSize - 1, model.TotalItems - 1);
      for (int i = 0; i < model.TotalPages; i++)
      {
        model.Pages.Add(i + 1);
      }

      return Ok(model);
    }
  }

  public class PagerInitModel
  {
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
  }

}