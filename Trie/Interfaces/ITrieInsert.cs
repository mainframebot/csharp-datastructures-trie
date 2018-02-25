using Trie.Models;

namespace Trie.Interfaces
{
    public interface ITrieInsert<T>
    {
        void Insert(string key, T value);
    }
}
