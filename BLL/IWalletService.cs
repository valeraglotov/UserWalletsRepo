using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;
using Helpers;

namespace BLL
{
  public interface IWalletService
  {
    Task<IEnumerable<WalletModel>> GetAllWallets();
    Task<WalletModel> GetWalletById(string id);
    Task<bool> Update(WalletModel value);
    Task<bool> Delete(string id);
    Task<IEnumerable<WalletModel>> GetSortedSearch(SortModel data);
  }
}
