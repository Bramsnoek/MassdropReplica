using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massdrop.Repository;
using Massdrop.Repository.Interfaces;
using ExtendedObservableCollection;
using Massdrop.Models;
using Massdrop.Repository.Contexts;

namespace Massdrop.Controllers
{
	public class MassdropShop : ExtendedNotifyPropertyChanged
	{
		#region Properties
		// The user thats logged in
		public User UserLoggedIn { get; set; }

		// The repository that holds all the massdrop data
		public MassdropRepository massdropRepo { get; set; }

		// The repository that holds all the order data
		public OrderRepository OrderRepo { get; set; }

		// The repository that holds all the user data
		public UserRepository UserRepo { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor will be used to get innitialize this class, it will set the loggedin user and link ALL data
		/// </summary>
		/// <param name="userLoggedInId">The id of the loggedin user</param>
		public MassdropShop(int userLoggedInId)
		{
			InitShop();

			this.UserLoggedIn = UserRepo.UserRepo.Collection.First(x => x.ID == userLoggedInId);
		}
		#endregion

		#region InitMethod
		/// <summary>
		/// This function is called when this class in innitialized, it innitializes all the repositories and binds all the data from the different repositories together using id's
		/// </summary>
		private void InitShop()
		{
			massdropRepo = new MassdropRepository(new ProductOracleDBContext(), new MassdropOracleDBContext(), new DiscussionOracleDBContext());
			OrderRepo = new OrderRepository(new OrderOracleDBContext());
			UserRepo = new UserRepository(new UserOracleDBContext(), new ShippingAddressOracleDBContext());

			foreach (Order order in OrderRepo.OrderRepo.Collection) // Koppelen Users en Orders
			{
				foreach (User user in UserRepo.UserRepo.Collection.Where(x => x.ID == order.User.ID))
				{
					order.User = user;
				}
                foreach(Models.Massdrop massdrop in massdropRepo.MassdropRepo.Collection.Where(x => x.ID == order.Massdrop.ID))
                {
                    order.Massdrop = massdrop;
                }
			}

			foreach (Models.Massdrop massdrop in massdropRepo.MassdropRepo.Collection.Where(x => x.Discussions != null))
			{
				AttachUsersToReply(massdrop.Discussions);
			}

			UserRepo.UserRepo.EnableListener();
			UserRepo.AddressRepo.EnableListener();
			massdropRepo.DiscussionRepo.EnableListener();
			massdropRepo.MassdropRepo.EnableListener();
			massdropRepo.ProductRepo.EnableListener();
			OrderRepo.OrderRepo.EnableListener();
		}

		/// <summary>
		/// This function is a recursive function to bind every discussion and its replies to a user
		/// </summary>
		/// <param name="discussion">The discussion to add users to</param>
		private void AttachUsersToReply(ExtendedBindingList<Discussion> discussion)
		{
			foreach (Discussion sDiscussion in discussion)
			{
				foreach (User user in UserRepo.UserRepo.Collection.Where(x => x.ID == sDiscussion.User.ID))
				{
					sDiscussion.User = user;
				}
				if(sDiscussion.Replies != null)
				{
					AttachUsersToReply(sDiscussion.Replies);
				}
			}
		}
		#endregion

		#region Methods

		/// <summary>
		/// This function returns the homepage of the productoverview
		/// </summary>
		/// <param name="id">The id is the currently selected category, onload this is popular so you get to see all products</param>
		/// <param name="search">This optional parameter is empty by default, by typing in the search bar you can search products</param>
		/// <returns></returns>
		public List<Models.Massdrop> GetMassdrops(string id, string search)
		{
			List<Models.Massdrop> drops = massdropRepo.MassdropRepo.Collection.ToList();

			if (search != "")
			{
				return drops.FindAll(x => x.Product.Name.Contains(search));
			}
			else if (id == "Popular")
			{
				return drops;
			}
			else
			{
				return drops.FindAll(x => x.Product.Category == (ProductCategory)Enum.Parse(typeof(ProductCategory), id));
			}
		}

		/// <summary>
		/// This function returns a specific productpage
		/// </summary>
		/// <param name="productName">The name of the product, this is used to show the correct information</param>
		/// <returns>The selected product</returns>
		public Product GetSelectedProduct(string productname)
		{
			return massdropRepo.ProductRepo.Collection.Single(x => x.Name == productname);
		}

		/// <summary>
		/// This function is used to join a drop, which means you want to order a product
		/// </summary>
		/// <param name="product">The product of the drop the user wants to join</param>
		/// <returns>If the user was allowed to join the drop</returns>
		public bool JoinDrop(Product product)
		{
			foreach (Order order in OrderRepo.OrderRepo.Collection.Where(x => x.Massdrop == product.Massdrop && x.User == UserLoggedIn))
			{
				return false;
			}

			OrderRepo.OrderRepo.Collection.Add(new Order(DateTime.Now, product.Price, UserLoggedIn, new Payment_Method(PaymentType.Paypal), product.Massdrop));
			return true;
		}
		#endregion
	}
}