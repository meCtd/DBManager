using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract]
	[KnownType(typeof(Column))]
	[KnownType(typeof(Trigger))]
	[KnownType(typeof(Index))]
	public abstract class TableData : DbObject
	{
		public override bool CanHaveDefinition => false;

		protected TableData(string name) : base(name)
		{
		}
	}
}
