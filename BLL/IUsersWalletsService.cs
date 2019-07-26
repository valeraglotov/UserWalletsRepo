using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL
{
  public interface IUsersWalletsService
  {
    Task<IEnumerable<UsersWalletsModel>> GetAllUsersWallets();
    Task<UsersWalletsModel> GetUsersWalletsById(string id);
    Task<bool> IsUserWallet(WalletModelPost item);
    Task<bool> TransferCrypto(WalletModelPost value);
  }
}
