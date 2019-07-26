using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
  public class UsersWalletsModel
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public Guid WalletId { get; set; }
  }
}
