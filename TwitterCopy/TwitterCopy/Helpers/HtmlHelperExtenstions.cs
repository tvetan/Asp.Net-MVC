namespace TwitterCopy.Helpers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    public static class HtmlHelperExtenstions
    {
        public static MvcHtmlString GenerateProfilePicture(this HtmlHelper html, string username)
        {
            return GenerateProfilePicture(html, username, null);
        }

        public static MvcHtmlString GenerateProfilePicture(this HtmlHelper html, string username, object attributes)
        {
            var builder = new TagBuilder("img");

            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            builder.MergeAttribute("src", urlHelper.Action("UploadProfilePicture", "User", new { username = username })); // "/User/UploadProfilePicture?username=" + username);
            if (attributes != null)
            {
                var atts = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
                builder.MergeAttributes(atts);
            }
            
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString DisplayWithLinks(this HtmlHelper htmlHelper, string content)
        {
            StringBuilder contentWithLinks = new StringBuilder();

            for (int index = 0; index < content.Length; index++)
            {
                if (content[index] == '@' && IsValidUsername(index, content))
                {
                    string username = ExtractUsername(index + 1, content);
                    TagBuilder usernameLink = CreateUsernameLinkTag(username);
                    contentWithLinks.Append(usernameLink);
                    index += username.Length; 
                }
                else
                {
                    contentWithLinks.Append(content[index]);
                }
            }

            return new MvcHtmlString(contentWithLinks.ToString());
        }
  
        private static TagBuilder CreateUsernameLinkTag(string username)
        {
            TagBuilder usernameLink = new TagBuilder("a");
            usernameLink.SetInnerText("@" + username);
            usernameLink.MergeAttribute("href", username);

            return usernameLink;
        }

        private static string ExtractUsername(int startIndex, string content)
        {
            int whiteSpaceIndex = content.IndexOf(' ', startIndex);
            string username = string.Empty;
            if (whiteSpaceIndex < 0)
            {
                username = content.Substring(startIndex);
            }
            else
            {
                username = content.Substring(startIndex, whiteSpaceIndex - startIndex);
            }
             
            return username;
        }

        private static bool IsValidUsername(int startIndex, string content)
        {
            var username = ExtractUsername(startIndex + 1, content);
            if (username.Contains('@'))
            {
                return false;
            }

            if (startIndex == 0)
            {
                return true;
            }

            if (char.IsWhiteSpace(content[startIndex - 1]))
            {
                return true;
            }

            return false;
        }
    }
}