using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "table")]
	[KnownType(typeof(Constraint))]
	[KnownType(typeof(Key))]
	public class Table : TableData
	{
		public override DbEntityType Type => DbEntityType.Table;

		public Table(string name) : base(name)
		{
		}

		protected override bool CanBeChild(DbObject obj)
		{
			return base.CanBeChild(obj) || obj.Type == DbEntityType.Key ||
				   obj.Type == DbEntityType.Constraint;
		}

	}
}
