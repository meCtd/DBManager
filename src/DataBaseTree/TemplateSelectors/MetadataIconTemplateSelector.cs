using System.Windows;
using System.Windows.Controls;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;

namespace DBManager.Application.TemplateSelectors
{
    class MetadataIconTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ServerIconTemplate { get; set; }

        public DataTemplate CategoryIconTemplate { get; set; }

        public DataTemplate DbObjectIconTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case ServerViewModel _:
                    return ServerIconTemplate;

                case CategoryViewModel _:
                    return CategoryIconTemplate;

                case DbObjectViewModel _:
                    return DbObjectIconTemplate;

                default: 
                    return new DataTemplate();
            }
        }
    }
}
