
using System.Runtime.Serialization;

namespace DataBaseTree.Model.Tree.DbEntities
{
	[DataContract(Name = "schema")]
	[KnownType(typeof(Procedure))]
	[KnownType(typeof(Function))]
	[KnownType(typeof(Table))]
	[KnownType(typeof(DbView))]
	public class Schema : DbObject
	{
		public override DbEntityType Type => DbEntityType.Schema;

		public override bool CanHaveDefinition => false;

		public Schema(string name) : base(name)
		{
		}

		protected override bool CanBeChild(DbObject obj)
		{
			var type = obj.Type;
			return type == DbEntityType.Procedure || type == DbEntityType.Function ||
			       type == DbEntityType.Table || type == DbEntityType.View;

		}
	}
}
