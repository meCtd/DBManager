namespace DBManager.Default.DataType
{
    public interface IDataTypeFactory
    {
        TypeDescriptor CreateTypeDescriptor(string typeName);
    }
}