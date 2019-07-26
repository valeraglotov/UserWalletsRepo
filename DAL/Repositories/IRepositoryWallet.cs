using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using Helpers;

namespace DAL.Repositories
{
  public interface IRepositoryWallet
  {
    Task<IEnumerable<Wallet>> GetSortedSearch(SortModel data);
  }
}
