using System;
using SuffixTrie.Interfaces;
using Trie;
using System.Collections.Generic;
using SuffixTrie.Models;

namespace SuffixTrie.Functions
{
    public class TrieInsert : ITrieInsert
    {
        private readonly Trie<Partition> _trie;

        public TrieInsert(Trie<Partition> trie)
        {
            _trie = trie;
        }

        public void Insert(string key)
        {
            foreach (var suffix in GenerateSuffixes(key))
            {
                _trie.Insert(suffix.KeyPart, suffix);
            }
        }

        private static IEnumerable<Partition> GenerateSuffixes(string key)
        {
            for (int i = key.Length - 1; i >= 0; i--)
            {
                var keyPart = key.Substring(i);

                yield return new Partition(key, keyPart, keyPart.Length, i);
            }
        }
    }
}
