using System;
using DataBaseTree.Model.Tree;

namespace DataBaseTree.Model.Printers
{
	public class MsSqlPrinterFactory : IPrinterFactory
	{
		public bool IsSupported(DbObject dbobject)
		{
			switch (dbobject.Type)
			{
				case DbEntityType.None:
				case DbEntityType.Server:
				case DbEntityType.Constraint:
				case DbEntityType.Column:
				case DbEntityType.Parameter:
				case DbEntityType.Key:
				case DbEntityType.Index:
				case DbEntityType.Type:
				case DbEntityType.All:
					return false;
			}

			return true;
		}

		public IPrinter GetPrinter(DbObject obj)
		{
			if (!IsSupported(obj))
				throw new NotSupportedException();
			if (obj.Type == DbEntityType.Table)
				return new MsSqlTablePrinter();

			return new MsSqlDefaultPrinter();
		}
	}
}
