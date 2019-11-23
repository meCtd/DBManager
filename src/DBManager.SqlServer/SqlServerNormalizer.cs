using DBManager.Default.Normalizers;

namespace DBManager.SqlServer
{
    internal class SqlServerNormalizer : NormalizerBase
    {
        private static SqlServerNormalizer _instance;

        public static SqlServerNormalizer Instance => _instance ?? (_instance = new SqlServerNormalizer());

        private SqlServerNormalizer()
        {
        }

        public override string ScriptDelimiter => "GO";

        public override string ParameterPrefix => "@";
        public override string Quote(string name)
        {
            return $"[{name}]";
        }
    }
}
