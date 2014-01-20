namespace TwitterCopy.Models.DataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Web;

    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> types;

        public FileTypesAttribute(string types)
        {
            this.types = types.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var fileExtension = Path.GetExtension((value as HttpPostedFileBase).FileName).Substring(1);

            return this.types.Contains(fileExtension);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid file type. Only the following types {0} are supported.", string.Join(", ", this.types));
        }
    }
}