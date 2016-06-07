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
		private OracleDB database;

		public DiscussionOracleDBContext()
		{
			database = new OracleDB();
		}

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

		public bool Remove(Discussion source)
		{
			return database.InsertData(new OracleCommand("Drop * From Discussion where ID = :ID"),
														  new OracleParameter[]
														  {
															  new OracleParameter("ID", source.ID)
														  });
		}

		public bool Update(Discussion source)
		{
			return database.InsertData(new OracleCommand("Update Discussion Set Message = :Message where ID = :ID"),
														  new OracleParameter[]
														  {
															  new OracleParameter("Message", source.Message),
															  new OracleParameter("ID", source.ID)
														  });
		}
	}
}