using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massdrop.Repository;
using Massdrop.Repository.Contexts;
using Massdrop.Models;

namespace Massdrop.Controllers
{
	public class LogInController
	{
		private UserRepository tempUserRepo;

		public LogInController()
		{
			tempUserRepo = new UserRepository(new UserOracleDBContext(), new ShippingAddressOracleDBContext());
		}

		public User CheckLogin(string password, string userName)
		{
			foreach(User user in tempUserRepo.UserRepo.Collection)
			{
				if(user.UserName == userName && user.Password == password)
				{
					return user;
				}
			}
			return null;
		}

		public void CreateUser(string password, string userName)
		{
			tempUserRepo.UserRepo.Collection.Add(new User(userName, password));	
		}
	}
}