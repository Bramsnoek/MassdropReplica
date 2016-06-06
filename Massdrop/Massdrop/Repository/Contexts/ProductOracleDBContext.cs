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
		private OracleDB database;

		public ProductOracleDBContext()
		{
			database = new OracleDB();
		}

		public IEnumerable<Product> GetAll()
		{
			List<Product> products = new List<Product>();

			foreach(DataRow row in database.SelectData(new OracleCommand("Select * From Product")).Rows)
			{
				products.Add(new Product(
					Convert.ToInt32(row["ID"].ToString()),
					row["NAME"].ToString(),
					Convert.ToDecimal(row["PRICE"].ToString()),
					(ProductCategory)Convert.ToInt32(row["PRODUCT_CATEGORY_ID"]), 
					row["IMAGEURL"].ToString()));
			}

			return products;
		}

		public bool Insert(Product source)
		{
			return database.InsertData(new OracleCommand("Insert Into product (Product_Category_id, Name, Price) Values (:Pid, :Name, :Price)"),
														 new OracleParameter[] 
														 {
															 new OracleParameter("Pid", (int)source.Category),
															 new OracleParameter("Name", source.Name),
															 new OracleParameter("Price", source.Price)
														 });
		}

		public bool Remove(Product source)
		{
			return database.InsertData(new OracleCommand("Drop * From Product Where Id = :Id"),
														 new OracleParameter[]
														 {
															 new OracleParameter("Id", source.ID)
														 });
		}

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
	}
}