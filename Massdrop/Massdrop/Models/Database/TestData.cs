using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Massdrop.Models
{
	/// <summary>
	/// This static class contains testdata used for unit testing the repositories
	/// </summary>
	public static class TestData
	{
		#region User Data

		/// <summary>
		/// This function will get all the testdata for shipping_addresses
		/// </summary>
		/// <returns>A list of shipping_addresses</returns>
		public static List<Shipping_Address> GetShippingAddresses()
		{
			List<Shipping_Address> addresses = new List<Shipping_Address>();

			addresses.Add(new Shipping_Address("Lithsedijk 33", "Lith", "Noord-Brabant", "5397EA", GetUsers()[0],0636489308));
			addresses.Add(new Shipping_Address("Bamistad 44", "Bamistad", "Noord-Bami", "1923EB", GetUsers()[0], 0547382910));
			addresses.Add(new Shipping_Address("Nasistad 44", "Nasistad", "Noord-Nasi", "4193EL", GetUsers()[0], 0584930291));
			addresses.Add(new Shipping_Address("Dorpstraat 199", "Roosendaal", "Noord-Brabant", "4732EL", GetUsers()[0], 0699887769));
			addresses.Add(new Shipping_Address("Venraaiseweg 44", "Hoorst", "Limburg", "4949EB", GetUsers()[0], 1293139123));

			return addresses;
		}

		/// <summary>
		/// This function will get all the testdata for Users
		/// </summary>
		/// <returns>A list of Users</returns>
		public static List<User> GetUsers()
		{
			List<User> users = new List<User>();

			users.Add(new User(1, "Bramsnoeklith@hotmail.com", "Bram", "Bramsnoek123", "Bramsnoek123", null));
			users.Add(new User(2, "Ferryjongmans@hotmail.com", "Ferry", "Ferry123", "Bramsnoek123", null));
			users.Add(new User(3, "Ruud@hotmail.com", "Ruud", "Ruud123", "Ruud123", null));
			users.Add(new User(4, "Sjoerd@hotmail.com", "Sjoerd", "Sjoerd123", "Sjoerd123", null));
			users.Add(new User(5, "Martijn@hotmail.com", "Martijn", "Martijn123", "Martijn123", null));
			users.Add(new User(6));
			users.Add(new User(7, "Bramsnoeklith@live.nl", "Bramsnoek123"));
			users.Add(new User("Ruudje", "Ruudje@Ruudje.nl", "Imageurl"));

			return users;
		}
		#endregion

		#region Massdrop Data

		/// <summary>
		/// This function will get all the testdata for Products
		/// </summary>
		/// <returns>A list of Products</returns>
		public static List<Product> GetProducts()
		{
			List<Product> products = new List<Product>();

			products.Add(new Product(1, "Laptop", 399.99m, ProductCategory.Tech));
			products.Add(new Product(2, "Knife", 45.00m, ProductCategory.EverydayCarry));
			products.Add(new Product(3, "Heating Pot", 99.99m, ProductCategory.Ultralight));
			products.Add(new Product(4, "Rc Car", 199.99m, ProductCategory.RCCars));
			products.Add(new Product(6, "Neat", 123, ProductCategory.Auto, "Image", "Its very nice!"));

			return products;
		}

		/// <summary>
		/// This function will get all the testdata for Discussions
		/// </summary>
		/// <returns>A list of Discussions</returns>
		public static List<Discussion> GetDiscussions()
		{
			List<Discussion> discussions = new List<Discussion>();

			discussions.Add(new Discussion(1, "Ferry is awesome!", 0, DateTime.Now, GetUsers()[0], GetMassdrops()[0] , new List<Discussion> { new Discussion(1, "Ben ik het mee eens!", 0, DateTime.Now.AddDays(1), GetUsers()[1], GetMassdrops()[0]) }));
			discussions.Add(new Discussion("Sjoerd is awesome!", 13, DateTime.Now.AddDays(10), GetUsers()[2], GetMassdrops()[1], new List<Discussion>
			{
				new Discussion("Ben ik het mee eens!", 0, DateTime.Now.AddDays(11), GetUsers()[0], GetMassdrops()[1], new List<Discussion>
				{
					new Discussion("Ben ik het ook mee eens!", 0, DateTime.Now.AddDays(12), GetUsers()[4], GetMassdrops()[1], new List<Discussion>
					{
						new Discussion(1, "Ben ik het niet mee eens! Foei!", 13, DateTime.Now.AddDays(13), GetUsers()[3], GetMassdrops()[1])
					})
				})
			}));

			return discussions;
		}

		/// <summary>
		/// This function will get all the testdata for Massdrops
		/// </summary>
		/// <returns>A list of Massdrops</returns>
		public static List<Massdrop> GetMassdrops()
		{
			List<Massdrop> massdrops = new List<Massdrop>();

			massdrops.Add(new Massdrop(1, 350.00m, 330.00m, DateTime.Now, DateTime.Now.AddDays(50), GetProducts()[0]));
			massdrops.Add(new Massdrop(2, 40.00m, 35.00m, DateTime.Now, DateTime.Now.AddDays(90), GetProducts()[1]));
			massdrops.Add(new Massdrop(3, 90.00m, 80.00m, DateTime.Now, DateTime.Now.AddDays(90), GetProducts()[2]));
			massdrops.Add(new Massdrop(4, 180.00m, 160.00m, DateTime.Now, DateTime.Now.AddDays(90), GetProducts()[3]));

			return massdrops;
		}
		#endregion

		#region Order Data

		/// <summary>
		/// This function will get all the testdata for PaymentMethods
		/// </summary>
		/// <returns>A list of PaymentMethods</returns>
		public static List<Payment_Method> GetPaymentMethods()
		{
			List<Payment_Method> methods = new List<Payment_Method>();

			methods.Add(new Payment_Method(PaymentType.Ideal));
			methods.Add(new Payment_Method(PaymentType.Paypal));

			return methods;
		}

		/// <summary>
		/// This function will get all the testdata for Orders
		/// </summary>
		/// <returns>A list of Orders</returns>
		public static List<Order> GetOrders()
		{
			List<Order> orders = new List<Order>();

			orders.Add(new Order(DateTime.Now, GetMassdrops()[0].Product.Price, GetUsers()[0], GetPaymentMethods()[0], GetMassdrops()[0]));
			orders.Add(new Order(DateTime.Now, GetMassdrops()[0].Product.Price, GetUsers()[0], GetPaymentMethods()[1], GetMassdrops()[2]));
			orders.Add(new Order(DateTime.Now, GetMassdrops()[0].Product.Price, GetUsers()[1], GetPaymentMethods()[0], GetMassdrops()[0]));
			orders.Add(new Order(DateTime.Now, GetMassdrops()[0].Product.Price, GetUsers()[1], GetPaymentMethods()[1], GetMassdrops()[3]));

			return orders;

		}
		#endregion
	}
}