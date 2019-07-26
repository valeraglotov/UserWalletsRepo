using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            
        }
        [Key]
        public int Id { get; set; }
    }
}
