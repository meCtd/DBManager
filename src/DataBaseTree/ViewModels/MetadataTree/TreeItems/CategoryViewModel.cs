using System.Threading;
using System.Threading.Tasks;

using DBManager.Default.Tree;

using Framework.EventArguments;

using Plurally;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class CategoryViewModel : MetadataViewModelBase
    {
        private const string NameFormat = "{0} [{1}]";

        private static readonly Pluralizer _pluralizer = new Pluralizer();

        private readonly string _categoryName;

        private readonly DbObject _model;

        private string _nameFormat = "{0}";

        public override string ObjectName => IsBusy
            ? _categoryName
            : string.Format(_nameFormat, _categoryName, Children.Count);

        public override MetadataType Type { get; }

        public CategoryViewModel(DbObject model, MetadataViewModelBase parent, MetadataType type) : base(parent, true)
        {
            Type = type;
            _model = model;
            _categoryName = _pluralizer.Pluralize(type.ToString());

            ExpandChanged += OnExpandChanged;
        }

        private void OnExpandChanged(object sender, ValueChangedEventArgs<bool> e)
        {
            _nameFormat = NameFormat;
        }

        protected override void RemoveChildrenFromModel()
        {
            _model.RemoveChildren(Type);
        }

        protected override async Task LoadChildren()
        {
            await GetLoader().LoadChildrenAsync(_model, Type, CancellationToken.None);
        }
    }
}
