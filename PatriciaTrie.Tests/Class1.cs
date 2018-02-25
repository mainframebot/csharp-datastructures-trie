using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PatriciaTrie.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Testing()
        {
            var trie = new PatriciaTrie<int>();

            trie.Insert("SOME", 1);
            trie.Insert("ABACUS", 2);
            trie.Insert("SOMETHING", 3);
            trie.Insert("B", 4);
            trie.Insert("ABRACADABRA", 5);
            trie.Insert("THIS", 6);
            trie.Insert("SOMERSET", 7);

            

            trie.Delete("ABACUS");

            var test1 = trie.Find("SOME");
            var test2 = trie.Find("ABACUS");
            var test3 = trie.Find("SOMETHING");
            var test4 = trie.Find("B");
            var test5 = trie.Find("ABRACADABRA");
            var test6 = trie.Find("THIS");
            var test7 = trie.Find("SOMERSET");
            var test8 = trie.Find("SQUIRREL");
        }
    }
}
