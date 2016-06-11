using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;
using Massdrop.Models;
using Massdrop.Models.Database;
using Massdrop.Repository.Interfaces;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Massdrop.Repository.Contexts
{
	public sealed class OrderOracleDBContext : IContext<Order>
	{
		#region Fields
		// The database class instance
		private OracleDB database;
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor will instantiate the database class
		/// </summary>
		public OrderOracleDBContext()
		{
			database = new OracleDB();
		}
		#endregion

		#region Methods
		/// <summary>
		/// This function will get all the orders from the database
		/// </summary>
		/// <returns>An IEnumerable of orders</returns>
		public IEnumerable<Order> GetAll()
		{
			List<Order> orders = new List<Order>();

			foreach(DataRow row in database.SelectData(new OracleCommand("Select * From \"ORDER\"")).Rows)
			{
				orders.Add(new Order(
					Convert.ToDateTime(row["DATE"].ToString()),
					Convert.ToDecimal(row["PRICE"].ToString()),
					new User(Convert.ToInt32(row["SYSTEMUSER_ID"].ToString())),
					new Payment_Method((PaymentType)Convert.ToInt32(row["PAYMENT_METHOD_ID"].ToString())),
					new Models.Massdrop(Convert.ToInt32(row["MASSDROP_ID"].ToString()))));
			}

			return orders;
		}

		/// <summary>
		/// This function inserts an order into the database
		/// </summary>
		/// <param name="source">The order that will be inserted</param>
		/// <returns>If the insertion was sucessfull, a true or false</returns>
		public bool Insert(Order source)
		{
			bool queryCheck = database.InsertData(new OracleCommand("Insert into \"ORDER\"(Systemuser_id, Payment_method_id, Massdrop_id, Price) Values(:UserId, :MethodId, :MassdropId, :Price)"),
														 new OracleParameter[]
														 {
															 new OracleParameter("UserId", source.User.ID),
															 new OracleParameter("MethodId", Convert.ToInt32(source.Payment_Method.Type)),
															 new OracleParameter("MassdropId", source.Massdrop.ID),
															 new OracleParameter("Price", source.Massdrop.Product.Price)
														 });
			source.ID = database.SelectSequenceValue("SEQ_ORDER");
			return queryCheck;
		}

		/// <summary>
		/// This function removes an order from the database
		/// </summary>
		/// <param name="source">The order that will be removed</param>
		/// <returns>If the removal was sucessfull, a true or false</returns>
		public bool Remove(Order source)
		{
			throw new NotSupportedException(); //Orders wont be removed they'll just expire (We need this to get the history for example)
		}

		/// <summary>
		/// This function updates an order to the database
		/// </summary>
		/// <param name="source">The order that will be updated</param>
		/// <returns>If the update was sucessfull, a true or false</returns>
		public bool Update(Order source)
		{
			return database.InsertData(new OracleCommand("UPDATE ORDER SET ID = :ID where ID = :oldID"),
									   new OracleParameter[]
									   {
										   new OracleParameter("ID", source.ID),
										   new OracleParameter("oldID", source.ID)
									   });
		}
		#endregion
	}
}