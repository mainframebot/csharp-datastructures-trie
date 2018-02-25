using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Trie.Tests.Trie
{
    [TestFixture]
    public class Base
    {
        [Test]
        public void Testing()
        {
            var trie = new Trie<int>();

            trie.Insert("she", 0);
            trie.Insert("sells", 1);
            trie.Insert("sea", 2);
            trie.Insert("shells", 3);
            trie.Insert("by", 4);
            trie.Insert("the", 5);
            trie.Insert("sea", 6);
            trie.Insert("shore", 7);

            var test = trie.Search("Tests");
            var test1 = trie.Search("sea");

            var test2 = trie.Keys();
            var test3 = trie.KeysWithPrefix("sh");

            var test4 = trie.KeysThatMatch(".he");

            var test5 = trie.SearchForLongestPrefixOf("shellsort");

            trie.Delete("she");
            trie.Delete("by");

            var test6 = trie.Search("the");
            var test7 = trie.Search("she");
            var test8 = trie.Search("by");
        }
    }
}
