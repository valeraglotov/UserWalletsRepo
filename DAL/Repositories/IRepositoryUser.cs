using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
  public interface IRepositoryUser
  {
    object Authenticate(string username, string password);
  }
}
