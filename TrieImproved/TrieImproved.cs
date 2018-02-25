using System;
using System.Collections.Generic;
using System.Linq;
using TrieImproved.Models;

namespace TrieImproved
{
    public class TrieImproved<T>
    {
        public Node<T> Root { get; set; }

        //public int Count { get; set; }

        public void Insert(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException();

            Root = Insert(Root, key, value, 0);
        }

        public void Delete(string key)
        {
            Delete(null, Root, key, 0);
        }

        public string SearchForLongestPrefixOf(string key)
        {
            var length = Search(Root, key, 0, 0);
            return key.Substring(0, length);
        }

        public IEnumerable<T> Search(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException();

            var result = Search(Root, key, 0);

            if (result == null)
                return null;

            return result.Values;
        }

        public IEnumerable<string> Keys()
        {
            return KeysWithPrefix("");
        }

        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            var queue = new Queue<string>();

            var result = Search(Root, prefix, 0);

            Collect(result, prefix, queue);

            return queue;
        }

        public IEnumerable<string> KeysThatMatch(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentNullException();

            var queue = new Queue<string>();

            Collect(Root, "", pattern, queue);

            return queue;
        }

        private void Collect(Node<T> node, string prefix, Queue<string> queue)
        {
            if (node == null)
                return;

            if (node.Values != null)
                queue.Enqueue(prefix);

            foreach (var child in node.Children)
            {
                Collect(child.Value, prefix + child.Key, queue);
            }
        }

        private void Collect(Node<T> node, string prefix, string pattern, Queue<string> queue)
        {
            var length = prefix.Length;

            if (node == null)
                return;

            if (length == pattern.Length && node.Values != null)
                queue.Enqueue(prefix);

            if (length == pattern.Length)
                return;

            var nextCharacter = pattern[length];

            foreach (var child in node.Children)
            {
                if (nextCharacter == '.' || nextCharacter == child.Key)
                {
                    Collect(child.Value, prefix + child.Key, pattern, queue);
                }
            }
        }

        private Node<T> Search(Node<T> node, string key, int position)
        {
            if (node == null)
                return null;

            if (position == key.Length)
                return node;

            Node<T> result;
            node.Children.TryGetValue(key[position], out result);

            return Search(result, key, position + 1);
        }

        private int Search(Node<T> node, string key, int length, int longest)
        {
            if (node == null)
                return longest;

            if (node.Values != null)
                longest = length;

            if (length == key.Length)
                return length;

            var nextCharacter = key[length];

            var result = node.Children.FirstOrDefault(x => x.Key == nextCharacter);

            return Search(result.Value, key, length + 1, longest);
        }

        private Node<T> Insert(Node<T> node, string key, T value, int position)
        {
            if (node == null)
            {
                node = new Node<T>();
            }

            if (position == key.Length)
            {
                node.Values.Enqueue(value);
            }
            else
            {
                Node<T> result;
                if (!node.Children.TryGetValue(key[position], out result))
                {
                    result = new Node<T>();
                    node.Children.Add(key[position], result);
                }

                Insert(result, key, value, position + 1);
            }

            return node;
        }

        private void Delete(Node<T> parent, Node<T> node, string key, int length)
        {
            if (length == key.Length)
            {
                node.Values.Clear();
            }
            else
            {
                var nextCharacter = key[length];

                var result = node.Children.FirstOrDefault(x => x.Key == nextCharacter);

                Delete(node, result.Value, key, length + 1);
            }

            if (!node.Children.Any())
            {
                if (parent != null)
                {
                    parent.Children.Remove(key[length - 1]);
                }
            }
        }
    }
}
