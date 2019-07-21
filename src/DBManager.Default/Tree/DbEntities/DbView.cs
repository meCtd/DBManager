using System.Runtime.Serialization;

namespace DataBaseTree.Model.Tree.DbEntities
{
	[DataContract(Name = "view")]
	public class DbView : TableData
	{
		public override DbEntityType Type => DbEntityType.View;

		public override bool CanHaveDefinition => true;

		public DbView(string name) : base(name)
		{
		}

		
	}
}
