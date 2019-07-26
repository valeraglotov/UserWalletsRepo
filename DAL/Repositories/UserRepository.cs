using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Interfaces;
using Helpers;
using Microsoft.EntityFrameworkCore;
using UsersWallets.Data;


namespace DAL.Repositories
{
  public class UserRepository : IRepository<User>, IRepositoryUser
  {
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public Task<bool> Create(User user, string password)
    {
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new AppException("Password is required");
      }
      //if (password.Length < 6)
      //{
      //  throw new AppException("Password must be at least six characters");
      //}
      if (_context.Users.Any(x => x.Username == user.Username))
      {
        throw new AppException("Username '" + user.Username + "' is already taken");
      }

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      Console.WriteLine(user);
      _context.Users.Add(user);
      _context.SaveChanges();

      return Task.FromResult(true);
    }

    public async Task<User> Get(string id)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(id));
    }

    public async Task<IEnumerable<User>> GetAll()
    {
      return await GetAllQueryable();
    }

    private async Task<IQueryable<User>> GetAllQueryable()
    {
      var authors = await _context.Users.ToListAsync();
      return authors.AsQueryable();
    }

    public Task<bool> Remove(string id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Update(User item)
    {
      throw new NotImplementedException();
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null)
      {
        throw new ArgumentNullException("password");
      }
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
      }
      
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    public object Authenticate(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      {
        return null;
      }
        
      var user = _context.Users.SingleOrDefault(x => x.Username == username);
      if (user == null)
      {
        return null;
      }

      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
      {
        return null;
      }

      return user;
    }

    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      if (password == null)
      {
        throw new ArgumentNullException("password");
      }
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new ArgumentException
          ("Value cannot be empty or whitespace only string.",
          "password");
      }

      if (storedHash.Length != 64)
      {
        throw new ArgumentException("Invalid length of password hash (64 bytes expected).",
          "passwordHash");
      }
      if (storedSalt.Length != 128)
      {
        throw new ArgumentException("Invalid length of password salt (128 bytes expected).",
          "passwordHash");
      }

      using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i])
          {
            return false;
          }
        }
      }

      return true;
    }
  }
}
