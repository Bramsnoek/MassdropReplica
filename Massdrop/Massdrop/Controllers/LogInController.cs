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
		#region Fields
		// The instance to the userrepo to get acccess to the user information
		private UserRepository tempUserRepo;
		#endregion

		#region Constructor
		public LogInController()
		{
			tempUserRepo = new UserRepository(new UserOracleDBContext(), new ShippingAddressOracleDBContext());
			tempUserRepo.UserRepo.EnableListener();
			tempUserRepo.AddressRepo.EnableListener();
		}
		#endregion

		#region Methods
		/// <summary>
		/// This function checks if an user exists with the parameters given. This function is used by the homecontroller for the normal login authentication
		/// </summary>
		/// <param name="password">The password of the user</param>
		/// <param name="userName">The username of the user</param>
		/// <returns></returns>
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

		/// <summary>
		/// This function checks if an user exists with the parameters given. This function is used by the homecontroller for the facebook login authentication
		/// </summary>
		/// <param name="name"></param>
		/// <param name="email"></param>
		/// <param name="imageurl"></param>
		/// <returns></returns>
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

		/// <summary>
		/// This function is used by the logincontroller to create a facebook user when the user logs in with facebook for the first time
		/// </summary>
		/// <param name="name">The name of the facebook account</param>
		/// <param name="email">The email of the facebook account</param>
		/// <param name="imageurl">THe imageurl of the profile image of the facebook account</param>
		/// <returns></returns>
		private User createFacebookUser(string name, string email, string imageurl)
		{
			User tempuser = new User(name, email, imageurl);
			tempUserRepo.UserRepo.Collection.Add(tempuser);
			return tempuser;
		}

		/// <summary>
		/// Sometimes the facebook api messes up and the email address couldnt get found, this function makes a placeholder email when this happens
		/// </summary>
		/// <param name="name">The name of the facebook account</param>
		/// <returns></returns>
		private string GetAdjustedEmail(string name)
		{
			return name + "@Placeholder.it";
		}

		/// <summary>
		/// This function is used by the homecontroller to create an user
		/// </summary>
		/// <param name="password">The password of the user</param>
		/// <param name="userName">The username of the user</param>
		public void CreateUser(string password, string userName)
		{
			tempUserRepo.UserRepo.Collection.Add(new User(tempUserRepo.UserRepo.Collection.Count, userName, password));	
		}
		#endregion
	}
}