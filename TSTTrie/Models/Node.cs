using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSTTrie.Models
{
    public class Node<T>
    {
        public char Index { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }   

        public Node<T> Middle { get; set; }

        public Queue<T> Values { get; set; }

        public Node()
        {
            Values = new Queue<T>();
        } 
    }
}
