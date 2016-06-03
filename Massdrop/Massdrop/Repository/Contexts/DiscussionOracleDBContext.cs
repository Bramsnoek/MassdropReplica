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
					tempDiscussion.Replies.Add(new Discussion(Convert.ToInt32(row["ID"].ToString()),
					row["MESSAGE"].ToString(),
					Convert.ToInt32(row["LIKES"].ToString()),
					Convert.ToDateTime(row["DATE"].ToString()),
					new User(Convert.ToInt32(row["SYSTEMUSER_ID"].ToString())),
					new Models.Massdrop(Convert.ToInt32(row["MASSDROP_ID"].ToString()))
					));
				}
				discussions.Add(tempDiscussion);
			}

			return discussions;
		}

		public bool Insert(Discussion source)
		{
			throw new NotImplementedException();
		}

		public bool Remove(Discussion source)
		{
			throw new NotImplementedException();
		}

		public bool Update(Discussion source)
		{
			throw new NotImplementedException();
		}
	}
}