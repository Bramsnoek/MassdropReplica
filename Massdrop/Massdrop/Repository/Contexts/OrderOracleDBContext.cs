using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;
using Massdrop.Models;
using Massdrop.Models.Database;
using Massdrop.Repository.Interfaces;
using System.Data;

namespace Massdrop.Repository.Contexts
{
	public sealed class OrderOracleDBContext : IContext<Order>
	{
		private OracleDB database;

		public OrderOracleDBContext()
		{
			database = new OracleDB();
		}

		public IEnumerable<Order> GetAll()
		{
			List<Order> orders = new List<Order>();

			foreach(DataRow row in database.SelectData(new Oracle.ManagedDataAccess.Client.OracleCommand("Select * From \"ORDER\"")).Rows)
			{
				orders.Add(new Order(
					Convert.ToDateTime(row["DATE"].ToString()),
					Convert.ToDecimal(row["PRICE"].ToString()),
					new User(Convert.ToInt32("SYSTEMUSER_ID")),
					new Payment_Method((PaymentType)Convert.ToInt32(row["PAYMENT_METHOD_ID"].ToString())),
					new Models.Massdrop(Convert.ToInt32(row["MASSDROP_ID"]))));
			}

			return orders;
		}

		public bool Insert(Order source)
		{
			throw new NotImplementedException();
		}

		public bool Remove(Order source)
		{
			throw new NotImplementedException();
		}

		public bool Update(Order source)
		{
			throw new NotImplementedException();
		}
	}
}