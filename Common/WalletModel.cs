using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class WalletModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
    }
}
