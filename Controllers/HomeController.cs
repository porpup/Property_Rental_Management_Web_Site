using Property_Rental_Management_Web_Site.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Property_Rental_Management_Web_Site.Controllers {
	public class HomeController: Controller {
		public ActionResult Index() {
			List<Apartment> apartments;
			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				apartments = context.Apartments.Include("Address").Where(a => a.Status == "Available").ToList();
			}

			return View(apartments);
		}

	}
}