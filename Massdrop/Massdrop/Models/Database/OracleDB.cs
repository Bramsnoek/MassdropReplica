﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace Massdrop.Models.Database
{
	public sealed class OracleDB
	{
		private static string ConnString = "Data Source=localhost/xe;User Id=SYSTEM;Password=Bramsnoek123!;";

		public OracleDB() { }

		public DataTable SelectData(OracleCommand command, OracleParameter[] parameters = null)
		{
			try
			{
				DataTable dataTable = new DataTable();

				using (OracleConnection connection = new OracleConnection())
				{
					connection.ConnectionString = ConnString;
					connection.Open();

					command.Connection = connection;

					if (parameters != null)
						command.Parameters.AddRange(parameters);

					OracleDataReader reader = command.ExecuteReader();
					dataTable.Load(reader);

					return dataTable;
				}
			}
			catch (OracleException e)
			{
				Console.Write(e.Message);
				return null;
			}
		}

		public int SelectSequenceValue(string sequenceName)
		{
			using (OracleConnection connection = new OracleConnection())
			{
				int id = -1;

				connection.ConnectionString = ConnString;
				connection.Open();

				OracleCommand command = new OracleCommand("Select " + sequenceName + ".CURRVAL FROM DUAL");
				command.Connection = connection;

				OracleDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					id = reader.GetInt32(0);
				}

				return id;
			}
		}

		public bool InsertData(OracleCommand command, OracleParameter[] parameters)
		{
			try
			{
				using (OracleConnection connection = new OracleConnection(ConnString))
				{
					connection.Open();

					OracleCommand cmd = connection.CreateCommand();
					OracleTransaction transaction;

					transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

					cmd.Transaction = transaction;

					cmd.CommandText = command.CommandText;

					cmd.BindByName = true;
					cmd.Parameters.AddRange(parameters);

					int check = cmd.ExecuteNonQuery();

					transaction.Commit();


					if (check > 0)
					{
						return true;
					}

					return false;
				}
			}
			catch (OracleException e)
			{
				Console.Write(e.Message);
				return false;
			}
		}

	}
}
