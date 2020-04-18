using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DBManager.Default.Loader;
using DBManager.Default.Tree;

using Framework.Extensions;

using Plurally;


namespace DBManager.Application.ViewModels.MetadataTree.TreeItems
{
    public class CategoryViewModel : MetadataViewModelBase
    {
        private const string LoadedNameFormat = "{0} [{1}]";
        private const string DefaultNameFormat = "{0}";

        private static readonly Pluralizer _pluralizer = new Pluralizer();

        private readonly string _categoryName;

        private readonly DbObject _model;

        private string _nameFormat = DefaultNameFormat;

        public override string Name => IsBusy
            ? _categoryName
            : string.Format(_nameFormat, _categoryName, Children.Count);

        public override MetadataType Type { get; }

        public CategoryViewModel(DbObject model, MetadataViewModelBase parent, MetadataType type) : base(parent, true)
        {
            Type = type;
            _model = model;
            _categoryName = _pluralizer.Pluralize(type.ToString());

            Loaded += OnLoaded;
            Refreshed += OnRefreshed;
        }

        private void OnRefreshed(object sender, EventArgs e)
        {
            _nameFormat = DefaultNameFormat;
            OnPropertyChanged(nameof(Name));
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            _nameFormat = LoadedNameFormat;
            OnPropertyChanged(nameof(Name));
        }

        protected override void RemoveChildrenFromModel()
        {
            _model.RemoveChildren(Type);
        }

        protected override async Task LoadChildren()
        {
            var loadingContext = new LoadingContext(((ServerViewModel)Root).ConnectionData, CancellationToken.None);
            await Components.Loader.LoadChildrenAsync(loadingContext, _model, Type);

            _model.Children
                .Where(s => Equals(Type, s.Type))
                .ForEach(s => Children.Add(new DbObjectViewModel(this, s)));
        }
    }
}
