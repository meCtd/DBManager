using System;
using System.IO;

using System.Windows;
using System.Xml;

using DBManager.Default;

using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;


namespace DBManager.Application.Controls
{
    class SqlEditor : TextEditor
    {
        private bool _isSuspended;

        public static readonly DependencyProperty SqlProperty = DependencyProperty.Register(
            "Sql", typeof(string), typeof(SqlEditor), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSqlChanged));

        public string Sql
        {
            get { return (string)GetValue(SqlProperty); }
            set { SetValue(SqlProperty, value); }
        }

        private static void OnSqlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (SqlEditor)d;
            if(editor._isSuspended)
                return;
            
            editor.Text = (string)e.NewValue;
        }

        public SqlEditor()
        {
            Setup();
        }

        public static void RegisterHighlights()
        {
            RegisterHighlight(DialectType.SqlServer, Properties.Resources.MsSql);
        }

        private static void RegisterHighlight(DialectType dialect, Byte[] ashx)
        {
            IHighlightingDefinition definition;

            using (Stream stream = new MemoryStream(ashx))
            using (XmlReader reader = XmlReader.Create(stream))
                definition = HighlightingLoader.Load(reader, HighlightingManager.Instance);

            HighlightingManager.Instance.RegisterHighlighting(dialect.ToString(), new String[] { }, definition);
        }

        private void Setup()
        {
            TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                _isSuspended = true;
                Sql = Text;
            }
            finally
            {
                _isSuspended = false;
            }
        }
    }
}
