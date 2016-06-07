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
		public GenericRepository<User> UserRepo;
		public GenericRepository<Shipping_Address> AddressRepo;
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
	}
}