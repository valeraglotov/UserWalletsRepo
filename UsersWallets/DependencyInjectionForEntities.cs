using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DAL.Entity;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace UsersWallets
{
  public static class DependencyInjectionForEntities
  {
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IWalletService, WalletService>();
      services.AddScoped<IUsersWalletsService, UsersWalletsService>();
     

      services.AddScoped<IRepository<User>, UserRepository>();
      services.AddScoped<IRepository<Wallet>, WalletRepository>();
      services.AddScoped<IRepository<UserWallets>, UsersWalletsRepository>();
      services.AddScoped<IRepositoryUserWallets, UsersWalletsRepository>();
      services.AddScoped<IRepositoryUser, UserRepository>();
      services.AddScoped<IRepositoryWallet, WalletRepository>();
      return services;
    }
  }
}
