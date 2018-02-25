using System.Collections.Generic;

namespace Trie.Models
{
    public class Node<T>
    {
        public Node<T>[] Children { get; set; }

        public Queue<T> Values { get; set; }

        public Node()
        {
            Children = new Node<T>[Trie<T>.R];
            Values = new Queue<T>();
        }
    }
}
