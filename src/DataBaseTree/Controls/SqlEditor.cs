using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ICSharpCode.AvalonEdit;

namespace DBManager.Application.Controls
{
    class SqlEditor : TextEditor
    {
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
            editor.Text = (string)e.NewValue;
        }
    }
}
