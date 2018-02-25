using System;
using System.Text;
using PatriciaTrie.Models;

namespace PatriciaTrie
{
    public class PatriciaTrie<T>
    {
        public Node<T> Root { get; set; } 

        public int Count { get; set; }

        public PatriciaTrie()
        {
            Root = new Node<T>("", default(T), 0);
            Root.Left = Root;
            Root.Right = Root;
            Count = 0;
        }

        public void Insert(string key, T value)
        {
            if(string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException();

            Node<T> parent;
            Node<T> current = Root;

            do
            {
                parent = current;
                current = SafeBitTest(key, current.Bit) ? current.Right : current.Left;
            } while (parent.Bit < current.Bit);

            if (!current.Key.Equals(key))
            {
                var bit = FirstBitDifferent(current.Key, key);

                current = Root;

                do
                {
                    parent = current;
                    current = SafeBitTest(key, current.Bit) ? current.Right : current.Left;
                } while (parent.Bit < current.Bit && current.Bit < bit);

                var node = new Node<T>(key, value, bit);

                if (SafeBitTest(key, bit))
                {
                    node.Left = current;
                    node.Right = node;
                }
                else
                {
                    node.Left = node;
                    node.Right = current;
                }

                if (SafeBitTest(key, parent.Bit))
                    parent.Right = node;
                else
                    parent.Left = node;

                if (string.IsNullOrWhiteSpace(Root.Key))
                {
                    Root = node;
                    Root.Left = Root;
                    Root.Right = Root;
                }

                Count++;
            }
        }

        public T Find(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException();

            Node<T> parent;
            Node<T> current = Root;

            do
            {
                parent = current;
                current = SafeBitTest(key, current.Bit) ? current.Right : current.Left;
            } while (parent.Bit < current.Bit);

            return current.Key.Equals(key) ? current.Value : default(T);
        }

        public void Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException();

            if (Count == 0)
                return;

            Node<T> grandparent;
            Node<T> parent = Root;
            Node<T> current = Root;

            do
            {
                grandparent = parent;
                parent = current;

                current = SafeBitTest(key, current.Bit) ? current.Right : current.Left;
            } while (parent.Bit < current.Bit);

            if (current.Key.Equals(key))
            {
                Node<T> z;
                Node<T> y = Root;

                do
                {
                    z = y;
                    y = SafeBitTest(key, y.Bit) ? y.Right : y.Left;
                } while (y != current);

                if (current == parent)
                {
                    Node<T> child = SafeBitTest(key, current.Bit) ? current.Left : current.Right;

                    if (SafeBitTest(key, z.Bit))
                        z.Right = child;
                    else
                        z.Left = child;
                }
                else
                {
                    Node<T> child = SafeBitTest(key, parent.Bit) ? parent.Left : parent.Right;

                    if (SafeBitTest(key, grandparent.Bit))
                        grandparent.Right = child;
                    else
                        grandparent.Left = child;

                    if (SafeBitTest(key, z.Bit))
                        z.Right = parent;
                    else
                        z.Left = parent;

                    parent.Left = current.Left;
                    parent.Right = current.Right;
                    parent.Bit = current.Bit;
                }

                Count--;
            }
        }

        private int FirstBitDifferent(string key1, string key2)
        {
            if (string.IsNullOrWhiteSpace(key1) || string.IsNullOrWhiteSpace(key2))
                return 0;

            var index = 0;

            var char1 = SafeCharAt(key1, index) & ~1;
            var char2 = SafeCharAt(key2, index) & ~1;

            while (char1 == char2)
            {
                index = 1;

                while (SafeCharAt(key1, index) == SafeCharAt(key2, index))
                {
                    index++;
                }

                char1 = SafeCharAt(key1, index);
                char2 = SafeCharAt(key2, index);
            }

            var position = 0;

            while ((((uint)char1 >> position) & 1) == (((uint)char2 >> position) & 1))
            {
                position++;
            }

            //var result = (index << 4) + position;

            var result = index * 16 + position;

            return result;

            //var str = new StringBuilder();

            //str.AppendLine("-----------------------------------------------");
            //str.AppendLine("Position: " + position);
            //str.AppendLine("-----------------------------------------------");

            //var test1 = (uint)char1 >> position;
            //str.AppendLine(Convert.ToString((uint)char1 >> position, 2).PadLeft(8, '0'));

            //var testA = test1 & 1;
            //str.AppendLine(Convert.ToString(test1 & 1, 2).PadLeft(8, '0'));

            //var test2 = (uint)char2 >> position;
            //str.AppendLine(Convert.ToString((uint)char2 >> position, 2).PadLeft(8, '0'));

            //var testB = test2 & 1;
            //str.AppendLine(Convert.ToString(test2 & 1, 2).PadLeft(8, '0'));

            //while (testA == testB)
            //{
            //    position++;

            //    str.AppendLine("-----------------------------------------------");
            //    str.AppendLine("Position: " + position);
            //    str.AppendLine("-----------------------------------------------");

            //    test1 = (uint)char1 >> position;
            //    str.AppendLine(Convert.ToString((uint)char1 >> position, 2).PadLeft(8, '0'));

            //    testA = test1 & 1;
            //    str.AppendLine(Convert.ToString(test1 & 1, 2).PadLeft(8, '0'));

            //    test2 = (uint)char2 >> position;
            //    str.AppendLine(Convert.ToString((uint)char2 >> position, 2).PadLeft(8, '0'));

            //    testB = test2 & 1;
            //    str.AppendLine(Convert.ToString(test2 & 1, 2).PadLeft(8, '0'));
            //}

            //var test = str.ToString();

            //var result = index * 16 + position;
        }

