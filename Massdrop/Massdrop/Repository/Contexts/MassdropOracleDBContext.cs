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

			foreach(DataRow row in database.SelectData(new OracleCommand("Select * from massdrop")).Rows)
			{
				massdrops.Add(new Models.Massdrop(
					Convert.ToInt32(row["ID"].ToString()),
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
			bool queryCheck = database.InsertData(new OracleCommand("Insert into massdrop(Product_id, First_massdroppedprice, Second_massdroppedprice, startdate, enddate) Values (:Productid, :FirstPrice, :SecondPRice, :SDate, :EDate"),
										new OracleParameter[] 
										{
											new OracleParameter("Productid", source.Product.ID),
											new OracleParameter("FirstPrice", source.First_DroppedPrice),
											new OracleParameter("SecondPrice", source.Second_DroppedPrice),
											new OracleParameter("SDate", source.StartDate),
											new OracleParameter("EDate", source.EndDate)
										});
			source.ID = database.SelectSequenceValue("SEQ_MASSDROP");
			return queryCheck;
		}

		public bool Remove(Models.Massdrop source)
		{
			throw new NotSupportedException(); //Massdrops wont be removed, they'll just expire
		}

		public bool Update(Models.Massdrop source)
		{
			throw new NotSupportedException(); //Massdrops wont be updated once they've been created
		}
	}
}