using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Massdrop.Repository.Contexts;
using Massdrop.Repository;
using System.Data;
using ExtendedObservableCollection;
using Massdrop.Models;
using System.Web.Services;
using System.Web.Script.Services;

namespace Massdrop.Controllers
{
    public class HomeController : Controller
	{
		private MassdropShop massdrop;
		private LogInController logInController;

        public ActionResult Index()
		{
			logInController = new LogInController();
			Session["LogInController"] = logInController;

			return View();
        }


		public string LogIn(string userName, string password)
		{
			LogInController loginController = (LogInController)Session["LogInController"];

			User tempuser = loginController.CheckLogin(password, userName);

			if (tempuser != null)
			{
				confirmLogin(tempuser);
				return "1";
			}
			else
				return "0";
		}

		public string FacebookLogIn(string name, string email, string imageurl)
		{
			LogInController loginController = (LogInController)Session["LogInController"];

			User tempuser = loginController.CheckFacebookUser(name, email, imageurl);

			if (tempuser != null)
			{
				confirmLogin(tempuser);
				return "1";
			}
			else
				return "0";
		}

		private void confirmLogin(User user)
		{
			massdrop = new MassdropShop(user.ID);
			Session["LoggedInName"] = user.Name;
			Session["massdrop"] = massdrop;
		}
		
		public void CreateUser(string userName, string password)
		{
			LogInController loginController = (LogInController)Session["LogInController"];
			loginController.CreateUser(password, userName);
		}
	}
}