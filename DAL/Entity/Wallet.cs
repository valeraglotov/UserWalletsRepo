using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entity
{
    public class Wallet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Wallet()
        {
            UserWallets = new HashSet<UserWallets>();
        }

        public string Type { get; set; }
        public int Amount { get; set; }

        public virtual ICollection<UserWallets> UserWallets { get; set; }
    }
}
