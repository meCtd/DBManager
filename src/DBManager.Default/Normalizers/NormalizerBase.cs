using DBManager.Default.Tree;

namespace DBManager.Default.Normalizers
{
    public abstract class NormalizerBase
    {
        public abstract string ScriptDelimiter { get; }

        public abstract string ParameterPrefix { get; }

        public abstract string Quote(string name);
    }
}
