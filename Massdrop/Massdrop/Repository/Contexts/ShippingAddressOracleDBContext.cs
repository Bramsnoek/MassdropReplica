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
		private OracleDB database;

		public ShippingAddressOracleDBContext()
		{
			database = new OracleDB();
		}

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
						Convert.ToInt32(row["PHONENUMBER"].ToString())
						));
				}
			}

			return addresses;
		}

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

		public bool Remove(Shipping_Address source)
		{
			return database.InsertData(new OracleCommand("Drop * From Shipping__Address Where Id = :Id"),
									   new OracleParameter[]
									   {
										   new OracleParameter("Id", source.ID)
									   });
		}

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
	}
}