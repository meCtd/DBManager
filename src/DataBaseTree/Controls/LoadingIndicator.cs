using System.Windows;
using System.Windows.Controls;

namespace DBManager.Application.Controls
{
    public class LoadingIndicator : Control
    {
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description", typeof(string), typeof(LoadingIndicator), new PropertyMetadata("Loading..."));

        public string Description
        {
            get { return (string) GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
    }
}
