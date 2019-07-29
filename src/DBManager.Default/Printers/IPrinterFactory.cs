using DBManager.Default.Tree;

namespace DBManager.Default.Printers
{
	public interface IPrinterFactory
	{
		bool IsSupported(DbObject dbobject);

		IPrinter GetPrinter(DbObject dbobject);
	}
}
