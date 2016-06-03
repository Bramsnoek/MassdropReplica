using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massdrop.Models;
using Massdrop.Repository.Interfaces;
using Massdrop.Models.Database;
using System.Data;

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

			foreach(DataRow row in database.SelectData(new Oracle.ManagedDataAccess.Client.OracleCommand("Select * From Product")).Rows)
			{
				products.Add(new Product(
					row["NAME"].ToString(),
					Convert.ToDecimal(row["PRICE"].ToString()),
					(ProductCategory)Convert.ToInt32(row["PRODUCT_CATEGORY_ID"])));
			}

			return products;
		}

		public bool Insert(Product source)
		{
			throw new NotImplementedException();
		}

		public bool Remove(Product source)
		{
			throw new NotImplementedException();
		}

		public bool Update(Product source)
		{
			throw new NotImplementedException();
		}
	}
}