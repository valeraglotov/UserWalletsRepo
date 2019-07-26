using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Interfaces;
using Helpers;
using Microsoft.EntityFrameworkCore;
using UsersWallets.Data;


namespace DAL.Repositories
{
  public class WalletRepository : IRepository<Wallet>, IRepositoryWallet
  {
    private readonly ApplicationDbContext _context;

    public WalletRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public Task<bool> Create(Wallet item,string password)
    {
      throw new NotImplementedException();
    }

    public async Task<Wallet> Get(string id)
    {
      return await _context.Wallets.FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));
    }

    public async Task<IEnumerable<Wallet>> GetAll()
    {
      return await _context.Wallets.ToListAsync();
    }

    public async Task<IEnumerable<Wallet>> GetSortedSearch(SortModel data)
    {
      PropertyInfo propertyInfo = null;
      if (!string.IsNullOrEmpty(data.Key))
      {
        propertyInfo = typeof(Wallet).GetProperty(data.Key);
      }
      
      IEnumerable<Wallet> queryable = await GetAll();

      if (!string.IsNullOrEmpty(data.Search))
      {
        queryable = queryable.Where
        (c => c.Id.ToString().Equals(data.Search)
              || c.Type.Equals(data.Search)
              || c.Amount.ToString().Equals(data.Search));
        //return queryable;
      }

      var skipped = (data.PagerModel.CurrentPage - 1) * data.PagerModel.PageSize;
      queryable = queryable.Skip(skipped).Take(data.PagerModel.PageSize);

      if (!string.IsNullOrEmpty(data.Key) && !string.IsNullOrEmpty(data.SortedFilter))
      {
        if (data.SortedFilter.Contains("asc"))
        {
          queryable = queryable.OrderByDescending(s => propertyInfo.GetValue(s, null));
        }

        if (data.SortedFilter.Contains("desc"))
        {
          queryable = queryable.OrderBy(s => propertyInfo.GetValue(s, null));
        }
      }
      
      return queryable;
    }

    //private async Task<IQueryable<Wallet>> GetAllQueryable()
    //{
    //    var authors = await _context.Wallets.ToListAsync();
    //    return authors.AsQueryable();
    //}

    public async Task<bool> Remove(string id)
    {
      _context.Remove(_context.Wallets.Single(a => a.Id == Guid.Parse(id)));
      return Convert.ToBoolean(await _context.SaveChangesAsync());
    }

    public async Task<bool> Update(Wallet item)
    {
      _context.Wallets.Update(item);
      return Convert.ToBoolean(await _context.SaveChangesAsync());
    }
  }
}
