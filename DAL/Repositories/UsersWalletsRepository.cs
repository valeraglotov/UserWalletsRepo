using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Interfaces;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UsersWallets.Data;

namespace DAL.Repositories
{
  public class UsersWalletsRepository : IRepository<UserWallets>, IRepositoryUserWallets
  {
    private readonly ApplicationDbContext _context;

    public UsersWalletsRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public Task<bool> Create(UserWallets item, string password)
    {
      throw new NotImplementedException();
    }

    public async Task<UserWallets> Get(string id)
    {
      //Guid guid = Guid.Parse(id);
      return await _context.UserWallets.FirstOrDefaultAsync(u => u.WalletId == Guid.Parse(id));
    }

    public Task<IEnumerable<UserWallets>> GetAll()
    {
      throw new NotImplementedException();
    }

    public Task<bool> Remove(string id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> IsUserWallet(Guid id, int idUser)
    {
      var temp = await _context.UserWallets.FirstOrDefaultAsync(s => s.WalletId == id);
      if (temp == null)
      {
        throw new AppException("An entity with such a Guid is absent");
      }

      //if (temp.UserId != idUser)
      //{
      //  throw new AppException("An entity with such a idUser is absent");
      //}

      return await Task.FromResult(true);
    }

    public async Task<bool> TransferCrypto(Guid senderGuid, Guid reciverGuid, int amount)
    {
      var walletSent = await _context.Wallets.Where(u => u.Id == senderGuid).FirstOrDefaultAsync();
      if (amount <= 0 || amount > walletSent.Amount)
      {
        throw new AppException("Amount must be more than zero or Amount more than there is");
      }
      if (senderGuid.Equals(reciverGuid))
      {
        throw new AppException("Can't be sent on the same wallet");
      }

      var userWalletSend = await _context.UserWallets.Where(u => u.WalletId == senderGuid).FirstOrDefaultAsync();
      var userWalletRec = await _context.UserWallets.Where(u => u.WalletId == reciverGuid).FirstOrDefaultAsync();
      if (userWalletSend.UserId == userWalletRec.UserId)
      {
        throw new AppException("Can't be sent on the same wallet of user");
      }

      walletSent.Amount -= amount;
      _context.Update(walletSent);
      await _context.SaveChangesAsync();

      var walletRec = await _context.Wallets.Where(u => u.Id == reciverGuid).FirstOrDefaultAsync();
      walletRec.Amount += amount;
      _context.Update(walletRec);
      await _context.SaveChangesAsync();

      return await Task.FromResult(true);
    }

    public Task<bool> Update(UserWallets item)
    {
      throw new NotImplementedException();
    }
  }
}
