using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TSTTrie.Tests.TSTTrie
{
    [TestFixture]
    public class Base
    {
        [Test]
        public void Testing()
        {
            TSTTrie<int> trie = new TSTTrie<int>();

            trie.Insert("she", 0);
            trie.Insert("sells", 1);
            trie.Insert("sea", 2);
            trie.Insert("shells", 3);
            trie.Insert("by", 4);
            trie.Insert("the", 5);
            trie.Insert("sea", 6);
            trie.Insert("shore", 7);
        }
    }
}
