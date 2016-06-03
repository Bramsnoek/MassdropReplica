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
	public sealed class UserOracleDBContext : IContext<User>
	{
		private OracleDB database;

		public UserOracleDBContext()
		{
			database = new OracleDB();
		}

		public IEnumerable<User> GetAll()
		{
			List<User> users = new List<User>();

			foreach(DataRow row in database.SelectData(new Oracle.ManagedDataAccess.Client.OracleCommand("Select * From SystemUser")).Rows)
			{
				users.Add(new User(
					row["EMAILADDRESS"].ToString(),
					row["NAME"].ToString(),
					row["SYSTEMUSERNAME"].ToString(),
					row["PASSWORD"].ToString()));
			}

			return users;
		}

		public bool Insert(User source)
		{
			throw new NotImplementedException();
		}

		public bool Remove(User source)
		{
			throw new NotImplementedException();
		}

		public bool Update(User source)
		{
			throw new NotImplementedException();
		}
	}
}