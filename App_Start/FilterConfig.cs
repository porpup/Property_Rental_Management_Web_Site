﻿using System.Web;
using System.Web.Mvc;

namespace Property_Rental_Management_Web_Site {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
	}
}
