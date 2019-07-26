using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DAL.Entity;

namespace BLL
{
  public interface IUserService
  {
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel> GetUserById(int id);
    Task<bool> Create(UserModel user);
    Task<object> Authenticate(string username, string password);
  }
}