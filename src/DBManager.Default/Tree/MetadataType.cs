using System;

namespace DBManager.Default.Tree
{
	[Flags]
	public enum MetadataType
	{
		None = 0,
		Database = 1 << 1,
		Schema = 1 << 2,
		Table = 1 << 3,
		View = 1 << 4,
		Function = 1 << 5,
		Procedure = 1 << 6,
		Constraint = 1 << 7,
		Column = 1 << 8,
		Trigger = 1 << 9,
		Parameter = 1 << 10,
		Key = 1 << 11 ,
		Index = 1 << 12,
		Type = 1 << 13
	}
}
