namespace TwitterCopy.Web.Common.EnumExtensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public static class EnumHelpers
    {
        public static IEnumerable<SelectListItem> GetItems(this Type enumType, int? selectedValue)
        {
            IsValidEnum(enumType);

            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();

            var items = names.Zip(values, (name, value) => new SelectListItem
            {
                Text = GetName(enumType, name),
                Value = value.ToString(),
                //Selected = value == selectedValue
            });

            return items;
        }

        private static void IsValidEnum(Type enumType)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
            {
                throw new ArgumentException("Type must be an enum");
            }
        }

        //public static string GetSelectedName(this Type enumType, int? selectedValue)
        //{
        //    IsValidEnum(enumType);
        //    var names = Enum.GetNames(enumType);
        //    var values = Enum.GetValues(enumType).Cast<int>();

        //    var nameValuePairs = names.Zip(values, (name, value) =>
        //    {
        //        return new
        //        {
        //            Name = name,
        //            Value = value
        //        };
        //    });

        //    foreach (var nameValueItem in nameValuePairs)
        //    {
        //        if (nameValueItem.Value == selectedValue)
        //        {
        //            return GetName(enumType, nameValueItem.Name);
        //        }
        //    }

        //    return GetName(enumType, names.FirstOrDefault());
        //}

        private static string GetName(Type enumType, string name)
        {
            var result = name;
            var attribute = enumType.GetField(name)
                                    .GetCustomAttributes(inherit: false)
                                    .OfType<DisplayAttribute>()
                                    .FirstOrDefault();

            if (attribute != null)
            {
                result = attribute.GetName();
            }

            return result;
        }
    }
}