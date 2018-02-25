namespace PatriciaTrie.Models
{
    public class Node<T>
    {
        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public string Key { get; set; }

        public T Value { get; set; }

        public int Bit { get; set; }

        public Node(string key, T value, int position)
        {
            Key = key;
            Value = value;
            Bit = position;
        }
    }
}
