using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
  public class WalletModelPost
  {
    public Guid Sent { get; set; }
    public int UserId { get; set; }
    public Guid Id { get; set; }
    public int Amount { get; set; }
  }
}
