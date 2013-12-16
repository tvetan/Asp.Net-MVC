using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitterCopy.Models.Contracts
{
    public interface IUsers
    {
       ICollection<ApplicationUser> Users { get; set; }
    }
}
