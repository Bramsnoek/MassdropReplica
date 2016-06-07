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
				ViewData["ProductList"] = drops.FindAll(x => x.Product.Category == (ProductCategory)Enum.Parse(typeof(ProductCategory), id));
			}


			return View("ProductView", "_ProductLayout");
        }

		public ActionResult ShowProductView(string productName)
		{
			massdrop = (MassdropShop)Session["massdrop"];
			Product selectedProduct = massdrop.massdropRepo.ProductRepo.Collection.Single(x => x.Name == productName);

			return View("ProductPageView", selectedProduct);
		}

		public void AddComment(string commentText)
		{
			massdrop = (MassdropShop)Session["massdrop"];
			Discussion newDiscussion = new Discussion()
		}
	}
}