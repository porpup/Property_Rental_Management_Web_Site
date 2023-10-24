using Property_Rental_Management_Web_Site.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Property_Rental_Management_Web_Site.Controllers {
	public class AppointmentsController: Controller {
		private readonly PropertyRentalDBEntities db = new PropertyRentalDBEntities();

		// GET: Appointments
		public ActionResult Index() {
			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
				ViewBag.Username = user.FirstName + " " + user.LastName;
			}
			var appointments = db.Appointments.Include(a => a.Apartment).Include(a => a.User).Include(a => a.User1);
			return View(appointments.ToList());
		}

		// GET: Appointments/Details/5
		public ActionResult Details(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Appointment appointment = db.Appointments.Find(id);
			if (appointment == null) {
				return HttpNotFound();
			}
			return View(appointment);
		}

		// GET: Appointments/Create
		public ActionResult Create() {
			ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Extra");
			ViewBag.ManagerID = new SelectList(db.Users, "UserID", "FirstName");
			ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName");
			return View();
		}

		// POST: Appointments/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "AppointmentID,ApartmentID,ManagerID,TenantID,Date,Status")] Appointment appointment) {
			if (ModelState.IsValid) {
				db.Appointments.Add(appointment);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Extra", appointment.ApartmentID);
			ViewBag.ManagerID = new SelectList(db.Users, "UserID", "FirstName", appointment.ManagerID);
			ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName", appointment.TenantID);
			return View(appointment);
		}

		// GET: Appointments/Edit/5
		public ActionResult Edit(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Appointment appointment = db.Appointments.Find(id);
			if (appointment == null) {
				return HttpNotFound();
			}
			ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "ApartmentID", appointment.ApartmentID);
			ViewBag.ManagerID = new SelectList(db.Users, "UserID", "UserID", appointment.ManagerID);
			ViewBag.TenantID = new SelectList(db.Users, "UserID", "UserID", appointment.TenantID);
			return View(appointment);
		}

		// POST: Appointments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "AppointmentID,ApartmentID,ManagerID,TenantID,Date,Status")] Appointment appointment) {
			if (ModelState.IsValid) {
				db.Entry(appointment).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "ApartmentID", appointment.ApartmentID);
			ViewBag.ManagerID = new SelectList(db.Users, "UserID", "UserID", appointment.ManagerID);
			ViewBag.TenantID = new SelectList(db.Users, "UserID", "UserID", appointment.TenantID);
			return View(appointment);
		}

		// GET: Appointments/Delete/5
		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Appointment appointment = db.Appointments.Find(id);
			if (appointment == null) {
				return HttpNotFound();
			}
			return View(appointment);
		}

		// POST: Appointments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			Appointment appointment = db.Appointments.Find(id);
			db.Appointments.Remove(appointment);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}






		public ActionResult MyAppointments() {
			// Check if the user is authenticated
			if (User.Identity.IsAuthenticated) {
				// Get the email of the currently logged-in user
				string userEmail = User.Identity.Name;

				List<Appointment> userAppointments;
				using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
					var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
					ViewBag.Username = user.FirstName + " " + user.LastName;

					// Explicitly include the related entities (adjust these based on your data model)
					userAppointments = context.Appointments
							.Include(a => a.User)
							.Include(a => a.User1) // Assuming User1 is related to the Appointment entity
							.Include(a => a.Apartment.Address) // Include the Apartment entity
							.Where(a => a.User1.Email == userEmail)
							.ToList();
				}

				return View(userAppointments);
			} else {
				// Handle the case when the user is not authenticated
				// You might want to redirect them to the login page or take other actions.
				return RedirectToAction("Login", "Login");
			}
		}






	}
}
