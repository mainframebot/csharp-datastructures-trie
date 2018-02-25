namespace SuffixTrie.Models
{
    public class Partition
    {
        public string Key { get; set; }

        public string KeyPart { get; set; }

        public int KeyPartLength { get; set; }

        public int KeyPartStart { get; set; }


        public Partition(string key, string keyPart, int keyPartLength, int keyPartStart)
        {
            Key = key;
            KeyPart = keyPart;
            KeyPartLength = keyPartLength;
            KeyPartStart = keyPartStart;
        }
    }
}
