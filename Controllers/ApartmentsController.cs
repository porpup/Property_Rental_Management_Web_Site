using Property_Rental_Management_Web_Site.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Property_Rental_Management_Web_Site.Controllers {
	public class ApartmentsController: Controller {
		private readonly PropertyRentalDBEntities db = new PropertyRentalDBEntities();

		// GET: Apartments
		public ActionResult Index() {
			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
				ViewBag.Username = user.FirstName + " " + user.LastName;
			}
			var apartments = db.Apartments.Include(a => a.Address).Include(a => a.User);
			return View(apartments.ToList());
		}

		// GET: Apartments/Details/5
		public ActionResult Details(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Apartment apartment = db.Apartments.Find(id);
			if (apartment == null) {
				return HttpNotFound();
			}
			return View(apartment);
		}

		// GET: Apartments/Create
		public ActionResult Create() {
			ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Street");
			ViewBag.ManagerID = new SelectList(db.Users, "UserID", "UserID");
			return View();
		}

		// POST: Apartments/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ApartmentID,ManagerID,AddressID,Extra,Price,Status,Street,Unit,ZipCode,City")] Apartment apartment) {
			if (ModelState.IsValid) {
				// Save the apartment with the additional properties
				db.Apartments.Add(apartment);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Street", apartment.AddressID);
			ViewBag.ManagerID = new SelectList(db.Users, "UserID", "UserID", apartment.ManagerID);
			return View(apartment);
		}


		// GET: Apartments/Edit/5
		public ActionResult Edit(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Apartment apartment = db.Apartments.Find(id);
			if (apartment == null) {
				return HttpNotFound();
			}
			ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Street", apartment.AddressID);
			ViewBag.ManagerID = new SelectList(db.Users, "UserID", "UserID", apartment.ManagerID);
			return View(apartment);
		}

		// POST: Apartments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ApartmentID,ManagerID,AddressID,Extra,Price,Status")] Apartment apartment) {
			if (ModelState.IsValid) {
				db.Entry(apartment).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Street", apartment.AddressID);
			ViewBag.ManagerID = new SelectList(db.Users, "UserID", "UserID", apartment.ManagerID);
			return View(apartment);
		}

		// GET: Apartments/Delete/5
		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Apartment apartment = db.Apartments.Find(id);
			if (apartment == null) {
				return HttpNotFound();
			}
			return View(apartment);
		}

		// POST: Apartments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			Apartment apartment = db.Apartments.Find(id);

			if (apartment == null) {
				return HttpNotFound();
			}

			try {
				// Delete all associated messages
				var messagesToDelete = db.Messages.Where(m => m.ApartmentID == id);
				db.Messages.RemoveRange(messagesToDelete);

				// Delete all associated appointments
				var appointmentsToDelete = db.Appointments.Where(a => a.ApartmentID == id);
				db.Appointments.RemoveRange(appointmentsToDelete);

				// Delete the apartment
				db.Apartments.Remove(apartment);
				db.SaveChanges();
				return RedirectToAction("Index");
			} catch (Exception) {
				// Handle any exceptions, if needed
				ViewBag.ErrorMessage = "An error occurred while trying to delete the apartment, messages, and appointments.";
				return View("Error");
			}
		}




		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
