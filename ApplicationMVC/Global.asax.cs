using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ApplicationMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        /// <summary>
        /// Localization operations
        /// </summary>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var lang = "tr"; // Default language
            var cookie = Request.Cookies["MultiLanguage"];
            if (cookie != null && cookie.Value != null)
            {
                lang = cookie.Value;
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        }
    }
}
