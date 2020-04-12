namespace DBManager.Default.Tree
{
    public struct Chunk
    {
        public string Name { get; }

        public MetadataType Type { get; }

        public Chunk(string name, MetadataType type)
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

