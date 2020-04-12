using System.Data.Common;

namespace DBManager.Access.ADO
{
    class AccessConnectionStringBuilder : DbConnectionStringBuilder
    {
        #region Fields
        private const string DataSourceCapture = "Data Source";
        private const string PasswordCapture = "Database Password";

        #endregion

        #region Properties

        public string DataSource
        {
            get { return DefaultIfNotExists(DataSourceCapture); }
            set { base[DataSourceCapture] = value; }
        }

        public string Password
        {
            get { return DefaultIfNotExists(PasswordCapture); }
            set { base[PasswordCapture] = value; }
        }

        #endregion

        #region C-tors

        public AccessConnectionStringBuilder()
        {
            Clear();
        }

        public AccessConnectionStringBuilder(string connectionString)
            : this()
        {
            ConnectionString = connectionString;
        }

        #endregion

        #region Methods

        private string DefaultIfNotExists(string caption)
        {
            if (!ContainsKey(caption))
                return string.Empty;

            return (string)this[caption];
        }

        public override sealed void Clear()
        {
            base.Clear();

            this[DataSourceCapture] = string.Empty;
            this[PasswordCapture] = string.Empty;
        }

        #endregion
    }
}
