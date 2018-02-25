using System;
using System.Linq;
using Trie.Interfaces;
using Trie.Models;

namespace Trie.Functions
{
    public class TrieDelete<T> : ITrieDelete<T>
    {
        private readonly Trie<T> _trie;

        public TrieDelete(Trie<T> trie)
        {
            if (trie == null)
                throw new ArgumentNullException();

            _trie = trie;
        }

        public void Delete(string key)
        {
            Delete(null, _trie.Root, key, 0);
        }

        private void Delete(Node<T> parent, Node<T> node, string key, int index)
        {
            if (index == key.Length)
            {
                node.Values = null;
                _trie.Count--;
            }
            else
            {
                var character = key[index];

                if(node.Children[character] != null)
                    Delete(node, node.Children[character], key, index + 1);
            }

            if (parent != null && node.Children.All(x => x == null))
            {
                parent.Children[key[index - 1]] = null;
            }
        }
    }
}
