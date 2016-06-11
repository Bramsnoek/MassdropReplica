using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;
using Massdrop.Models;
using Massdrop.Repository.Interfaces;
using Massdrop.Models.Database;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Massdrop.Repository.Contexts
{
	public class ShippingAddressOracleDBContext : IContext<Shipping_Address>
	{
		#region Fields
		// The database class instance
		private OracleDB database;
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor will instantiate the database class
		/// </summary>
		public ShippingAddressOracleDBContext()
		{
			database = new OracleDB();
		}
		#endregion

		#region Methods

		/// <summary>
		/// This function will get all the shipping_addresses from the database
		/// </summary>
		/// <returns>An IEnumerable of Shipping_Addresses</returns>
		public IEnumerable<Shipping_Address> GetAll()
		{
			List<Shipping_Address> addresses = new List<Shipping_Address>();

			foreach (DataRow row in database.SelectData(new Oracle.ManagedDataAccess.Client.OracleCommand("Select * From Shipping_Address")).Rows)
			{
				if (row["PHONENUMBER"] == DBNull.Value)
				{
					addresses.Add(new Shipping_Address(
						row["ADDRESS"].ToString(),
						row["CITY"].ToString(),
						row["PROVINCE"].ToString(),
						row["POSTALCODE"].ToString(),
						new User(Convert.ToInt32(row["SYSTEMUSER_ID"].ToString()))
						));
				}
				else
				{
					addresses.Add(new Shipping_Address(
						row["ADDRESS"].ToString(),
						row["CITY"].ToString(),
						row["PROVINCE"].ToString(),
						row["POSTALCODE"].ToString(),
						new User(Convert.ToInt32(row["SYSTEMUSER_ID"].ToString())),
						Convert.ToInt64(row["PHONENUMBER"].ToString())
						));
				}
			}

			return addresses;
		}

		/// <summary>
		/// This function will insert a shipping_address into the database
		/// </summary>
		/// <param name="source">The shipping_address instance to be updated</param>
		/// <returns>If the insertion was succesfull, a true or false</returns>
		public bool Insert(Shipping_Address source)
		{
			return database.InsertData(new OracleCommand("Insert into Shipping_Address (Systemuser_id, Address, City, Province, Postalcode, Phonenumber) Values (:UserId, :Address, :City, :Province, :Postalcode, :PhoneNumber)"),
														 new OracleParameter[]
														 {
															 new OracleParameter("UserId", source.User.ID),
															 new OracleParameter("Address", source.Address),
															 new OracleParameter("City", source.City),
															 new OracleParameter("Province", source.Province),
															 new OracleParameter("Postalcode", source.PostalCode),
															 new OracleParameter("PhoneNumber", (source.PhoneNumber == 0) ? source.PhoneNumber : 0)
														 });
		}

		/// <summary>
		/// This function will remove a shipping_address from the database
		/// </summary>
		/// <param name="source">The shipping_address instance to be updated</param>
		/// <returns>If the removal was succesfull, a true or false</returns>
		public bool Remove(Shipping_Address source)
		{
			bool queryCheck = database.InsertData(new OracleCommand("Drop * From Shipping__Address Where Id = :Id"),
									   new OracleParameter[]
									   {
										   new OracleParameter("Id", source.ID)
									   });
			source.ID = database.SelectSequenceValue("SEQ_SHIPPING_ADDRESS");
			return queryCheck;
		}


		/// <summary>
		/// This function will update a shipping_address to the database
		/// </summary>
		/// <param name="source">The shipping_address instance to be updated</param>
		/// <returns>If the update was succesfull, a true or false</returns>
		public bool Update(Shipping_Address source)
		{
			return database.InsertData(new OracleCommand("Update Shipping_address Set Systemuser_id = :UserId, Address = :Address, City = :City, Province = :Province, Postalcode = :Postalcode, Phonenumber = :PhoneNumber Where Id = :Id"),
												new OracleParameter[]
														 {
															 new OracleParameter("Id", source.ID),
															 new OracleParameter("UserId", source.User.ID),
															 new OracleParameter("Address", source.Address),
															 new OracleParameter("City", source.City),
															 new OracleParameter("Province", source.Province),
															 new OracleParameter("Postalcode", source.PostalCode),
															 new OracleParameter("PhoneNumber", (source.PhoneNumber == 0) ? source.PhoneNumber : 0)
														 });
		}
		#endregion
	}
}