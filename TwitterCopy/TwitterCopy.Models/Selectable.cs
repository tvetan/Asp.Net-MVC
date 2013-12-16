using System;
using System.Collections.Generic;
using System.Linq;
using TwitterCopy.Models.Contracts;

namespace TwitterCopy.Models
{
    public class Selectable : ISelectable, IUsers
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Code { get; set; }

        protected ICollection<ApplicationUser> users;

        public virtual ICollection<ApplicationUser> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
            }
        }
    }
}
