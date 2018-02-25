using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SuffixTrie.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Testing()
        {
            var suffixTrie = new SuffixTrie();

            suffixTrie.Insert("Pokemon");
        }
    }
}
