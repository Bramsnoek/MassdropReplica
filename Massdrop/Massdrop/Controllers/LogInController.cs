﻿using System;
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

		public void CreateUser(string password, string userName)
		{
			tempUserRepo.UserRepo.Collection.Add(new User(tempUserRepo.UserRepo.Collection.Count, userName, password));	
		}
	}
}