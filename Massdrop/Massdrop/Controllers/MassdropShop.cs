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
			this.UserLoggedIn = userLoggedIn;

			InitShop();
		}

		private void InitShop()
		{
			massdropRepo = new MassdropRepository(new ProductOracleDBContext(), new MassdropOracleDBContext(), new DiscussionOracleDBContext());
			OrderRepo = new OrderRepository(new OrderOracleDBContext());
			UserRepo = new UserRepository(new UserOracleDBContext(), new ShippingAddressOracleDBContext());

			foreach (Order order in OrderRepo.OrderRepo.Collection) // Koppelen Users en Orders
			{
				foreach(User user in UserRepo.UserRepo.Collection.Where(x => x.ID == order.User.ID))
				{
					order.User = user;
				}
			}

			foreach (Models.Massdrop massdrop in massdropRepo.MassdropRepo.Collection.Where(x => x.Discussion != null))
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
					foreach(User user in UserRepo.UserRepo.Collection.Where(x => x.ID == subDiscussion.User.ID))
					{
						subDiscussion.User = user;
					}
					AttachUsersToReply(subDiscussion);
				}
			}
		}
	}
}