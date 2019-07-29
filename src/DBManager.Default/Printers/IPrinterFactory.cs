using DataBaseTree.Model.Tree;

namespace DataBaseTree.Model.Printers
{
	public interface IPrinterFactory
	{
		bool IsSupported(DbObject dbobject);

		IPrinter GetPrinter(DbObject dbobject);
	}
}
