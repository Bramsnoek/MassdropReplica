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
	public class MassdropOracleDBContext : IContext<Models.Massdrop>
	{
		private OracleDB database;

		public MassdropOracleDBContext()
		{
			database = new OracleDB();
		}

		public IEnumerable<Models.Massdrop> GetAll()
		{
			List<Models.Massdrop> massdrops = new List<Models.Massdrop>();

			foreach(DataRow row in database.SelectData(new Oracle.ManagedDataAccess.Client.OracleCommand("Select * from massdrop")).Rows)
			{
				massdrops.Add(new Models.Massdrop(
					Convert.ToDecimal(row["FIRST_MASSDROPPEDPRICE"].ToString()),
					Convert.ToDecimal(row["SECOND_MASSDROPPEDPRICE"].ToString()),
					Convert.ToDateTime(row["STARTDATE"].ToString()),
					Convert.ToDateTime(row["ENDDATE"].ToString()),
					new Product(Convert.ToInt32(row["PRODUCT_ID"].ToString())
					)));
			}

			return massdrops;
		}

		public bool Insert(Models.Massdrop source)
		{
			throw new NotImplementedException();
		}

		public bool Remove(Models.Massdrop source)
		{
			throw new NotImplementedException();
		}

		public bool Update(Models.Massdrop source)
		{
			throw new NotImplementedException();
		}
	}
}