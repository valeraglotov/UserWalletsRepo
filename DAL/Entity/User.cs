using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entity
{
    public class User : BaseEntity
    {
        public User()
        {
            UserWallets = new HashSet<UserWallets>();
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual ICollection<UserWallets> UserWallets { get; set; }
    }
}
