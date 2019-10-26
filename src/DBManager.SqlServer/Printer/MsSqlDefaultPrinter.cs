using DBManager.Default.Printers;
using DBManager.Default.Tree;

namespace DBManager.SqlServer.Printer
{
	public class MsSqlDefaultPrinter : IPrinter
	{

		public string GetDefinition(DbObject dbObject)
		{
			if(!dbObject.CanHaveDefinition)
				dbObject.Definition = $"CREATE {dbObject.Type.ToString().ToUpper()} [{dbObject.Name}]";
			return dbObject.Definition;
		}

        //TODO:REWRITE PRINTER STRUCTURE
        public string GetDefinition()
        {
            throw new System.NotImplementedException();
        }
    }
}
