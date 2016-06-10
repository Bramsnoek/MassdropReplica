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
			ViewData["User"] = massdrop.UserLoggedIn;
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
			return "0";
		}

		public string AddShippingAddress(string address, string city, string province, string postalcode, string phonenumber)
		{
			massdrop = (MassdropShop)Session["massdrop"];
			massdrop.UserLoggedIn.Shipping_Addresses.Add(new Shipping_Address(address, city, province, postalcode, massdrop.UserLoggedIn));
			return "1";
		}

		public void RemoveShippingAddres(int id)
		{
			massdrop = (MassdropShop)Session["massdrop"];
			massdrop.UserLoggedIn.Shipping_Addresses.Remove(massdrop.UserLoggedIn.Shipping_Addresses.First(x => x.ID == id));
		}
	}
}