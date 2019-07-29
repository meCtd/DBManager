using DataBaseTree.Model.Tree;

namespace DataBaseTree.Model.Printers
{
	public interface IPrinter
	{
		string GetDefinition(DbObject dbObject);
		
	}
	
}