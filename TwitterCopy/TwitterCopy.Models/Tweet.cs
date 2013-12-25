using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TwitterCopy.Models.Contracts;

namespace TwitterCopy.Models
{
    public class Tweet : IAuditInfo
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        [MaxLength(200, ErrorMessage="The length of the tweet canot be more than 200 characters.")]
        public string Status { get; set; }

        #region IAuditInfo
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        #endregion
    }
}