﻿using System;
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

			foreach (DataRow row in database.SelectData(new OracleCommand("Select * From SystemUser")).Rows)
			{
				users.Add(new User(
					Convert.ToInt32(row["ID"].ToString()),
					row["EMAILADDRESS"].ToString(),
					row["NAME"].ToString(),
					row["SYSTEMUSERNAME"].ToString(),
					row["PASSWORD"].ToString()));
			}

			return users;
		}

		public bool Insert(User source)
		{		
			source.Name = (source.Name == null) ? "Nameless" : source.Name;
			source.UserName = (source.UserName == null) ? "UsernameLess" : source.UserName;

			bool queryCheck = database.InsertData(new OracleCommand("Insert Into SYSTEMUSER (EmailAddress, Name, SystemUserName, Password) Values (:EAddress, :Name, :Username, :Password)"),
														 new OracleParameter[]
														 {
															 new OracleParameter("EAddress", source.EmailAddress),
															 new OracleParameter("Name", source.Name),
															 new OracleParameter("Username", source.UserName),
															 new OracleParameter("Password", source.Password)
														 });
			source.ID = database.SelectSequenceValue("SEQ_SYSTEMUSER");
			return queryCheck;
		}

		public bool Remove(User source)
		{
			throw new NotSupportedException(); //Users wont't be removed
		}

		public bool Update(User source)
		{
			return database.InsertData(new OracleCommand("Update User Set EmailAddress = :EAddress, Name = :Name, SystemUsername = :Username, Password = :Password"),
														 new OracleParameter[]
														 {
															 new OracleParameter("EAddress", source.EmailAddress),
															 new OracleParameter("Name", source.Name),
															 new OracleParameter("Username", source.UserName),
															 new OracleParameter("Password", source.Password)
														 });
		}
	}
}