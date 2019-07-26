using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using DAL.Entity;
using DAL.Interfaces;
using DAL.Repositories;
using Helpers;

namespace BLL
{
  public class WalletService : IWalletService
  {
    private readonly IRepository<Wallet> _repository;
    private readonly IRepositoryWallet _repositoryWallet;

    public WalletService(IRepository<Wallet> repository, IRepositoryWallet repositoryWallet)
    {
      _repository = repository;
      _repositoryWallet = repositoryWallet;
    }

    public async Task<bool> Delete(string id)
    {
      return Mapper.Map<bool>(await _repository.Remove(id));
    }

    public async Task<IEnumerable<WalletModel>> GetAllWallets()
    {
      IEnumerable<Wallet> list = await _repository.GetAll();
      IEnumerable<WalletModel> wallets = list.Select(store => new WalletModel()
      {
        Id = store.Id,
        Amount = store.Amount,
        Type = store.Type
      });

      return wallets;
    }

    public async Task<IEnumerable<WalletModel>> GetSortedSearch(SortModel data)
    {
      IEnumerable<Wallet> res = await _repositoryWallet.GetSortedSearch(data);
      var wallets = (from e in res select new WalletModel
          { Id = e.Id, Amount = e.Amount, Type = e.Type }).AsEnumerable();

      return wallets;
    }

    public async Task<WalletModel> GetWalletById(string id)
    {
      return Mapper.Map<WalletModel>(await _repository.Get(id));
    }

    public async Task<bool> Update(WalletModel value)
    {
      return await _repository.Update(Mapper.Map<Wallet>(value));
    }
  }
}
