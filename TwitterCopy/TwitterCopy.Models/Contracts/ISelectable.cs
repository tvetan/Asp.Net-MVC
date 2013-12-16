using System;
using System.Linq;

namespace TwitterCopy.Models.Contracts
{
    public interface ISelectable
    {
        int Id { get; set; }

        string Name { get; set; }

        string Code { get; set; }
    }
}
