﻿using System.Threading;
using System.Threading.Tasks;
using DBManager.Default.Tree;

namespace DBManager.Default.Loaders
{
    public interface IObjectLoader
    {
        Task LoadChildrenAsync(DbObject obj, CancellationToken token);

        Task LoadChildrenAsync(DbObject obj, MetadataType childType, CancellationToken token);

        Task LoadPropertiesAsync(DbObject obj, CancellationToken token);
    }
}
