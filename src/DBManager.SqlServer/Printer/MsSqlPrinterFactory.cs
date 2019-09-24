using System;
using DBManager.Default.Printers;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Printer
{
	public class MsSqlPrinterFactory : IPrinterFactory
	{
		public bool IsSupported(DbObject dbobject)
		{
			switch (dbobject.Type)
			{
				case MetadataType.None:
				case MetadataType.Server:
				case MetadataType.Constraint:
				case MetadataType.Column:
				case MetadataType.Parameter:
				case MetadataType.Key:
				case MetadataType.Index:
				case MetadataType.Type:
				case MetadataType.All:
					return false;
			}

			return true;
		}

		public IPrinter GetPrinter(DbObject obj)
		{
			if (!IsSupported(obj))
				throw new NotSupportedException();
			if (obj.Type == MetadataType.Table)
				return new MsSqlTablePrinter();

			return new MsSqlDefaultPrinter();
		}
	}
}
