using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massdrop.Repository.Interfaces;
using Massdrop.Repository.Contexts;
using Massdrop.Models;

namespace Massdrop.Repository
{
	public class UserRepository
	{
		#region Properties
		// The repository that will get all the user information
		public GenericRepository<User> UserRepo { get; set; }

		// The repository that will get all the address information
		public GenericRepository<Shipping_Address> AddressRepo { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// The userrepository requires two contexts, these will be used to instantiate repositories and get the data. In this constructor that data will be linked to eachother
		/// </summary>
		/// <param name="userContext">The context that will be used to create the userrepository</param>
		/// <param name="addressContext">The context that will be used to create the addressrepository</param>
		public UserRepository(IContext<User> userContext, IContext<Shipping_Address> addressContext)
		{
			UserRepo = new GenericRepository<User>(userContext);
			AddressRepo = new GenericRepository<Shipping_Address>(addressContext);

			foreach(User user in UserRepo.Collection)
			{
				foreach(Shipping_Address address in AddressRepo.Collection.Where(x => x.User.ID == user.ID))
				{
					user.Shipping_Addresses.Add(address); 
					address.User = user;
				}
			}
		}
		#endregion
	}
}