﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Property_Rental_Management_Web_Site {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
					name: "Default",
					url: "{controller}/{action}/{id}",
					defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
