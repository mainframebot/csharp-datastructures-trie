using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSTTrie.Models;

namespace TSTTrie
{
    public class TSTTrie<T>
    {
        public Node<T> Root { get; set; }

        public void Insert(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException();

            Root = Insert(Root, key, value, 0);
        }

        private Node<T> Insert(Node<T> node, string key, T value, int position)
        {
            var character = key[position];

            if (node == null)
            {
                node = new Node<T> {Index = character};
            }

            if (character < node.Index)
            {
                node.Left = Insert(node.Left, key, value, position);
            }
            else if (character > node.Index)
            {
                node.Right = Insert(node.Right, key, value, position);
            }
            else if (position < key.Length - 1)
            {
                node.Middle = Insert(node.Middle, key, value, position + 1);
            }
            else
            {
                node.Values.Enqueue(value);
            }

            return node;
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

        private Node<T> Search(Node<T> node, string key, int position)
        {
            if (node == null)
                return null;

            var character = key[position];

            if (position == key.Length)
                return node;

            if (character < node.Index)
            {
                return Search(node.Left, key, position);
            }
            else if (character > node.Index)
            {
                return Search(node.Right, key, position);
            }
            else if (position < key.Length - 1)
            {
                return Search(node.Middle, key, position + 1);
            }
            else
            {
                return node;
            }
        }
    }
}
