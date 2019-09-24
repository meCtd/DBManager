using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "routine")]
	[KnownType(typeof(Parameter))]
	public abstract class Routine : DbObject
	{
		public override bool CanHaveDefinition => true;

		protected Routine(string name) : base(name)
		{
		}
	}

}
