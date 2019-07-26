using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using DAL.Entity;
using DAL.Interfaces;
using DAL.Repositories;

namespace BLL
{
  public class UsersWalletsService : IUsersWalletsService
  {
    private readonly IRepository<UserWallets> _repository;
    private readonly IRepositoryUserWallets _repositoryUserWallets;

    public UsersWalletsService(IRepository<UserWallets> repository,
      IRepositoryUserWallets repositoryUserWallets)
    {
      _repository = repository;
      _repositoryUserWallets = repositoryUserWallets;
    }

    public Task<IEnumerable<UsersWalletsModel>> GetAllUsersWallets()
    {
      throw new NotImplementedException();
    }

    public async Task<UsersWalletsModel> GetUsersWalletsById(string id)
    {
      return Mapper.Map<UsersWalletsModel>(await _repository.Get(id));
    }

    public Task<bool> IsUserWallet(WalletModelPost value)
    {
      return _repositoryUserWallets.IsUserWallet(value.Id, value.UserId);
    }

    public async Task<bool> TransferCrypto(WalletModelPost value)
    {
      return await _repositoryUserWallets.TransferCrypto(value.Sent,value.Id ,value.Amount);
    }
  }
}
