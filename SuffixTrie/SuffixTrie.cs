using System;
using SuffixTrie.Functions;
using SuffixTrie.Interfaces;
using SuffixTrie.Models;
using Trie;

namespace SuffixTrie
{
    public class SuffixTrie
    {
        private readonly Trie<Partition> _trie;

        private readonly ITrieInsert _trieInsert;

        public SuffixTrie()
        {
            _trie = new Trie<Partition>();

            _trieInsert = new TrieInsert(_trie);
        }

        public SuffixTrie(
            Trie<Partition> trie,
            ITrieInsert trieInsert)
        {
            _trie = trie;
            _trieInsert = trieInsert;
        }

        public void Insert(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException();

            _trieInsert.Insert(key);
        }
    }
}
