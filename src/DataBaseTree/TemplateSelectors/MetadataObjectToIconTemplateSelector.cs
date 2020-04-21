using System.Windows;
using System.Windows.Controls;
using DBManager.Default.Tree;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Application.TemplateSelectors
{
    public class MetadataObjectToIconTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DbObjectIconTemplate { get; set; }
        public DataTemplate ConstraintIconTemplate { get; set; }
        public DataTemplate IndexIconTemplate { get; set; }
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case Index _:
                    return IndexIconTemplate;

                case Constraint _:
                    return ConstraintIconTemplate;

                case DbObject _:
                    return DbObjectIconTemplate;

                default:
                    return new DataTemplate();
            }
        }
    }
}
