namespace TwitterCopy.Models.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DeletebleEntity : IDeletableEntity
    {
        [Display(Name = "Deleted?")]
        [Editable(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Date of deletion")]
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}