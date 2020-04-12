﻿using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "constraint")]
	public class Constraint : DbObject
	{
		public override MetadataType Type => MetadataType.Constraint;

		public ConstraintType ConstraintType { get; set; }

		public Constraint(string name) : base(name)
		{
		}
	}
}
