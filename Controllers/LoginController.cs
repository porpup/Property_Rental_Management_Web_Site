using Property_Rental_Management_Web_Site.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Property_Rental_Management_Web_Site.Controllers {
	public class LoginController: Controller {
		// GET: Login
		public ActionResult Login() {
			return View();
		}

		[HttpPost]
		public ActionResult Login(UserModel model) {
			using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
				bool isValidUser = context.Users.Any(user => user.Email.ToLower() == model.Email.ToLower() && user.Password == model.Password);
				if (isValidUser) {
					FormsAuthentication.SetAuthCookie(model.Email, false);

					// Get the logged-in user
					var loggedInUser = context.Users.SingleOrDefault(u => u.Email.ToLower() == model.Email.ToLower());

					if (loggedInUser != null) {
						// Store the user's first and last names in ViewBag
						ViewBag.UserFirstName = loggedInUser.FirstName;
						ViewBag.UserLastName = loggedInUser.LastName;
					}

					var role = context.Users.Where(u => u.Email.ToLower() == model.Email.ToLower()).Select(u => u.RoleID).FirstOrDefault();
					if (role == 1) {
						return RedirectToAction("Index", "Tenant");
					} else {
						return RedirectToAction("Index", "Apartments");
					}
				}
				ModelState.AddModelError("", "Invalid Email or Password!");
				return View();
			}
		}


		// GET: Signup
		public ActionResult Signup() {
			return View();
		}

		[HttpPost]
		public ActionResult Signup(UserModel userModel) {
			if (ModelState.IsValid) {
				// Map UserModel to User entity and set RoleID to 1
				var newUser = new User {
					FirstName = userModel.FirstName,
					LastName = userModel.LastName,
					Email = userModel.Email,
					Password = userModel.Password,
					PhoneNo = userModel.PhoneNo,
					RoleID = 1 // Setting Role to 1 for all new users
				};

				using (PropertyRentalDBEntities context = new PropertyRentalDBEntities()) {
					context.Users.Add(newUser);
					context.SaveChanges();
				}

				// Redirect to a success page or perform other actions as needed
				return RedirectToAction("Login");
			}

			// If ModelState is not valid, return the view with validation errors
			return View(userModel);
		}

		[HttpPost]
		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Login");
		}

	}
}