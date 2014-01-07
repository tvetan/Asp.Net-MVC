using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TwitterCopy.Helpers;
namespace TwitterCopy.Web.Tests.HtmlHelpers
{
    [TestClass]
    public class DisplayWithLinksTests
    {
        // todo fix tests
        [TestMethod]
        public void ShouldReturnSameTextWhenContentHasNoUsernames()
        {
            //var result =  HtmlHelper.DisplayWithLinks("test");
             

            //Assert.AreEqual("test", result);
        }
    }

    //public static class HtmlEnumHelper
    //{
    //    private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
    //    {
    //        var realModelType = modelMetadata.ModelType;

    //        var underlyingType = Nullable.GetUnderlyingType(realModelType);
    //        if (underlyingType != null)
    //        {
    //            realModelType = underlyingType;
    //        }
    //        return realModelType;
    //    }

    //    private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

    //    public static string GetEnumDescription<TEnum>(TEnum value)
    //    {
    //        var fi = value.GetType().GetField(value.ToString());

    //        var attributes =
    //            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

    //        return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
    //    }

    //    public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
    //                                                                   Expression<Func<TModel, TEnum>> expression,
    //                                                                   object htmlAttributes = null)
    //    {
    //        var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
    //        var enumType = GetNonNullableModelType(metadata);
    //        var values = Enum.GetValues(enumType).Cast<TEnum>();

    //        var items = from value in values
    //                    select new SelectListItem
    //                    {
    //                        Text = GetEnumDescription(value),
    //                        Value = value.ToString(),
    //                        Selected = value.Equals(metadata.Model)
    //                    };

    //        if (metadata.IsNullableValueType)
    //            items = SingleEmptyItem.Concat(items);

    //        return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
    //    }
    //}
}
