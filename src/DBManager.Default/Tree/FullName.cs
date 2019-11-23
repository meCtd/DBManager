using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace DBManager.Default.Tree
{
    public class FullName : IEnumerable<Chunk>
    {
        private readonly List<Chunk> _items;

        public FullName(DbObject obj)
        {
            _items = new List<Chunk>();

            var current = obj;
            while (current != null)
            {
                _items.Insert(0, new Chunk(current.Name, current.Type));
                current = current.Parent;
            }
        }

        public Chunk Schema => _items.FirstOrDefault(s => s.Type == MetadataType.Schema);
        public Chunk Database => _items.FirstOrDefault(s => s.Type == MetadataType.Database);

        public IEnumerator<Chunk> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(".", _items);

        }
    }
}
