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

		public MassdropShop(int userLoggedInId)
		{
			InitShop();

			this.UserLoggedIn = UserRepo.UserRepo.Collection.First(x => x.ID == userLoggedInId);
		}

		private void InitShop()
		{
			massdropRepo = new MassdropRepository(new ProductOracleDBContext(), new MassdropOracleDBContext(), new DiscussionOracleDBContext());
			OrderRepo = new OrderRepository(new OrderOracleDBContext());
			UserRepo = new UserRepository(new UserOracleDBContext(), new ShippingAddressOracleDBContext());

			foreach (Order order in OrderRepo.OrderRepo.Collection) // Koppelen Users en Orders
			{
				foreach (User user in UserRepo.UserRepo.Collection.Where(x => x.ID == order.User.ID))
				{
					order.User = user;
				}
			}

			foreach (Models.Massdrop massdrop in massdropRepo.MassdropRepo.Collection.Where(x => x.Discussion != null))
			{
				AttachUsersToReply(massdrop.Discussion);
			}

			UserRepo.UserRepo.EnableListener();
			UserRepo.AddressRepo.EnableListener();
			massdropRepo.DiscussionRepo.EnableListener();
			massdropRepo.MassdropRepo.EnableListener();
			massdropRepo.ProductRepo.EnableListener();
			OrderRepo.OrderRepo.EnableListener();
		}

		private void AttachUsersToReply(ExtendedBindingList<Discussion> discussion)
		{
			foreach (Discussion sDiscussion in discussion)
			{
				foreach (User user in UserRepo.UserRepo.Collection.Where(x => x.ID == sDiscussion.User.ID))
				{
					sDiscussion.User = user;
				}
				if(sDiscussion.Replies != null)
				{
					AttachUsersToReply(sDiscussion.Replies);
				}
			}
		}
	}
}