using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using UsersWallets.Data;

namespace DAL.Data
{
  public class SeedDataBase
  {
    public static void Initialize(IServiceProvider service)
    {
      var context = service.GetRequiredService<ApplicationDbContext>();

      #region MyRegion




      //var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
      //context.Database.EnsureCreated();
      //if (!context.Users.Any())
      //{
      //    ApplicationUser user = new ApplicationUser()
      //    {
      //        Email = "a@b.com",
      //        SecurityStamp = Guid.NewGuid().ToString("N"),
      //        UserName = "Val"
      //    };
      //     userManager.CreateAsync(user, "Password@123");
      //}
      //context.Users.Add(new User()
      //{
      //    LastName = "lastName 3",
      //    FirstName = "lastName 3"
      //});
      #endregion

      for (int i = 0; i < 4; i++)
      {
        string type = "";
        if (i == 0)
        {
          type = "BitCoin";
        }
        if (i == 1)
        {
          type = "BitCoin";
        }
        if (i == 2)
        {
          type = "FreeCoin";
        }
        if (i == 3)
        {
          type = "CryptoCoin";
        }

        context.Wallets.Add(new Wallet()
        {
          Id = Guid.Parse(Guid.NewGuid().ToString().Replace("-", string.Empty)),
          Type = type,
          Amount = 2+i+1
        });


      }
     

      context.SaveChanges();
    }
  }
}
