using System;
using System.Collections.Generic;
using Trie.Functions;
using Trie.Interfaces;
using Trie.Models;

namespace Trie
{
    public class Trie<T>
    {
        public static int R { get { return 256; } }

        public Node<T> Root { get; set; }

        public int Count { get; set; }

        private readonly ITrieSearch<T> _trieSearch;

        private readonly ITrieInsert<T> _trieInsert;

        private readonly ITrieDelete<T> _trieDelete;

        public Trie()
        {
            _trieSearch = new TrieSearch<T>(this);
            _trieInsert = new TrieInsert<T>(this);
            _trieDelete = new TrieDelete<T>(this);
        }

        public Trie(
            ITrieSearch<T> trieSearch,
            ITrieInsert<T> trieInsert,
            ITrieDelete<T> trieDelete)
        {
            _trieSearch = trieSearch;
            _trieInsert = trieInsert;
            _trieDelete = trieDelete;
        } 

        public void Insert(string key, T value)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException();

            if(value == null)
                throw new ArgumentNullException();

            _trieInsert.Insert(key, value);
        }

        public void Delete(string key)
        {
            if (Root == null)
                return;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException();   

            _trieDelete.Delete(key);
        }

        public string SearchForLongestPrefixOf(string key)
        {
            if (Root == null)
                return null;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException();

            return _trieSearch.SearchForLongestPrefixOf(key);
        }

        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            if (Root == null)
                return null;

            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException();

            return _trieSearch.KeysWithPrefix(prefix);
        }

        public IEnumerable<string> KeysThatMatch(string pattern)
        {
            if (Root == null)
                return null;

            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentNullException();

            return _trieSearch.KeysThatMatch(pattern);
        } 

        public IEnumerable<T> Search(string key)
        {
            if (Root == null)
                return null;

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException();

            return _trieSearch.Search(key);
        }

        public IEnumerable<string> Keys()
        {
            if (Root == null)
                return null;

            return _trieSearch.Keys();
        }
    }
}
