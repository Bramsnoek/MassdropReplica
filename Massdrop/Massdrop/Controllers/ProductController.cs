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
        public ActionResult Index(string id = "Popular")
        {
			massdrop = (MassdropShop)Session["massdrop"];
			ViewData["CategoryName"] = id;
			ViewData["Name"] = massdrop.UserLoggedIn.Name;

			List<Models.Massdrop> drops = massdrop.massdropRepo.MassdropRepo.Collection.ToList();

			if (id == "Popular")
			{
				ViewData["ProductList"] = drops;
			}
			else
			{
				ProductCategory category = (ProductCategory)Enum.Parse(typeof(ProductCategory), id);
				foreach (Models.Massdrop drop in drops.Where(x => x.Product.Category == (ProductCategory)Enum.Parse(typeof(ProductCategory), id)))
				{
					Console.WriteLine("test");
				}
				ViewData["ProductList"] = drops.FindAll(x => x.Product.Category == (ProductCategory)Enum.Parse(typeof(ProductCategory), id));
			}


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