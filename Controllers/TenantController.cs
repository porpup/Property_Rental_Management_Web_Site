using Property_Rental_Management_Web_Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Property_Rental_Management_Web_Site.Controllers {
	public class TenantController: Controller {
		// GET: Tenant
		public ActionResult Index() {
			List<Apartment> apartments;
			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				apartments = context.Apartments.Include("Address").Where(a => a.Status == "Available").ToList();
				var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
				ViewBag.Username = user.FirstName + " " + user.LastName;
			}

			return View(apartments);
		}





		// GET: WriteMessage
		public ActionResult WriteMessage(int id) {
			var message = new Message { ApartmentID = id };

			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
				ViewBag.Username = user.FirstName + " " + user.LastName;
			}

			return View(message);
		}


		// POST: WriteMessage
		[HttpPost]
		public ActionResult WriteMessage(Message msg, int id) {
			try {
				// Get the sender (logged-in user)
				string senderEmail = User.Identity.Name;
				User sender = null;
				using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
					sender = context.Users.FirstOrDefault(u => u.Email == senderEmail);
				}

				// Get the receiver (user associated with the specified apartment)
				User receiver = null;
				using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
					var apartment = context.Apartments
													.Where(a => a.ApartmentID == id);
					receiver = apartment.Select(a => a.User).FirstOrDefault();
				}

				// Create a new message
				Message message = new Message {
					ApartmentID = id,
					SenderID = sender.UserID,
					RecieverID = receiver.UserID,
					Date = DateTime.Now,
					Text = msg.Text
				};

				// Add the message to the database
				using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
					context.Messages.Add(message);
					context.SaveChanges();
				}

				return RedirectToAction("MyMessages", "Messages"); // Redirect to the user's messages page
			} catch (Exception ex) {
				// Log the exception or handle it as needed
				Debug.WriteLine("Error saving message: " + ex.Message);
				// You can also return an error view or message to the user
				return View("Error");
			}
		}





		// GET: MakeAppointment
		public ActionResult MakeAppointment(int id) {
			var appointment = new Appointment { ApartmentID = id, Date = DateTime.Today };

			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				var user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
				ViewBag.Username = user.FirstName + " " + user.LastName;
			}

			return View(appointment);
		}


		// POST: MakeAppointment
		[HttpPost]
		public ActionResult MakeAppointment(Appointment app, int id) {
			Debug.WriteLine("Date and Time: " + app.Date.ToString());
			// Get the logged-in user
			string userEmail = User.Identity.Name;
			User user = null;
			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				user = context.Users.FirstOrDefault(u => u.Email == userEmail);
			}

			// Get the associated apartment
			Apartment apartment = null;
			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				apartment = context.Apartments.FirstOrDefault(a => a.ApartmentID == id);
			}

			if (user != null && apartment != null) {
				if (app.Date > DateTime.Now) {
					// Create a new appointment with status "Pending"
					Appointment appointment = new Appointment {
						ApartmentID = id,
						ManagerID = apartment.ManagerID,
						TenantID = user.UserID,
						Date = app.Date,
						Status = "Pending" // Set status to "Pending"
					};

					// Add the appointment to the database
					using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
						context.Appointments.Add(appointment);
						context.SaveChanges();
					}

					return RedirectToAction("MyAppointments", "Appointments"); // Redirect to the user's appointments page
				} else {
					// Handle the case where the appointment date is not in the future
					ViewBag.ErrorMessage = "Appointment date must be in the future.";
					return View("Error");
				}
			} else {
				// Handle the case where user or apartment is not found
				return View("Error");
			}
		}

	}
}
