﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Massdrop.Repository.Contexts;
using Massdrop.Repository;
using System.Data;
using Massdrop.Models;

namespace Massdrop.Controllers
{
    public class HomeController : Controller
	{
		private MassdropShop massdrop;
		private LogInController logInController;

        public ActionResult Index()
		{
			massdrop = new MassdropShop(null);
			logInController = new LogInController();

			return View();
        }

		public int LogIn(string userName, string password)
		{
			User tempuser = logInController.CheckLogin(password, userName);

			if (tempuser != null)
			{
				massdrop = new MassdropShop(tempuser);
				Session["massdrop"] = massdrop;
				return 1;
			}
			else
				return 0;
		}

		public void CreateUser(string userName, string password)
		{
			logInController.CreateUser(password, userName);
		}

		public void CreateFacebookUser(string userName, string password)
		{

		}
	}
}