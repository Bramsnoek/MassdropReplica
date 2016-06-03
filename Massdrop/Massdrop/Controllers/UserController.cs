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

	
    }
}