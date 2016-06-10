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
			tempUserRepo.UserRepo.EnableListener();
			tempUserRepo.AddressRepo.EnableListener();
		}

		public User CheckLogin(string password, string userName)
		{
			foreach(User user in tempUserRepo.UserRepo.Collection)
			{
				if(user.EmailAddress == userName && user.Password == password)
				{
					return user;
				}
			}
			return null;
		}

		public User CheckFacebookUser(string name, string email, string imageurl)
		{
			string emailCheck = email;

			if(emailCheck == null)
			{
				emailCheck = GetAdjustedEmail(name);
			}

			foreach(User user in tempUserRepo.UserRepo.Collection.Where(x => x.EmailAddress == emailCheck && x.Name == name))
			{
				return user;
			}
			return createFacebookUser(name, emailCheck, imageurl);
		}

		private User createFacebookUser(string name, string email, string imageurl)
		{
			User tempuser = new User(name, email, imageurl);
			tempUserRepo.UserRepo.Collection.Add(tempuser);
			return tempuser;
		}

		private string GetAdjustedEmail(string name)
		{
			return name + "@Placeholder.it";
		}

		public void CreateUser(string password, string userName)
		{
			tempUserRepo.UserRepo.Collection.Add(new User(tempUserRepo.UserRepo.Collection.Count, userName, password));	
		}
	}
}