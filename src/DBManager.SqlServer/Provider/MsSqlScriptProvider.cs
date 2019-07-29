using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBManager.Default;
using DBManager.Default.Providers;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Provider
{
	public class MsSqlScriptProvider : ScriptProvider
	{
		public override string GetLoadNameScript(DbEntityType parentType, DbEntityType childType)
		{
			switch (childType)
			{
				case DbEntityType.Schema:
					return $"SELECT SCHEMA_NAME AS '{Constants.NameProperty}' FROM INFORMATION_SCHEMA.SCHEMATA";

				case DbEntityType.Table:
				case DbEntityType.View:
					return $"SELECT TABLE_NAME AS '{Constants.NameProperty}' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = {Constants.TypeParameter} AND TABLE_SCHEMA = {Constants.SchemaNameParameter}";

				case DbEntityType.Function:
				case DbEntityType.Procedure:
					return $"SELECT ROUTINE_NAME AS '{Constants.NameProperty}' FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = {Constants.TypeParameter} AND ROUTINE_SCHEMA = {Constants.SchemaNameParameter}";

				case DbEntityType.Constraint:
					return
						($"SELECT CONSTRAINT_NAME AS {Constants.NameProperty} FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE ='CHECK' " +
						$"AND TABLE_SCHEMA = {Constants.SchemaNameParameter} AND TABLE_NAME = {Constants.NameParameter}");

				case DbEntityType.Column:
					switch (parentType)
					{
						case DbEntityType.Table:
							return ($"SELECT col.name AS'{Constants.NameProperty}', " +
									$"TYPE_NAME(col.system_type_id) AS '{Constants.TypeNameProperty}', " +
									$"col.max_length AS '{Constants.MaxLengthProperty}', " +
									$"col.precision AS '{Constants.PrecisionProperty}', " +
									$"col.scale AS '{Constants.ScaleProperty}'  " +
									$"FROM sys.columns AS col " +
									$"JOIN INFORMATION_SCHEMA.COLUMNS AS inf ON col.name=inf.COLUMN_NAME AND col.object_id=OBJECT_ID(inf.TABLE_CATALOG+'.'+inf.TABLE_SCHEMA+'.'+inf.TABLE_NAME) where inf.TABLE_NAME={Constants.NameParameter} AND inf.TABLE_SCHEMA={Constants.SchemaNameParameter}");
						case DbEntityType.View:
							return ($"SELECT col.name AS'{Constants.NameProperty}', " +
								   $"TYPE_NAME(col.system_type_id) AS  '{Constants.TypeNameProperty}', " +
								   $"col.max_length AS '{Constants.MaxLengthProperty}', " +
								   $"col.precision AS '{Constants.PrecisionProperty}', " +
								   $"col.scale AS '{Constants.ScaleProperty}'  " +
								   $"FROM sys.columns AS col " +
								   $"JOIN INFORMATION_SCHEMA.VIEW_COLUMN_USAGE AS inf ON col.name=inf.COLUMN_NAME AND col.object_id=OBJECT_ID(inf.TABLE_CATALOG+'.'+inf.TABLE_SCHEMA+'.'+inf.TABLE_NAME) WHERE inf.VIEW_NAME = {Constants.NameParameter} AND inf.VIEW_SCHEMA={Constants.SchemaNameParameter}");

						default:
							throw new ArgumentException();
					}


				case DbEntityType.Trigger:
					return $"SELECT name AS '{Constants.NameProperty}' FROM sys.triggers WHERE parent_id = COALESCE (OBJECT_ID({Constants.FullParentNameParameter}),0)";

				case DbEntityType.Parameter:
					return ($"SELECT  name AS'{Constants.NameProperty}', " +
							$"TYPE_NAME(system_type_id) AS '{Constants.TypeNameProperty}', " +
							$"max_length AS '{Constants.MaxLengthProperty}', " +
							$"precision AS '{Constants.PrecisionProperty}', " +
							$"scale AS '{Constants.ScaleProperty}' " +
							$"FROM sys.parameters WHERE object_id = OBJECT_ID({Constants.FullParentNameParameter}) AND parameter_id != 0");

				case DbEntityType.Key:
					return $"SELECT DISTINCT CONSTRAINT_NAME AS '{Constants.NameProperty}' FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_SCHEMA = {Constants.SchemaNameParameter} AND TABLE_NAME = {Constants.NameParameter}";

				case DbEntityType.Index:
					return
						$"SELECT name AS '{Constants.NameProperty}' FROM sys.indexes WHERE is_hypothetical = 0 AND index_id != 0 AND object_id = OBJECT_ID({Constants.FullParentNameParameter})";

				case DbEntityType.Database:
					return $"SELECT name  AS '{Constants.NameProperty}' FROM sys.databases";

				default:
					throw new ArgumentException();

			}
		}

		public override string GetDefinitionScript()
		{
			return $"EXEC sp_helptext {Constants.NameParameter}";
		}

		public override IEnumerable<IDbDataParameter> GetDefinitionParameters(DbObject obj)
		{
			yield return new SqlParameter(Constants.NameParameter, $"{obj.SchemaName}.{obj.Name}");
		}

		public override IEnumerable<IDbDataParameter> GetChildrenLoadParameters(DbObject obj, DbEntityType childType)
		{
			List<IDbDataParameter> parameters = new List<IDbDataParameter>();
			switch (childType)
			{

				case DbEntityType.Table:
					parameters.Add(new SqlParameter(Constants.TypeParameter, "BASE TABLE"));
					parameters.Add(new SqlParameter(Constants.SchemaNameParameter, obj.SchemaName));
					break;

				case DbEntityType.View:
				case DbEntityType.Function:
				case DbEntityType.Procedure:
					parameters.Add(new SqlParameter(Constants.TypeParameter, childType.ToString().ToUpper()));
					parameters.Add(new SqlParameter(Constants.SchemaNameParameter, obj.SchemaName));
					break;

				case DbEntityType.Column:
				case DbEntityType.Constraint:
				case DbEntityType.Key:
					parameters.Add(new SqlParameter(Constants.SchemaNameParameter, obj.SchemaName));
					parameters.Add(new SqlParameter(Constants.NameParameter, obj.Name));
					break;

				case DbEntityType.Trigger:
				case DbEntityType.Parameter:
				case DbEntityType.Index:
					parameters.Add(new SqlParameter(Constants.FullParentNameParameter, obj.FullName.ToString()));
					break;
			}

			return parameters;
		}

		public override string GetPropertiesScript(DbObject obj)
		{
			switch (obj.Type)
			{
				case DbEntityType.Server:
					return ($"SELECT serv.product AS '{Constants.ProductProperty}', " +
							$"@@VERSION AS '{Constants.ServerVersionProperty}', " +
							$"serv.provider AS '{Constants.ProviderProperty}', " +
							$"serv.data_source AS '{Constants.DataSourceProperty}', " +
							$"serv.connect_timeout AS '{Constants.ConnectTimeoutProperty}', " +
							$"serv.query_timeout AS '{Constants.QueryTimeoutProperty}', " +
							$"serv.is_linked AS '{Constants.IsLinkedProperty}', " +
							$"serv.is_remote_login_enabled AS '{Constants.IsRemoteLoginEnabledProperty}', " +
							$"serv.is_data_access_enabled AS '{Constants.IsDataAccessEnabledProperty}', " +
							$"serv.modify_date AS '{Constants.ModifyDateProperty}', " +
							$"serv.is_system AS '{Constants.IsSystemProperty}', " +
							$"serv.is_publisher AS '{Constants.IsPublishedProperty}', " +
							$"serv.is_subscriber AS '{Constants.IsSubscriberProperty}', " +
							$"serv.is_distributor AS '{Constants.IsDistributorProperty}', " +
							$"serv.is_nonsql_subscriber AS '{Constants.IsNonSqlSubscriberProperty}' " +
							"FROM sys.servers AS serv WHERE name = @@SERVERNAME ");

				case DbEntityType.Database:
					return ($"SELECT suser_sname( owner_sid ) AS '{Constants.OwnerNameProperty}', " +
							$"create_date AS '{Constants.CreationDateProperty}', " +
							$"compatibility_level AS '{Constants.VersionProperty}', " +
							$"collation_name AS '{Constants.CollationNameProperty}', " +
							$"user_access_desc AS '{Constants.AccessLevelProperty}', " +
							$"is_read_only AS '{Constants.IsReadOnlyProperty}', " +
							$"state_desc AS '{Constants.StateProperty}', " +
							$"recovery_model_desc AS '{Constants.RecoveryModelProperty}', " +
							$"is_encrypted AS '{Constants.IsEncripedProperty}', " +
							$"page_verify_option_desc AS '{Constants.PageVerifyOptionProperty}' " +
							$"FROM sys.databases WHERE name = {Constants.DatabaseNameParameter}");

				case DbEntityType.Schema:
					return ($"SELECT SCHEMA_OWNER AS '{Constants.SchemaOwnerProperty}' FROM INFORMATION_SCHEMA.SCHEMATA " +
							$"WHERE SCHEMA_NAME = {Constants.SchemaNameParameter}");


				case DbEntityType.Table:
					return ($"SELECT create_date AS '{Constants.CreationDateProperty}', " +
							$"modify_date AS '{Constants.ModifyDateProperty}', " +
							$"is_ms_shipped AS '{Constants.IsMsShippedProperty}', " +
							$"is_published AS '{Constants.IsPublishedProperty}', " +
							$"has_unchecked_assembly_data AS '{Constants.HasUncheckedAssemblyDataProperty}', " +
							$"lock_escalation_desc AS '{Constants.LockEscalationProperty}', " +
							$"lock_on_bulk_load AS '{Constants.LockOnBulkLoadProperty}', " +
							$"is_replicated AS '{Constants.IsReplicatedProperty}', " +
							$"has_replication_filter AS '{Constants.HasReplicationFilterProperty}', " +
							$"is_filetable AS '{Constants.IsFileTableProperty}' " +
							$"FROM sys.tables" +
							$" WHERE name = {Constants.NameParameter} AND schema_id = Schema_id({Constants.SchemaNameParameter})");


				case DbEntityType.View:
					return ($"SELECT create_date AS '{Constants.CreationDateProperty}', " +
							$"modify_date AS '{Constants.ModifyDateProperty}', " +
							$"is_ms_shipped AS '{Constants.IsMsShippedProperty}', " +
							$"is_published AS '{Constants.IsPublishedProperty}', " +
							$"is_replicated AS '{Constants.IsReplicatedProperty}', " +
							$"has_replication_filter AS '{Constants.HasReplicationFilterProperty}', " +
							$"has_unchecked_assembly_data AS '{Constants.HasUncheckedAssemblyDataProperty}', " +
							$"with_check_option AS '{Constants.WithCheckOptionProperty}', " +
							$"has_opaque_metadata AS '{Constants.HasOpaqueMetadataProperty}', " +
							$"is_date_correlation_view AS '{Constants.IsDateCorrelationViewProperty}' " +
							$"FROM sys.views WHERE name = {Constants.NameParameter} AND schema_id = Schema_id({Constants.SchemaNameParameter})");

				case DbEntityType.Trigger:
					return ($"SELECT tr.create_date AS '{Constants.CreationDateProperty}', " +
							$"tr.modify_date  AS '{Constants.ModifyDateProperty}', " +
							$"tr.is_ms_shipped  AS '{Constants.IsMsShippedProperty}', " +
							$"tr.is_not_for_replication  AS '{Constants.IsNotForReplicationProperty}', " +
							$"tr.is_disabled As '{Constants.IsDisabledProperty}', " +
							$"tr.is_instead_of_trigger AS '{Constants.IsInsteadOfTrigerProperty}', " +
							$"OBJECTPROPERTY( tr.object_id, 'ExecIsUpdateTrigger') AS '{Constants.IsUpdateProperty}' ,  " +
							$"OBJECTPROPERTY( tr.object_id, 'ExecIsDeleteTrigger') AS '{Constants.IsDeleteProperty}' ,  " +
							$"OBJECTPROPERTY( tr.object_id, 'ExecIsInsertTrigger') AS '{Constants.IsInsertProperty}' ,  " +
							$"OBJECTPROPERTY( tr.object_id, 'ExecIsAfterTrigger') AS '{Constants.IsAfterProperty}' " +
							"FROM  sys.triggers as tr " +
							"JOIN sys.objects as ob on  tr.object_id=ob.object_id  " +
							$"WHERE ob.parent_object_id = OBJECT_ID({Constants.FullParentNameParameter}) and tr.name = {Constants.NameParameter}");

				case DbEntityType.Constraint:
					return ($"SELECT (SELECT COLUMN_NAME +' ' FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Where TABLE_NAME=inf.TABLE_NAME and CONSTRAINT_NAME=inf.CONSTRAINT_NAME FOR XML PATH('')) AS '{Constants.ColumnsProperty}', " +
							$"con.type_desc AS '{Constants.TypeProperty}', " +
							$"con.create_date AS '{Constants.CreationDateProperty}', " +
							$"con.modify_date AS '{Constants.ModifyDateProperty}', " +
							$"con.definition AS '{Constants.DefinitionProperty}' " +
							"FROM sys.check_constraints AS con " +
							$"JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS inf On con.name=inf.CONSTRAINT_NAME WHERE con.name={Constants.NameParameter} AND inf.TABLE_NAME={Constants.ParentNameParameter}");

				case DbEntityType.Column:
					return ($"SELECT col.is_nullable AS '{Constants.IsNullableProperty}', " +
							$"inf.COLUMN_DEFAULT AS  '{Constants.DefaultValueProperty}', " +
							$"col.is_identity AS '{Constants.IsIdentityProperty}', " +
							$"id.seed_value AS '{Constants.SeedValueProperty}', " +
							$"id.increment_value AS '{Constants.SeedIncrementProperty}', " +
							$"col.is_ansi_padded AS '{Constants.IsAsnsiPaddedProperty}', " +
							$"col.is_computed AS '{Constants.IsComputedProperty}', " +
							$"col.is_filestream AS  '{Constants.IsFilestreamProeprty}', " +
							$"col.is_replicated AS '{Constants.IsReplicatedProperty}', " +
							$"col.is_non_sql_subscribed '{Constants.IsNonSqlSubscribedProperty}', " +
							$"col.is_xml_document as  '{Constants.IsXmlPaddedProperty}', " +
							$"col.collation_name AS '{Constants.CollationNameProperty}', " +
							$"col.column_id AS '{Constants.ColumnIdProperty}' " +
							$"FROM sys.columns AS col " +
							$"JOIN INFORMATION_SCHEMA.COLUMNS AS inf on inf.COLUMN_NAME=col.name and col.object_id=OBJECT_ID(inf.TABLE_CATALOG+'.'+inf.TABLE_SCHEMA+'.'+inf.TABLE_NAME) " +
							$"LEFT JOIN sys.identity_columns AS id ON col.object_id=id.object_id  " +
							$"WHERE col.name={Constants.NameParameter} AND col.object_id=OBJECT_ID({Constants.FullParentNameParameter})");

				case DbEntityType.Function:
					return ($"SELECT CREATED AS '{Constants.CreationDateProperty}', " +
							$"LAST_ALTERED AS '{Constants.LastAlteredProperty}', " +
							$"DATA_TYPE AS '{Constants.ReturnValueTypeProperty}', " +
							$"ROUTINE_BODY AS '{Constants.RoutineBodyProperty}', " +
							$"IS_DETERMINISTIC AS '{Constants.IsDeterministicProperty}', " +
							$"SQL_DATA_ACCESS AS '{Constants.SqlDataAccessProeprty}', " +
							$"IS_USER_DEFINED_CAST AS '{Constants.IsUserDefinedCastProeprty}', " +
							$"IS_IMPLICITLY_INVOCABLE AS '{Constants.IsImplicityInvocableProeprty}' " +
							$"FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE={Constants.TypeParameter} AND SPECIFIC_SCHEMA = {Constants.SchemaNameParameter} AND ROUTINE_NAME= {Constants.NameParameter}");

				case DbEntityType.Procedure:
					return ($"SELECT CREATED AS '{Constants.CreationDateProperty}', " +
							$"LAST_ALTERED AS '{Constants.LastAlteredProperty}', " +
							$"ROUTINE_BODY AS '{Constants.RoutineBodyProperty}', " +
							$"IS_DETERMINISTIC AS '{Constants.IsDeterministicProperty}', " +
							$"SQL_DATA_ACCESS AS '{Constants.SqlDataAccessProeprty}', " +
							$"IS_USER_DEFINED_CAST AS '{Constants.IsUserDefinedCastProeprty}', " +
							$"IS_IMPLICITLY_INVOCABLE AS '{Constants.IsImplicityInvocableProeprty}' " +
							$"FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE={Constants.TypeParameter} AND SPECIFIC_SCHEMA ={Constants.SchemaNameParameter} AND ROUTINE_NAME= {Constants.NameParameter}");

				case DbEntityType.Parameter:
					return ($"SELECT has_default_value AS '{Constants.HasDefaultValueProperty}', " +
						   $"default_value AS '{Constants.DefaultValueProperty}', " +
						   $"is_output AS '{Constants.IsOutputProperty}', " +
						   $"is_readonly AS '{Constants.IsReadOnlyProperty}', " +
						   $"is_xml_document AS  '{Constants.IsXmlDocumentProperty}' " +
						   "FROM  sys.parameters " +
						   $"WHERE object_id = OBJECT_ID({Constants.FullParentNameParameter}) AND name = {Constants.NameParameter} ");

				case DbEntityType.Index:
					return ($"SELECT type_desc AS '{Constants.TypeProperty}', " +
							$"is_unique AS '{Constants.IsUniqueProperty}', " +
							$"is_unique_constraint AS '{Constants.IsUniqueConstraintProperty}', " +
							$"is_primary_key AS '{Constants.IsPrimaryKeyProperty}', " +
							$"is_padded AS '{Constants.IsPaddedProperty}', " +
							$"is_disabled AS '{Constants.IsDisabledProperty}', " +
							$"is_hypothetical AS '{Constants.IsHypotheticalProperty}', " +
							$"has_filter AS '{Constants.HasFilterProperty}' " +
							$"FROM sys.indexes WHERE is_hypothetical = 0 AND index_id != 0 AND object_id = OBJECT_ID({Constants.FullParentNameParameter}) AND name = {Constants.NameParameter}");

				case DbEntityType.Key:
					return  $"SELECT (SELECT COLUMN_NAME +' ' FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE TABLE_NAME=inf.TABLE_NAME and CONSTRAINT_NAME=inf.CONSTRAINT_NAME FOR XML PATH('')) AS '{Constants.ColumnsProperty}', " +
							$"OBJECT_SCHEMA_NAME(ref.referenced_object_id) as '{Constants.ReferenceSchemaNameProperty}', " +
							$"OBJECT_NAME(ref.referenced_object_id) as '{Constants.ReferenceTableNameProperty}', " +
							$"COL_NAME(ref.referenced_object_id,ref.referenced_column_id) as '{Constants.ReferenceColumnProperty}', " +
							$"ks.[{Constants.TypeProperty}], " +
							$"ks.[{Constants.CreationDateProperty}], " +
							$"ks.[{Constants.ModifyDateProperty}], " +
							$"ks.[{Constants.IsSystemNamedProperty}], " +
							$"ks.[{Constants.IsPublishedProperty}], " +
							$"ks.[{Constants.IsMsShippedProperty}], " +
							$"ks.[{Constants.DeleteReferentialActionProperty}], " +
							$"ks.[{Constants.UpdateReferentialActionProperty}], " +
							$"ks.[{Constants.IndexIdProperty}] " +
							$"FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS inf " +
							$"JOIN (SELECT name AS '{Constants.NameProperty}', " +
							$"type_desc AS '{Constants.TypeProperty}', " +
							$"create_date AS '{Constants.CreationDateProperty}', " +
							$"modify_date AS '{Constants.ModifyDateProperty}', " +
							$"is_system_named AS '{Constants.IsSystemNamedProperty}', " +
							$"is_published AS '{Constants.IsPublishedProperty}', " +
							$"is_ms_shipped AS '{Constants.IsMsShippedProperty}', " +
							$"NULL AS '{Constants.DeleteReferentialActionProperty}', " +
							$"unique_index_id AS '{Constants.IndexIdProperty}', " +
							$"NULL AS '{Constants.UpdateReferentialActionProperty}' " +
							$"FROM sys.key_constraints " +
							$"UNION SELECT name, " +
							$"type_desc, " +
							$"create_date, " +
							$"modify_date, " +
							$"is_system_named, " +
							$"is_published, " +
							$"is_ms_shipped, " +
							$"delete_referential_action_desc, " +
							$"key_index_id, " +
							$"update_referential_action_desc " +
							$"FROM sys.foreign_keys ) AS ks ON inf.CONSTRAINT_NAME=ks.[{Constants.NameProperty}]  " +
							$"LEFT JOIN sys.foreign_key_columns AS ref ON OBJECT_ID(inf.TABLE_SCHEMA+'.'+inf.CONSTRAINT_NAME)=ref.constraint_object_id  " +
							$"WHERE inf.CONSTRAINT_NAME ={Constants.NameParameter} AND inf.CONSTRAINT_SCHEMA={Constants.SchemaNameParameter} AND inf.CONSTRAINT_CATALOG={Constants.DatabaseNameParameter}";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override IEnumerable<IDbDataParameter> GetLoadPropertiesParameters(DbObject obj)
		{
			List<IDbDataParameter> parameters = new List<IDbDataParameter>();
			switch (obj.Type)
			{
				case DbEntityType.Database:
					parameters.Add(new SqlParameter(Constants.DatabaseNameParameter, obj.DataBaseName));
					break;

				case DbEntityType.Schema:
					parameters.Add(new SqlParameter(Constants.SchemaNameParameter, obj.SchemaName));
					break;

				case DbEntityType.Table:
				case DbEntityType.View:
					parameters.Add(new SqlParameter(Constants.NameParameter, obj.Name));
					parameters.Add(new SqlParameter(Constants.SchemaNameParameter, obj.SchemaName));
					break;

				case DbEntityType.Function:
				case DbEntityType.Procedure:
					parameters.Add(new SqlParameter(Constants.TypeParameter, obj.Type.ToString().ToUpper()));
					parameters.Add(new SqlParameter(Constants.SchemaNameParameter, obj.SchemaName));
					parameters.Add(new SqlParameter(Constants.NameParameter, obj.Name));
					break;

				case DbEntityType.Trigger:
				case DbEntityType.Index:
				case DbEntityType.Parameter:
				case DbEntityType.Column:
					parameters.Add(new SqlParameter(Constants.NameParameter, obj.Name));
					parameters.Add(new SqlParameter(Constants.FullParentNameParameter, obj.Parent.FullName.ToString()));
					break;

				case DbEntityType.Constraint:
					parameters.Add(new SqlParameter(Constants.ParentNameParameter, obj.Parent.Name));
					parameters.Add(new SqlParameter(Constants.NameParameter, obj.Name));

					break;

				case DbEntityType.Key:
					parameters.Add(new SqlParameter(Constants.NameParameter, obj.Name));
					parameters.Add(new SqlParameter(Constants.SchemaNameParameter, obj.SchemaName));
					parameters.Add(new SqlParameter(Constants.DatabaseNameParameter, obj.DataBaseName));
					break;


				case DbEntityType.Server:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return parameters;
		}
	}
}
