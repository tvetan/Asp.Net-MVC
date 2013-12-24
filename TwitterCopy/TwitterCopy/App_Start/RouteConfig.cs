using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TwitterCopy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SearchRoute",
                url: "profile/{action}",
                defaults: new { controller = "profile", action = "index" }
                );

            routes.MapRoute(
                name: "AccountRoute",
                url: "search",
                defaults: new { controller = "Home", action = "search" }
                );
            routes.MapRoute(
                name: "MentionsRoute",
                url: "Mentions",
                defaults: new { controller = "Home", action = "mentions" }
                );

            routes.MapRoute(
                name: "SettingsRoute",
                url: "settings/account",
                defaults: new { controller = "Account", action = "settings" }
                );

            routes.MapRoute(
                name: "ProfileRoute",
                url: "Account/{action}",
                defaults: new { controller = "Account", action = "index" }
                );

            // followers
            routes.MapRoute(
                name: "Followers",
                url: "followers",
                defaults: new { controller = "home", action = "followers" }
                );

            // followings
            routes.MapRoute(
               name: "Followings",
               url: "followings",
               defaults: new { controller = "home", action = "followings" }
               );

            // create
            routes.MapRoute(
               name: "Create",
               url: "create",
               defaults: new { controller = "home", action = "create" }
               );

            routes.MapRoute(
              name: "Follow",
              url: "follow",
              defaults: new { controller = "home", action = "follow" }
              );

            routes.MapRoute(
              name: "Unfollow",
              url: "unfollow",
              defaults: new { controller = "home", action = "unfollow" }
              );

            routes.MapRoute(
              name: "Profiles",
              url: "profiles",
              defaults: new { controller = "home", action = "profiles" }
              );


            // {username}/{action}
            routes.MapRoute(
               name: "UserDefault",
               url: "{username}",
               defaults: new { controller = "user", action = "index" }
               );

            routes.MapRoute(
               name: "WhoToFollowRoute",
               url: "who_to_follow/suggestions",
               defaults: new { controller = "User", action = "suggestions" }
               );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
