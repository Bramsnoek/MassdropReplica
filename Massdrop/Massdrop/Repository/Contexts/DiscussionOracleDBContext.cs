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
	public class DiscussionOracleDBContext : IContext<Models.Discussion>
	{
		#region Fields
		// The database class instance
		private OracleDB database;
		#endregion

		#region Constructor
		/// <summary>
		/// This constructor will instantiate the database
		/// </summary>
		public DiscussionOracleDBContext()
		{
			database = new OracleDB();
		}
		#endregion

		#region Methods
		/// <summary>
		/// This function will get all discussions from the database
		/// </summary>
		/// <returns>An IEnumerable of discussions</returns>
		public IEnumerable<Discussion> GetAll()
		{
			List<Discussion> discussions = new List<Discussion>();

			foreach(DataRow row in database.SelectData(new OracleCommand(@"Select * from discussion where DISCUSSION_ID is null")).Rows)
			{
				Discussion tempDiscussion = new Discussion(
					Convert.ToInt32(row["ID"].ToString()),
					row["MESSAGE"].ToString(),
					Convert.ToInt32(row["LIKES"].ToString()),
					Convert.ToDateTime(row["DATE"].ToString()),
					new User(Convert.ToInt32(row["SYSTEMUSER_ID"].ToString())),
					new Models.Massdrop(Convert.ToInt32(row["MASSDROP_ID"].ToString()))
					);

				foreach(DataRow subrow in database.SelectData(new OracleCommand(@"Select * from discussion where DISCUSSION_ID is not null and discussion_id =" + tempDiscussion.ID)).Rows)
				{
					tempDiscussion.Replies.Add(new Discussion(Convert.ToInt32(subrow["ID"].ToString()),
					subrow["MESSAGE"].ToString(),
					Convert.ToInt32(subrow["LIKES"].ToString()),
					Convert.ToDateTime(subrow["DATE"].ToString()),
					new User(Convert.ToInt32(subrow["SYSTEMUSER_ID"].ToString())),
					new Models.Massdrop(Convert.ToInt32(subrow["MASSDROP_ID"].ToString()))
					));
				}
				discussions.Add(tempDiscussion);
			}

			return discussions;
		}

		/// <summary>
		/// This function will insert a discussion into the database
		/// </summary>
		/// <param name="source">The discussion instance that will be inserted</param>
		/// <returns>If the insertion was succesfull, a true or false</returns>
		public bool Insert(Discussion source)
		{
		  bool QueryCheck = database.InsertData(new OracleCommand("Insert Into Discussion(SYSTEMUSER_ID, MASSDROP_ID, MESSAGE) VALUES (:UserId, :MassdropId, :Message)"),
												   new OracleParameter[]
												   {
													   new OracleParameter("UserId", source.User.ID),
													   new OracleParameter("MassdropId", source.Massdrop.ID),
													   new OracleParameter("Message", source.Message)
												   });
			source.ID = database.SelectSequenceValue("SEQ_DISCUSSION");
			return QueryCheck;
		}

		/// <summary>
		/// This function will remove a discussion from the database
		/// </summary>
		/// <param name="source">The discussion instance that will be removed</param>
		/// <returns>If the removal was succesfull, a true or false</returns>
		public bool Remove(Discussion source)
		{
			return database.InsertData(new OracleCommand("Drop * From Discussion where ID = :ID"),
														  new OracleParameter[]
														  {
															  new OracleParameter("ID", source.ID)
														  });
		}

		/// <summary>
		/// This function will update a discussion to the database
		/// </summary>
		/// <param name="source">The discussion instance that will be updated</param>
		/// <returns>If the update was succesfull, a true or false</returns>
		public bool Update(Discussion source)
		{
			return database.InsertData(new OracleCommand("Update Discussion Set Message = :Message where ID = :ID"),
														  new OracleParameter[]
														  {
															  new OracleParameter("Message", source.Message),
															  new OracleParameter("ID", source.ID)
														  });
		}
		#endregion
	}
}