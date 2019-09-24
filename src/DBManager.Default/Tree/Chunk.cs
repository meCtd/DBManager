namespace DBManager.Default.Tree
{
	public class Chunk
	{
		public string Name { get; }

		public MetadataType Type { get; }

		public Chunk(string name, MetadataType type)
		{
			Name = name;
			Type = type;
		}
	}
}

