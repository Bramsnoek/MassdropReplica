using Massdrop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Massdrop.Controllers
{
	public class UserController : Controller
	{
		private MassdropShop massdrop;

		public ActionResult Index()
		{
			massdrop = (MassdropShop)Session["massdrop"];
			return View("UserView", "_ProductLayout");
		}

		public string ChangeUserInfo(string userName, string name, string email, string password, string oldpassword)
		{
			massdrop = (MassdropShop)Session["massdrop"];

			if (massdrop.UserLoggedIn.Password == oldpassword)
			{
				massdrop.UserLoggedIn.ChangeUserInfo(userName, name, email, password);
				return "1";
			}
			else
			{
				return "0" ;
			}
		}
	}
}