using Property_Rental_Management_Web_Site.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Property_Rental_Management_Web_Site.Controllers {
	public class MessagesController: Controller {
		private readonly PropertyRentalDBEntities db = new PropertyRentalDBEntities();

		// GET: Messages
		public ActionResult Index() {
			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
				ViewBag.Username = user.FirstName + " " + user.LastName;
			}
			var messages = db.Messages.Include(m => m.Apartment).Include(m => m.User).Include(m => m.User1);
			return View(messages.ToList());
		}

		// GET: Messages/Details/5
		public ActionResult Details(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Message message = db.Messages.Find(id);
			if (message == null) {
				return HttpNotFound();
			}
			return View(message);
		}

		// GET: Messages/Create
		public ActionResult Create() {
			ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Extra");
			ViewBag.RecieverID = new SelectList(db.Users, "UserID", "FirstName");
			ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName");
			return View();
		}

		// POST: Messages/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "MessageID,ApartmentID,SenderID,RecieverID,Date,Text")] Message message) {
			if (ModelState.IsValid) {
				db.Messages.Add(message);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Extra", message.ApartmentID);
			ViewBag.RecieverID = new SelectList(db.Users, "UserID", "FirstName", message.RecieverID);
			ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName", message.SenderID);
			return View(message);
		}

		// GET: Messages/Edit/5
		public ActionResult Edit(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Message message = db.Messages.Find(id);
			if (message == null) {
				return HttpNotFound();
			}
			ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "ApartmentID", message.ApartmentID);
			ViewBag.RecieverID = new SelectList(db.Users, "UserID", "UserID", message.RecieverID);
			ViewBag.SenderID = new SelectList(db.Users, "UserID", "UserID", message.SenderID);
			return View(message);
		}

		// POST: Messages/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "MessageID,ApartmentID,SenderID,RecieverID,Date,Text")] Message message) {
			if (ModelState.IsValid) {
				db.Entry(message).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "ApartmentID", message.ApartmentID);
			ViewBag.RecieverID = new SelectList(db.Users, "UserID", "UserID", message.RecieverID);
			ViewBag.SenderID = new SelectList(db.Users, "UserID", "UserID", message.SenderID);
			return View(message);
		}

		// GET: Messages/Delete/5
		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Message message = db.Messages.Find(id);
			if (message == null) {
				return HttpNotFound();
			}
			return View(message);
		}

		// POST: Messages/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			Message message = db.Messages.Find(id);
			db.Messages.Remove(message);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		public ActionResult MyMessages() {
			// Check if the user is authenticated
			if (User.Identity.IsAuthenticated) {
				// Get the email of the currently logged-in user
				string userEmail = User.Identity.Name;

				List<Message> userMessages;
				using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
					var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
					ViewBag.Username = user.FirstName + " " + user.LastName;
					// Explicitly include the related User, User1, and Apartment entities
					userMessages = context.Messages
							.Include(m => m.User)
							.Include(m => m.User1)
							.Include(m => m.Apartment.Address)
							.Where(m => m.User.Email == userEmail || m.User1.Email == userEmail)
							.ToList();
				}

				return View(userMessages);
			} else {
				// Handle the case when the user is not authenticated
				// You might want to redirect them to the login page or take other actions.
				return RedirectToAction("Login", "Login");
			}
		}

	}
}
