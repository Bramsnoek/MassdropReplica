using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtendedObservableCollection;
using Massdrop.Models;
using Massdrop.Repository.Interfaces;
using Massdrop.Models.Database;
using System.Data;

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
			throw new NotImplementedException();
		}

		public bool Remove(Shipping_Address source)
		{
			throw new NotImplementedException();
		}

		public bool Update(Shipping_Address source)
		{
			throw new NotImplementedException();
		}
	}
}