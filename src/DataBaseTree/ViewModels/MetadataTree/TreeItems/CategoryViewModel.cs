using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using DBManager.Default.Loaders;
using DBManager.Default.Tree;

using Ninject;
using Plurally;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class CategoryViewModel : MetadataViewModelBase
    {
        private const string NameFormat = "{0} [{1}]";

        private static readonly Pluralizer _pluralizer = new Pluralizer();

        private readonly string _categoryName;

        private readonly DbObject _model;

        public override string ObjectName => IsBusy
            ? _categoryName
            : string.Format(NameFormat, _categoryName, Children.Count);

        public override MetadataType Type { get; }

        public CategoryViewModel(DbObject model, MetadataViewModelBase parent, MetadataType type) : base(parent, true)
        {
            Type = type;
            _model = model;
            _categoryName = _pluralizer.Pluralize(type.ToString());
        }

        protected override void RemoveChildrenFromModel()
        {
            _model.RemoveChildren(Type);
        }

        protected override async Task LoadChildren()
        {
            var loader = Resolver.Get<IObjectLoader>();

            await loader.LoadChildrenAsync(_model, Type, CancellationToken.None);

        }
    }
}
