using System;
using System.Linq;

namespace TwitterCopy.Models.Contracts
{
    public interface IAuditInfo
    { 
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
