using System.Web.Mvc;
using ExtendedObservableCollection;
using Massdrop.Models;

namespace Massdrop.Controllers
{
	public class HomeController : Controller
	{
		#region Fields
		// The instance to the massdropdrop that holds all the repositories
		private MassdropShop massdrop;

		// The instance to the logincontroller thats used for user related actions
		private LogInController logInController;
		#endregion

		#region ViewMethods
		/// <summary>
		/// This function returns the home page
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			logInController = new LogInController();
			Session["LogInController"] = logInController;

			return View();
		}
		#endregion

		#region Methods
		/// <summary>
		/// This method is called when the user is trying to login, it will return a 1 or a 0 according to the logincheck
		/// </summary>
		/// <param name="userName">The username of the user</param>
		/// <param name="password">The password of the user</param>
		/// <returns></returns>
		public string LogIn(string userName, string password)
		{
			LogInController loginController = (LogInController)Session["LogInController"];

			User tempuser = loginController.CheckLogin(password, userName);

			if (tempuser != null)
			{
				confirmLogin(tempuser);
				return "1";
			}
			return "0";
		}

		/// <summary>
		/// This function is called when the user tries to login with facebook, it wil return a 1 or a 0 according to the check
		/// </summary>
		/// <param name="name">The facebookname of the user</param>
		/// <param name="email">The email of the user</param>
		/// <param name="imageurl">the url to the profileimage of the facebook user</param>
		/// <returns></returns>
		public string FacebookLogIn(string name, string email, string imageurl)
		{
			LogInController loginController = (LogInController)Session["LogInController"];

			User tempuser = loginController.CheckFacebookUser(name, email, imageurl);

			if (tempuser != null)
			{
				confirmLogin(tempuser);
				return "1";
			}

			return "0";
		}

		/// <summary>
		/// This function confirms when a user is logged in, it will instantiate the main class and put it into a session
		/// </summary>
		/// <param name="user"></param>
		private void confirmLogin(User user)
		{
			massdrop = new MassdropShop(user.ID);
			Session["LoggedInName"] = user.Name;
			Session["massdrop"] = massdrop;
		}

		/// <summary>
		/// This function is called when the user wants to create an account
		/// </summary>
		/// <param name="userName">The username of the new account</param>
		/// <param name="password">The password of the new account</param>
		public void CreateUser(string userName, string password)
		{
			LogInController loginController = (LogInController)Session["LogInController"];
			loginController.CreateUser(password, userName);
		}
		#endregion
	}
}