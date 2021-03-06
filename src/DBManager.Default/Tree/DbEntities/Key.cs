﻿using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "key")]
	public class Key : DbObject
	{
		public override MetadataType Type => MetadataType.Key;

		public Key(string name) : base(name)
		{
		}

	}
}
