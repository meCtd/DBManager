using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "routine")]
	public abstract class Routine : DefinitionObject
	{
		protected Routine(string name) : base(name)
		{
		}
	}

}
