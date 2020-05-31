namespace DBManager.Default.DataType
{
    public class TypeDescriptor
    {
        public string Name { get; }
        public DataTypeFamily TypeFamilty { get; }

        public int? Length { get; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }

        public TypeDescriptor(string name, DataTypeFamily typeFamily)
        {
            Name = name;
            TypeFamilty = typeFamily;
        }
    }
}