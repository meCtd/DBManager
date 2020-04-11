using System.Runtime.Serialization;


namespace DBManager.Default.Tree.DbEntities
{
    [DataContract(Name = "type-object")]
    public abstract class TypeObject : DbObject
    {
        [DataMember(Name = "db-type")]
        public DbType DbType { get; set; }

        [DataMember(Name = "ordinal")]
        public int Ordinal { get; set; }

        [DataMember(Name = "default-value")]
        public int DefaultValue { get; set; }

        protected TypeObject(string name) : base(name)
        {
        }

        //public override string ToString()
        //{
        //StringBuilder name = new StringBuilder();
        //name.Append($"{Name} ({ObjectMemberType.Name}");
        //switch (ObjectMemberType.Name)
        //{
        //	case "nvarchar":
        //	case "varchar":
        //	case "nchar":
        //	case "сhar":
        //	case "varbinary":
        //	case "binary":
        //		name.Append(ObjectMemberType.Length == -1 ? $"(max))" : $"({ObjectMemberType.Length}))");
        //		break;

        //	case "time":
        //	case "datetime2":
        //		name.Append($"({ObjectMemberType.Scale}))");
        //		break;

        //	case "decimal":
        //		name.Append($"({ObjectMemberType.Precision},{ObjectMemberType.Scale})");
        //		break;
        //	default:
        //		name.Append(")");
        //		break;
        //}
        //return name.ToString();
        //}
    }
}
