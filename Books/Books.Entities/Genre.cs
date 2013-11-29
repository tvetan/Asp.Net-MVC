using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Books.Entities
{
    public enum Genre
    {
        [Display(Name ="No Notifications")]
        Nonfiction,
        Romance,
        Action,
        [Display(Name="Science Fiction")]
        ScienceFiction
    }
}
