using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massdrop.Repository.Interfaces;
using Massdrop.Repository.Contexts;
using Massdrop.Models;

namespace Massdrop.Repository
{
	public class OrderRepository
	{
		public GenericRepository<Order> OrderRepo;
		public OrderRepository(IContext<Order> orderContext)
		{
			OrderRepo = new GenericRepository<Order>(orderContext);

			OrderRepo.EnableListener();
		}
	}
}