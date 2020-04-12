using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Default.Tree
{
    public enum ConstraintType
    {
        None,
        PrimaryKey,
        ForeignKey,
        CheckConstraint,
        UniqueConstraint
    }
}
