using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.Loaders;
using DBManager.Default.Tree;
using DBManager.SqlServer.Provider;

namespace DBManager.SqlServer.Loader
{
	[DataContract(Name = "MsSqlLoader")]
	public class MsSqlObjectLoader : ObjectLoader
	{

		public MsSqlObjectLoader(ConnectionData connection) : base(connection, new MsSqlScriptProvider())
		{
		}

		public override Task LoadChildrenAsync(DbObject obj)
		{
			return Task.Run(() =>
			{
				foreach (var child in Hierarchy.HierarchyObject.GetChildTypes(obj.Type))
				{
					SetChildrens(obj, child);
				}
			});
		}

		public override Task LoadChildrenAsync(DbObject obj, DbEntityType childType)
		{
			if (!Hierarchy.HierarchyObject.GetChildTypes(obj.Type).Contains(childType))
				throw new ArgumentException();

			return Task.Run(() => SetChildrens(obj, childType));
		}

		public override Task LoadPropertiesAsync(DbObject obj)
		{
			return Task.Run(() => SetObjectProperties(obj));
		}

		private void SetChildrens(DbObject target, DbEntityType childrenType)
		{
			if (!(Connection.GetConnection() is SqlConnection))
				throw new InvalidCastException();


			using (SqlConnection connection = (SqlConnection)Connection.GetConnection())
			{
				connection.Open();

				List<DbObject> childs = new List<DbObject>();

				string query = _provider.GetLoadNameScript(target.Type, childrenType);

				SqlCommand command = new SqlCommand(query, connection);

				foreach (var param in _provider.GetChildrenLoadParameters(target, childrenType))
				{
					command.Parameters.Add(param);
				}

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						DbObject child = (DbEntityFactory.ObjectCreator.Create(reader, childrenType));
						if (!target.AddChild(child))
						{
							throw new ArgumentException();
						}
						childs.Add(child);
					}
				}

				foreach (var child in childs)
				{
					if (child.CanHaveDefinition)
					{
						query = _provider.GetDefinitionScript();
						SqlCommand descCommand = new SqlCommand(query, connection);
						foreach (var param in _provider.GetDefinitionParameters(child))
						{
							descCommand.Parameters.Add(param);
						}
						using (SqlDataReader descReader = descCommand.ExecuteReader())
						{

							StringBuilder defText = new StringBuilder();
							while (descReader.Read())
							{
								defText.Append(descReader.GetString(0));
							}

							child.Definition = defText.ToString();
						}
					}
				}

			}
		}

		private void SetObjectProperties(DbObject obj)
		{
			if (!(Connection.GetConnection() is SqlConnection))
				throw new InvalidCastException();
			using (SqlConnection connection = (SqlConnection)Connection.GetConnection())
			{
				connection.Open();

				string query = _provider.GetPropertiesScript(obj);
				SqlCommand command = new SqlCommand(query, connection);

				foreach (var param in _provider.GetLoadPropertiesParameters(obj))
				{
					command.Parameters.Add(param);
				}

				SqlDataReader reader = command.ExecuteReader();

				reader.Read();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					if (reader.HasRows)
						obj.Properties.Add(reader.GetName(i), reader.GetValue(i));
				}
			}

			obj.IsPropertyLoaded = true;
		}

		[OnDeserializing]
		private void DeserializationInitializer(StreamingContext ctx)
		{
			_provider = new MsSqlScriptProvider();
		}
	}
}
