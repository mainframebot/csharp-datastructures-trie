using System;
using RadixTrie.Models;

namespace RadixTrie
{
    public class RadixTrie<T>
    {
        public Node<T> Root { get; set; }

        public RadixTrie()
        {
            Root = new Node<T>() { Text = "" };
        }

        public void Insert(string word, T value)
        {
            if(string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException();

            Insert(word, Root, value);
        }

        private void Insert(string partition, Node<T> node, T value)
        {
            var matchedCharacters = 0;
                
            var matchPartitionType = MatchPartitionCharacters(partition, node, out matchedCharacters);

            switch (matchPartitionType)
            {
                case MatchType.None:
                case MatchType.IsRoot:
                case MatchType.Partial:
                    InsertWhenPartitionIsPartial(partition, node, value, matchedCharacters);
                    break;
                case MatchType.Contains:
                    InsertWhenPartitionContainsMatches(partition, node, matchedCharacters);
                    break;
                case MatchType.IsContained:
                    InsertWhenCurrentIsContainedInMatches(partition, node, matchedCharacters);
                    break;

            }
        }

        private void InsertWhenPartitionIsPartial(string partition, Node<T> node, T value, int matchedCharacters)
        {
            var inserted = false;

            var newPartition = partition.Substring(matchedCharacters, partition.Length - matchedCharacters);

            foreach (var child in node.Children)
            {
                if (child.Text.StartsWith(newPartition[0].ToString()))
                {
                    inserted = true;
                    Insert(newPartition, child, value);
                }
            }

            if(inserted == false)
                node.Children.Add(new Node<T>() { Text = newPartition });
        }

        private void InsertWhenPartitionContainsMatches(string partition, Node<T> node, int matchedCharacters)
        {
            var commonPartition = partition.Substring(0, matchedCharacters);
            var prevPartition = node.Text.Substring(matchedCharacters, node.Text.Length - matchedCharacters);
            var newPartition = partition.Substring(matchedCharacters, partition.Length - matchedCharacters);

            node.Text = commonPartition;

            var newChildBasedOnPrevPartition = new Node<T>() { Text = prevPartition };
            var newChildBasedOnNewPartition = new Node<T>() { Text = newPartition };

            newChildBasedOnPrevPartition.Children.AddRange(node.Children);

            node.Children.Clear();

            node.Children.Add(newChildBasedOnPrevPartition);
            node.Children.Add(newChildBasedOnNewPartition);
        }

        private void InsertWhenCurrentIsContainedInMatches(string partition, Node<T> node, int matchedCharacters)
        {
            var newText = node.Text.Substring(node.Text.Length, partition.Length);
            var newNode = new Node<T>() { Text = newText };

            node.Children.Add(newNode);
        }

        private MatchType MatchPartitionCharacters(string partition, Node<T> node, out int matchedCharacters)
        {
            var length = node.Text.Length >= partition.Length ? partition.Length : node.Text.Length;

            matchedCharacters = MatchedPartitionCharactersCount(partition, node.Text, length);

            return MatchedPartitionType(partition, node, matchedCharacters);
        }

        private MatchType MatchedPartitionType(string partition, Node<T> node, int matchedCharacters)
        {
            var matchedType = MatchType.None;

            if (Root == node)
            {
                matchedType = MatchType.IsRoot;
            }
            else if (matchedCharacters == 0)
            {
                matchedType = MatchType.None;
            }
            else if (
                (matchedCharacters > 0) &&
                (matchedCharacters < partition.Length) &&
                (matchedCharacters >= node.Text.Length))
            {
                matchedType = MatchType.Partial;
            }
            else if (matchedCharacters < partition.Length)
            {
                matchedType = MatchType.Contains;
            }
            else if (matchedCharacters == node.Text.Length)
            {
                matchedType = MatchType.ExactMatch;
            }
            else if (matchedCharacters > node.Text.Length)
            {
                matchedType = MatchType.IsContained;
            }

            return matchedType;
        }

        private int MatchedPartitionCharactersCount(string partition, string current, int length)
        {
            var matchedCharacters = 0;

            if (length > 0)
            {
                for (var i = 0; i < length; i++)
                {
                    if (partition[i] == current[i])
                    {
                        matchedCharacters++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return matchedCharacters;
        }

        public enum MatchType
        {
            None,
            IsRoot,
            ExactMatch,
            IsContained,
            Contains,
            Partial
        }
    }
}
