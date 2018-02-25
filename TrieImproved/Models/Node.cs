using System.Collections.Generic;

namespace TrieImproved.Models
{
    public class Node<T>
    {
        public Dictionary<char, Node<T>> Children { get; set; }

        public Queue<T> Values { get; set; }

        public Node()
        {
            Children = new Dictionary<char, Node<T>>();
            Values = new Queue<T>();
        }
    }
}
