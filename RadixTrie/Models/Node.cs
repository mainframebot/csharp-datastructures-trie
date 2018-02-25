using System.Collections.Generic;

namespace RadixTrie.Models
{
    public class Node<T>
    {
        public List<Node<T>> Children { get; set; } 

        public Queue<T> Values { get; set; }

        public string Text { get; set; }

        public Node()
        {
            Children = new List<Node<T>>();
            Values = new Queue<T>();
        }
    }
}
