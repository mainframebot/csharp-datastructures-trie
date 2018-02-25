using System;
using System.Collections.Generic;
using Trie.Interfaces;
using Trie.Models;

namespace Trie.Functions
{
    public class TrieSearch<T> : ITrieSearch<T>
    {
        private readonly Trie<T> _trie;

        public TrieSearch(Trie<T> trie)
        {
            if (trie == null)
                throw new ArgumentNullException();

            _trie = trie;
        }

        public string SearchForLongestPrefixOf(string key)
        {
            var length = Search(_trie.Root, key, 0, 0);
            return key.Substring(0, length);
        }

        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            var queue = new Queue<string>();

            var result = Search(_trie.Root, prefix, 0);

            Collect(result, prefix, queue);

            return queue;
        }

        public IEnumerable<string> KeysThatMatch(string pattern)
        {
            var queue = new Queue<string>();

            Collect(_trie.Root, "", pattern, queue);

            return queue;
        }

        public IEnumerable<string> Keys()
        {
            return KeysWithPrefix("");
        }

        public IEnumerable<T> Search(string key)
        {
            var result = Search(_trie.Root, key, 0);

            return result != null ? result.Values : null;
        }

        private int Search(Node<T> node, string key, int index, int longest)
        {
            if (node.Values != null)
                longest = index;

            if (index == key.Length)
                return index;

            var character = key[index];

            if (node.Children[character] == null)
                return longest;

            return Search(node.Children[character], key, index + 1, longest);
        }

        private void Collect(Node<T> node, string prefix, Queue<string> queue)
        {
            if (node == null)
                return;

            if (node.Values != null)
                queue.Enqueue(prefix);

            for(int i = 0; i < node.Children.Length; i++)
            {
                if (node.Children[i] != null)
                {
                    var character = (char) i;
                    Collect(node.Children[i], prefix + character, queue);
                }
            }
        }

        private void Collect(Node<T> node, string prefix, string pattern, Queue<string> queue)
        {
            var length = prefix.Length;

            if (length == pattern.Length && node.Values != null)
                queue.Enqueue(prefix);

            if (length == pattern.Length)
                return;

            var nextCharacter = pattern[length];

            for (int i = 0; i < node.Children.Length; i++)
            {
                if (node.Children[i] != null)
                {
                    var character = (char)i;

                    if (nextCharacter == '.' || nextCharacter == character)
                    {
                        Collect(node.Children[i], prefix + character, pattern, queue);
                    }
                }
            }
        }

        private Node<T> Search(Node<T> node, string key, int index)
        {
            if (index == key.Length)
                return node;

            var character = key[index];

            if (node.Children[character] == null)
                return null;

            return Search(node.Children[character], key, index + 1);
        }
    }
}
