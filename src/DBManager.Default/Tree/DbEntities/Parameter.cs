﻿using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "parameter")]

	public class Parameter : TypeObject
	{
		public override DbEntityType Type => DbEntityType.Parameter;

		public override bool CanHaveDefinition => false;

		public Parameter(string name, DbType parameterType) : base(name, parameterType)
		{
		}
	}
}
