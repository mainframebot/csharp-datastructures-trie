using NUnit.Framework;

namespace RadixTrie.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Testing()
        {
            var tree = new RadixTrie<int>();
            tree.Insert("romane", 0);
            tree.Insert("romanus", 1);
            tree.Insert("romulus", 2);
            tree.Insert("rubens", 3);
            tree.Insert("ruber", 4);
            tree.Insert("rubicon", 5);
            tree.Insert("rubicundus", 6);
        }
    }
}
