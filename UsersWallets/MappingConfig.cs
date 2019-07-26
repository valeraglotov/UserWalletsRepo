using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DAL.Entity;

namespace UsersWallets
{
    public static class MappingConfig
    {
        public static void InitializeAutoMapper()
        {
            //Mapper.Initialize(cfg => { });
            Mapper.Initialize(cfg =>
            {

                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<User, UserModel >();

                cfg.CreateMap<WalletModel, Wallet>();
                cfg.CreateMap<Wallet, WalletModel>();

               cfg.CreateMap<UsersWalletsModel, UserWallets>();
               cfg.CreateMap<UserWallets, UsersWalletsModel>();

                cfg.CreateMap<IEnumerable<Wallet>, IEnumerable<WalletModel>>();
                cfg.CreateMap<IEnumerable<WalletModel>, IEnumerable<Wallet>>();

            });
        }
    }
}
