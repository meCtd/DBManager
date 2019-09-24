using System.Runtime.Serialization;

namespace DBManager.Default.Tree.DbEntities
{
	[DataContract(Name = "parameter")]

	public abstract class Parameter : TypeObject
	{
		public override MetadataType Type => MetadataType.Parameter;

		public override bool CanHaveDefinition => false;

		public Parameter(string name, DbType parameterType) : base(name, parameterType)
		{
		}
	}
}
