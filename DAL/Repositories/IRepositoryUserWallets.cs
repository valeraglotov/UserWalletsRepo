using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
  public interface IRepositoryUserWallets
  {
    Task<bool> IsUserWallet(Guid id, int userId);

    Task<bool> TransferCrypto(Guid senderGuid,Guid reciverGuid ,int amount);
  }
}
