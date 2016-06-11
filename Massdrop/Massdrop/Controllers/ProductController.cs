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
		#region Fields
		// The instance to the massdropdrop that holds all the repositories
		private MassdropShop massdrop;
		#endregion

		#region ViewMethods
		/// <summary>
		/// This function returns the homepage of the productoverview
		/// </summary>
		/// <param name="id">The id is the currently selected category, onload this is popular so you get to see all products</param>
		/// <param name="search">This optional parameter is empty by default, by typing in the search bar you can search products</param>
		/// <returns></returns>
		public ActionResult Index(string id = "Popular", string search = "")
        {
			massdrop = (MassdropShop)Session["massdrop"];

			ViewData["CategoryName"] = id;
			ViewData["LoggedUser"] = massdrop.UserLoggedIn;
			ViewData["ProductList"] = massdrop.GetMassdrops(id, search);

			return View("ProductView", "_ProductLayout");
        }

		/// <summary>
		/// This function returns a specific productpage
		/// </summary>
		/// <param name="productName">The name of the product, this is used to show the correct information</param>
		/// <returns></returns>
		public ActionResult ShowProductView(string productName)
		{
			massdrop = (MassdropShop)Session["massdrop"];

			Product selectedProduct = massdrop.GetSelectedProduct(productName);

			Session["selectedProduct"] = selectedProduct;

			return View("ProductPageView", selectedProduct);
		}
		#endregion

		#region Methods
		/// <summary>
		/// This function is used to add a comment to a massdrop
		/// </summary>
		/// <param name="commentText">The text of the comment you want to add</param>
		public void AddComment(string commentText)
		{
			massdrop = (MassdropShop)Session["massdrop"];
			Product currentProduct = (Product)Session["selectedProduct"];

			currentProduct.Massdrop.AddComment(commentText, massdrop.UserLoggedIn);
		}

		/// <summary>
		/// This function is used to join a drop, which means you want to order a product
		/// </summary>
		/// <returns></returns>
		public string JoinDrop()
		{
			massdrop = (MassdropShop)Session["massdrop"];

			return (Convert.ToInt32(massdrop.JoinDrop((Product)Session["selectedProduct"]))).ToString();
		}
		#endregion
	}
}