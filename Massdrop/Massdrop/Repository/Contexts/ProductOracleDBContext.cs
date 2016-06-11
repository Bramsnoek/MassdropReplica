using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massdrop.Models;
using Massdrop.Repository.Interfaces;
using Massdrop.Models.Database;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Massdrop.Repository.Contexts
{
	public class ProductOracleDBContext : IContext<Product>
	{
		#region Fields
		// The database class instance
		private OracleDB database;
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor will instantiate the database class
		/// </summary>
		public ProductOracleDBContext()
		{
			database = new OracleDB();
		}
		#endregion

		#region Methods

		/// <summary>
		/// This function will get all the products from the database
		/// </summary>
		/// <returns>An IEnumerable of products</returns>
		public IEnumerable<Product> GetAll()
		{
			List<Product> products = new List<Product>();

			foreach(DataRow row in database.SelectData(new OracleCommand("Select * From Product")).Rows)
			{
				products.Add(new Product(
					Convert.ToInt32(row["ID"].ToString()),
					row["NAME"].ToString(),
					Convert.ToDecimal(row["PRICE"].ToString()),
					(ProductCategory)(Convert.ToInt32(row["PRODUCT_CATEGORY_ID"].ToString())-1), 
					row["IMAGEURL"].ToString(), 
					row["DESCRIPTION"].ToString()));
			}

			return products;
		}

		/// <summary>
		/// This function will insert a product into the database
		/// </summary>
		/// <param name="source">The class instance that will be inserted</param>
		/// <returns>If the insertion was succesfull, a true or false</returns>
		public bool Insert(Product source)
		{
			bool queryCheck =  database.InsertData(new OracleCommand("Insert Into product (Product_Category_id, Name, Price) Values (:Pid, :Name, :Price)"),
														 new OracleParameter[] 
														 {
															 new OracleParameter("Pid", (int)source.Category),
															 new OracleParameter("Name", source.Name),
															 new OracleParameter("Price", source.Price)
														 });
			source.ID = database.SelectSequenceValue("SEQ_PRODUCT");
			return queryCheck;
		}

		/// <summary>
		/// This function will remove a product from the database
		/// </summary>
		/// <param name="source">The class instance that will be inserted</param>
		/// <returns>If the removal was succesfull, a true or false</returns>
		public bool Remove(Product source)
		{
			return database.InsertData(new OracleCommand("Drop * From Product Where Id = :Id"),	
														 new OracleParameter[]
														 {
															 new OracleParameter("Id", source.ID)
														 });
		}

		/// <summary>
		/// This function will update a product to the database
		/// </summary>
		/// <param name="source">The class instance that will be inserted</param>
		/// <returns>If the update was succesfull, a true or false</returns>
		public bool Update(Product source)
		{
			return database.InsertData(new OracleCommand("Update Product Set Name = :Name, Price = :Price Where Id = :Id"),
									   new OracleParameter[]
									   {
										   new OracleParameter("Name", source.Name),
										   new OracleParameter("Price", source.Price),
										   new OracleParameter("Id", source.ID)
									   });
		}
		#endregion
	}
}