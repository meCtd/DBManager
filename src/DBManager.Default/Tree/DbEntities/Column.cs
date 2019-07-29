﻿using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "column")]
	
	public class Column : TypeObject
	{
		public override DbEntityType Type => DbEntityType.Column;

		public override bool CanHaveDefinition => false;

		public Column(string name, DbType columnType) : base(name, columnType)
		{
		}
	}
}
