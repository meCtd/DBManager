using DBManager.Default.Normalizers;

namespace DBManager.SqlServer
{
    internal class SqlServerNormalizer : NormalizerBase
    {
        public override string ScriptDelimiter => "GO";

        public override string ParameterPrefix => "@";
        public override string Quote(string name)
        {
            return $"[{name}]";
        }
    }
}
