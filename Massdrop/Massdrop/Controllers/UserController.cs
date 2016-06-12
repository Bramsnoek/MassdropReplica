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
		#region Fields
		// The instance to the massdropdrop that holds all the repositories
		private MassdropShop massdrop;
		#endregion

		#region ViewMethods
		/// <summary>
		/// This function returns the homepage of the user settings
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			massdrop = (MassdropShop)Session["massdrop"];
			ViewData["User"] = massdrop.UserLoggedIn;
            ViewData["Orders"] = massdrop.OrderRepo.OrderRepo.Collection.ToList().FindAll(x => x.User == massdrop.UserLoggedIn);
			return View("UserView", "_ProductLayout");
		}
		#endregion

		#region Methods
		/// <summary>
		/// This function is used to change the information of the currently logged in user
		/// </summary>
		/// <param name="userName">The username of the user</param>
		/// <param name="name">The name of the user</param>
		/// <param name="email">The email of the user</param>
		/// <param name="password">The password of the user</param>
		/// <param name="oldpassword">The old password of the user, used for authentication purposes</param>
		/// <returns></returns>
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

		/// <summary>
		/// This function adds a shipping address to an user
		/// </summary>
		/// <param name="address">The address of the shipping address</param>
		/// <param name="city">The city of the shipping address</param>
		/// <param name="province">The province of the shipping address</param>
		/// <param name="postalcode">The postalcode of the shipping address</param>
		/// <param name="phonenumber">The phonenumber of the shipping addres</param>
		/// <returns></returns>
		public string AddShippingAddress(string address, string city, string province, string postalcode, string phonenumber)
		{
			massdrop = (MassdropShop)Session["massdrop"];
			return massdrop.UserLoggedIn.AddShippingAddress(address, city, province, postalcode);
		}

		/// <summary>
		/// This removes a shipping address from an user
		/// </summary>
		/// <param name="id">The id of the shipping address</param>
		public void RemoveShippingAddres(int id)
		{
			massdrop = (MassdropShop)Session["massdrop"];
			massdrop.UserLoggedIn.RemoveShippingAddres(id);
		}
		#endregion
	}
}