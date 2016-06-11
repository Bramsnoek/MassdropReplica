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
		#region Properties
		// This repository will be used to get the orderdata
		public GenericRepository<Order> OrderRepo { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor requires one ordercontext, this will be used to instantiate the orderrepo and get the the order data
		/// </summary>
		/// <param name="orderContext">The context that will be used to instantiate the orderrepo</param>
		public OrderRepository(IContext<Order> orderContext)
		{
			OrderRepo = new GenericRepository<Order>(orderContext);
		}
		#endregion
	}
}