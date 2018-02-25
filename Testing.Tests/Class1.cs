using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Testing.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Testing()
        {
            var tree = new Tree();
            tree.Insert("romane");
            tree.Insert("romanus");
            tree.Insert("romulus");
            tree.Insert("rubens");
            tree.Insert("ruber");
            tree.Insert("rubicon");
            tree.Insert("rubicundus");

            Console.WriteLine("predecessor of the word \"romanes\":" + tree.FindPredecessor("romanes"));
            Console.WriteLine("successor of the word \"rom\":" + tree.FindSuccessor("rom"));

            Console.WriteLine(tree.Lookup("romulus") ? "there" : "not there");

            tree.Delete("romanus");
        }
    }
}
