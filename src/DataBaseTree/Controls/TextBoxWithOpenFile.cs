using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DBManager.Application.Controls
{
    class TextBoxWithOpenFile : TextBox
    {
        public static readonly DependencyProperty FileFilterProperty = DependencyProperty.Register("FileFilterProperty", typeof(string), typeof(TextBoxWithOpenFile));

        public string FileFilter
        {
            get => (string)GetValue(FileFilterProperty);
            set => SetValue(FileFilterProperty, value);
        }

        static TextBoxWithOpenFile()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxWithOpenFile), new FrameworkPropertyMetadata(typeof(TextBoxWithOpenFile)));
            CommandManager.RegisterClassCommandBinding(typeof(TextBoxWithOpenFile), new CommandBinding(
                ApplicationCommands.Open,
                (obj, e) => { e.Handled = true; ((TextBoxWithOpenFile)obj).ShowOpenFileDialog(); },
                (obj, e) => { e.CanExecute = true; }));
        }

        private void ShowOpenFileDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = FileFilter
            };

            if (dialog.ShowDialog() == true)
            {
                Text = dialog.FileName;
            }
        }
    }
}