        private bool SafeBitTest(string key, int position)
        {
            if (position < key.Length*16)
                return BitTest(key, position) != 0;

            if (position > key.Length*16 + 15)
                return false;

            return true;
        }

        private int BitTest(string key, int position)
        {
            var adjPosition = (int)((uint) position >> 4);
            var keyAtPosition = key[adjPosition];

            return (int)(((uint)keyAtPosition >> (position & 0xf)) & 1);
        }

        private int SafeCharAt(string key, int position)
        {
            if (position < key.Length)
                return key[position];

            if (position > key.Length)
                return 0x0000;

            return 0xffff;

            //return 0x0000;
        }
















        //private int FirstBitDifferent(string key1, string key2)
        //{
        //    if (string.IsNullOrWhiteSpace(key1) || string.IsNullOrWhiteSpace(key2))
        //        return 0;

        //    var index = 0;
        //    var position = 0;

        //    var char1 = SafeCharAt(key1, index);
        //    var char2 = SafeCharAt(key2, index);

        //    while (char1 == char2 && char1 != '\0' && char2 != '\0')
        //    {
        //        index++;

        //        char1 = SafeCharAt(key1, index);
        //        char2 = SafeCharAt(key2, index);
        //    }

        //    if (char1 == '\0' && char2 == '\0')
        //        return 0;

        //    while (GetBit(char1, position) == GetBit(char2, position))
        //    {
        //        position++;
        //    }

        //    var test1 = Convert.ToString(position, 2).PadLeft(8, '0');
        //    var test2 = Convert.ToString(index, 2).PadLeft(8, '0');
        //    var test3 = Convert.ToString(index << 3, 2).PadLeft(8, '0');
        //    var test4 = Convert.ToString((index << 3) + position, 2).PadLeft(8, '0');

        //    var result = (index << 3) + position;

        //    return result;
        //}

        //private int GetBit(char key, int position)
        //{
        //    //int k = position & 0x7;

        //    //string one = partial + (position >> 3);
        //    //int two = one >> k;
        //    //int three = two & 0x1;

        //    //return three;

        //    var sb = new StringBuilder();

        //    var test = Convert.ToString(key, 2).PadLeft(8, '0');
        //    sb.AppendLine("Key: " + test);

        //    var test1 = Convert.ToString(0x7, 2).PadLeft(8, '0');
        //    sb.AppendLine("0x7: " + test1);

        //    var test2 = Convert.ToString(position, 2).PadLeft(8, '0');
        //    sb.AppendLine("Position: " + test2);

        //    var test3 = Convert.ToString(position & 0x7, 2).PadLeft(8, '0');
        //    sb.AppendLine("Position & 0x7: " + test3);

        //    var test4 = Convert.ToString(position >> 3, 2).PadLeft(8, '0');
        //    sb.AppendLine("Position >> 3: " + test4);

        //    var test5 = Convert.ToString(key + (position >> 3), 2).PadLeft(8, '0');
        //    sb.AppendLine("Key + (Position >> 3): " + test5);

        //    var test6 = Convert.ToString((key + (position >> 3)) >> (position & 0x7), 2).PadLeft(8, '0');
        //    sb.AppendLine("(Key + (Position >> 3)) >> (Position & 0x7): " + test6);

        //    var test7 = Convert.ToString(((key + (position >> 3)) >> (position & 0x7)) & 0x1, 2).PadLeft(8, '0');
        //    sb.AppendLine("((Key + (Position >> 3)) >> (Position & 0x7)) & 0x1: " + test7);

        //    var result = ((key + (position >> 3)) >> (position & 0x7)) & 0x1;
        //    sb.AppendLine("Result: " + result);

        //    var final = sb.ToString();

        //    return result;
        //}

        //private char SafeCharAt(string key, int position)
        //{
        //    if (position < key.Length)
        //        return key[position];

        //    return '\0';
        //}

        //private static bool safeBitTest(string key, int position)
        //{
        //    if (position < key.Length*16)
        //        return bitTest(key, position) != 0;

        //    if (position > key.Length*16 + 15)
        //        return false;

        //    return true;
        //}

        //private static int bitTest(string key, int position)
        //{
        //    var charPosition = (int)((uint)position >> 4);

        //    var charAtPosition = key[charPosition];

        //    var unknown = position & 0xf;

        //    var result1 = (int)((uint)charAtPosition >> unknown);
        //    var result2 = result1 & 1;

        //    return result2;
        //}

        //private static int firstDifferingBit(string k1, string k2)
        //{
        //    int i = 0;
        //    int c1 = safeCharAt(k1, 0) & ~1;
        //    int c2 = safeCharAt(k2, 0) & ~1;

        //    if (c1 == c2)
        //    {
        //        i = 1;

        //        while (safeCharAt(k1, i) == safeCharAt(k2, i))
        //        {
        //            i++;
        //        }

        //        c1 = safeCharAt(k1, i);
        //        c2 = safeCharAt(k2, i);
        //    }

        //    int b = 0;
        //    while ((((uint)c1 >> b) & 1) == (((uint)c2 >> b) & 1))
        //    {
        //        b++;
        //    }

        //    return i*16 + b;
        //}
    }
}
