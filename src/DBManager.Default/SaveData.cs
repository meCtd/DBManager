using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataBaseTree.Model.Loaders;
using DataBaseTree.Model.Tree;
using DataBaseTree.Model.Tree.DbEntities;

namespace DataBaseTree.Model
{
	[DataContract(Name = "SaveData")]
	[KnownType("KnownType")]
	public class SaveData
	{
		[DataMember(Name ="Loader")]
		public ObjectLoader ObjectLoader { get; private set; }

		[DataMember(Name = "Root")]
		public DbObject Root { get; private set; }

		public SaveData(ObjectLoader objectLoader,DbObject root)
		{
			ObjectLoader = objectLoader;
			Root = root;
		}

		private static IEnumerable<Type> KnownType()
		{

			foreach (var type in typeof(ObjectLoader).Assembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof(ObjectLoader)))
					yield return type;
			}

			yield return typeof(Server);
		}
	}
}
