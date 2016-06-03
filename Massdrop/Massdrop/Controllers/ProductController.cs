using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Massdrop.Models;

namespace Massdrop.Controllers
{
    public class ProductController : Controller
    {
		MassdropShop massdrop;
        public ActionResult Index(string id = "Popular Drops")
        {
			massdrop = (MassdropShop)Session["massdrop"];
			ViewData["CategoryName"] = id;
			return View("ProductView", "_ProductLayout");
        }

		public ActionResult ProductPage(string id)
		{
			ViewData["ProductPageName"] = id;
			ViewData["ProductPageCategory"] = ViewData["CategoryName"];
			return View("ProductPageView", "_ProductLayout");
		}

		public List<Product> Products()
		{
			return new List<Product>(massdrop.massdropRepo.ProductRepo.Collection.Where(x => x.Category.ToString() == ViewData["CategoryName"].ToString()));
		}

		public string GetUserName()
		{
			if (massdrop.UserLoggedIn.Name != null)
				return massdrop.UserLoggedIn.Name;
			else
				return massdrop.UserLoggedIn.UserName;
		}
	}
}