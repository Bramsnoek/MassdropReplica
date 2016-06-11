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
		#region Fields
		// The database class instance
		private OracleDB database;
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor will instantiate the database class
		/// </summary>
		public MassdropOracleDBContext()
		{
			database = new OracleDB();
		}
		#endregion

		#region Methods
		/// <summary>
		/// This function will get all the massdrops from the database
		/// </summary>
		/// <returns>An IEnumerable of massdrops</returns>
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

		/// <summary>
		/// This function will insert a massdrop into the database
		/// </summary>
		/// <param name="source">The massdrop instance that will be inserted</param>
		/// <returns>If the insertion was successfull, a true or false</returns>
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

		/// <summary>
		/// This function will remove a massdrop from the database
		/// </summary>
		/// <param name="source">The massdrop instance that will be removed</param>
		/// <returns>If the removal was successfull, a true or false</returns>
		public bool Remove(Models.Massdrop source)
		{
			throw new NotSupportedException(); //Massdrops wont be removed, they'll just expire
		}

		/// <summary>
		/// This function will update a massdrop to the database
		/// </summary>
		/// <param name="source">The massdrop instance that will be updated</param>
		/// <returns>If the update was successfull, a true or false</returns>
		public bool Update(Models.Massdrop source)
		{
			throw new NotSupportedException(); //Massdrops wont be updated once they've been created
		}
		#endregion
	}
}