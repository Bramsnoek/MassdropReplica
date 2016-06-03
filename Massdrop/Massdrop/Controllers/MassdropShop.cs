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
		public User UserLoggedIn { get; set; }
		public MassdropRepository massdropRepo { get; set; }
		public OrderRepository OrderRepo { get; set; }
		public UserRepository UserRepo { get; set; }

		public MassdropShop(User userLoggedIn)
		{
			//this.UserLoggedIn = userLoggedIn;

			InitShop();
		}

		private void InitShop()
		{
			massdropRepo = new MassdropRepository(new ProductOracleDBContext(), new MassdropOracleDBContext(), new DiscussionOracleDBContext());
			OrderRepo = new OrderRepository(new OrderOracleDBContext());
			UserRepo = new UserRepository(new UserOracleDBContext(), new ShippingAddressOracleDBContext());

			foreach (Order order in OrderRepo.OrderRepo.Collection) // Koppelen Users en Orders
			{
				order.User = UserRepo.UserRepo.Collection.Single(x => x.ID == order.User.ID);
			}

			foreach (Models.Massdrop massdrop in massdropRepo.MassdropRepo.Collection)
			{
				AttachUsersToReply(massdrop.Discussion);
			}
		}

		private void AttachUsersToReply(Discussion discussion)
		{
			if (discussion.Replies != null)
			{
				foreach (Discussion subDiscussion in discussion.Replies)
				{
					subDiscussion.User = UserRepo.UserRepo.Collection.Single(x => x.ID == subDiscussion.User.ID);

					AttachUsersToReply(subDiscussion);
				}
			}
		}
	}
}