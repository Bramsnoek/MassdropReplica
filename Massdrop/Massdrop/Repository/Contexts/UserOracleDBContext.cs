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
	public sealed class UserOracleDBContext : IContext<User>
	{
		#region Fields
		// The database class
		private OracleDB database;

		#endregion

		#region Constructor
		/// <summary>
		/// This constructor instantiates the database class
		/// </summary>
		public UserOracleDBContext()
		{
			database = new OracleDB();
		}
		#endregion

		#region Methods

		/// <summary>
		/// This function will get all the user data
		/// </summary>
		/// <returns>An IEnumerable of users</returns>
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
					row["PASSWORD"].ToString(), 
					row["IMAGEURL"].ToString()));
			}

			return users;
		}

		/// <summary>
		/// This function will insert a user to the database
		/// </summary>
		/// <param name="source">The user that will be inserted</param>
		/// <returns>If the insert was succesfull, true or false</returns>
		public bool Insert(User source)
		{		
			source.Name = (source.Name == null) ? "Nameless" : source.Name;
			source.UserName = (source.UserName == null) ? "UsernameLess" : source.UserName;
			source.ImageUrl = (source.ImageUrl == null) ? "http://placehold.it/300x300" : source.ImageUrl;

			bool queryCheck = database.InsertData(new OracleCommand("Insert Into SYSTEMUSER (EmailAddress, Name, SystemUserName, Password, IMAGEURL) Values (:EAddress, :Name, :Username, :Password, :ImageUrl)"),
														 new OracleParameter[]
														 {
															 new OracleParameter("EAddress", source.EmailAddress),
															 new OracleParameter("Name", source.Name),
															 new OracleParameter("Username", source.UserName),
															 new OracleParameter("Password", source.Password),
															 new OracleParameter("ImageUrl", source.ImageUrl),
														 });
			source.ID = database.SelectSequenceValue("SEQ_SYSTEMUSER");
			return queryCheck;
		}

		/// <summary>
		/// This function will be used to remove a user
		/// </summary>
		/// <param name="source">The user to be removed</param>
		/// <returns>If the removal was succesfull, a true or false</returns>
		public bool Remove(User source)
		{
			throw new NotSupportedException(); //Users wont't be removed
		}

		/// <summary>
		/// This function will be used to update a user
		/// </summary>
		/// <param name="source">The user to be updated</param>
		/// <returns>If the update was succesfull, a true or false</returns>
		public bool Update(User source)
		{
			return database.InsertData(new OracleCommand("Update SystemUser Set EmailAddress = :EAddress, Name = :Name, SystemUsername = :Username, Password = :Password WHERE ID = :ID"),
														 new OracleParameter[]
														 {
															 new OracleParameter("EAddress", source.EmailAddress),
															 new OracleParameter("Name", source.Name),
															 new OracleParameter("Username", source.UserName),
															 new OracleParameter("Password", source.Password),
															 new OracleParameter("ID", source.ID)
														 });
		}
		#endregion
	}
}