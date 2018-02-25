using System.Collections.Generic;

namespace Trie.Interfaces
{
    public interface ITrieSearch<T>
    {
        string SearchForLongestPrefixOf(string key);

        IEnumerable<string> KeysWithPrefix(string prefix);

        IEnumerable<string> KeysThatMatch(string pattern);

        IEnumerable<string> Keys();

        IEnumerable<T> Search(string key);
    }
}
