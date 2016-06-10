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
        public ActionResult Index(string id = "Popular", string search = "")
        {
			massdrop = (MassdropShop)Session["massdrop"];
			ViewData["CategoryName"] = id;
			ViewData["LoggedUser"] = massdrop.UserLoggedIn;

			List<Models.Massdrop> drops = massdrop.massdropRepo.MassdropRepo.Collection.ToList();

			if(search != "")
			{
				ViewData["ProductList"] = drops.FindAll(x => x.Product.Name.Contains(search));
			}
			else if (id == "Popular")
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

			Session["selectedProduct"] = selectedProduct;

			return View("ProductPageView", selectedProduct);
		}

		public void AddComment(string commentText)
		{
			massdrop = (MassdropShop)Session["massdrop"];
			Product currentProduct = (Product)Session["selectedProduct"];

			Discussion newDiscussion = new Discussion(commentText, 0, DateTime.Now, massdrop.UserLoggedIn, currentProduct.Massdrop);
			currentProduct.Massdrop.Discussion.Add(newDiscussion);
		}

		public string JoinDrop()
		{
			massdrop = (MassdropShop)Session["massdrop"];
			Product currentProduct = (Product)Session["selectedProduct"];
			foreach(Order order in massdrop.OrderRepo.OrderRepo.Collection.Where(x => x.Massdrop == currentProduct.Massdrop && x.User == massdrop.UserLoggedIn))
			{
				return "0";
			}

			massdrop.OrderRepo.OrderRepo.Collection.Add(new Order(DateTime.Now, currentProduct.Price, massdrop.UserLoggedIn, new Payment_Method(PaymentType.Paypal), currentProduct.Massdrop));
			return "1";
		}
	}
}