using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Default.Tree;

namespace DBManager.Default.Normalizers
{
    public abstract class NormalizerBase
    {
        public abstract string ScriptDelimiter { get; }

        public abstract string ParameterPrefix { get; }

        public abstract string Normalize(FullName fullName);
    }
}
