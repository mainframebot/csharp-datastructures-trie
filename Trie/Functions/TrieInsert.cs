using System;
using Trie.Interfaces;
using Trie.Models;

namespace Trie.Functions
{
    public class TrieInsert<T> : ITrieInsert<T>
    {
        private readonly Trie<T> _trie;

        public TrieInsert(Trie<T> trie)
        {
            if(trie == null)
                throw new ArgumentNullException();

            _trie = trie;
        }

        public void Insert(string key, T value)
        {
            _trie.Root = Insert(_trie.Root, key, value, 0);

            _trie.Count++;
        }

        private Node<T> Insert(Node<T> node, string key, T value, int index)
        {
            if (node == null)
            {
                node = new Node<T>();
            }

            if (index == key.Length)
            {
                node.Values.Enqueue(value);
            }
            else
            {
                var character = key[index];

                node.Children[character] = Insert(node.Children[character], key, value, index + 1);
            }

            return node;
        }
    }
}
