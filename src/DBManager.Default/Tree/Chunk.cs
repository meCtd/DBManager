namespace DataBaseTree.Model.Tree
{
	public class Chunk
	{
		public string Name { get; }

		public DbEntityType Type { get; }

		public Chunk(string name, DbEntityType type)
		{
			Name = name;
			Type = type;
		}
	}
}

