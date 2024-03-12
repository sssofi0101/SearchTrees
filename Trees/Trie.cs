namespace Trees
{
    internal class Trie 
    {
        private TrieNode root;
        public Trie()
        {
            root = getTrieNode();
        }

        public TrieNode getTrieNode()
        {
            return new TrieNode();
        }

        public void TrieInsert(string key)
        {
            int length = key.Length;
            int i;
            TrieNode pCrawl = root;
            for (int level = 0; level < length; level++)
            {
                i = key[level] - 'a';    
                if (i < 0)              
                    break;
                if (pCrawl.children[i] == null)
                {
                    pCrawl.children[i] = getTrieNode(); 
                }
                pCrawl = pCrawl.children[i];
            }
            pCrawl.isEndOfWord = true;
        }

        public int TrieDelete(string key)
        {
            int length = key.Length;
            int i;
            TrieNode pCrawl = root;

            for (int level = 0; level < length; level++)
            {
                i = key[level] - 'a';    
                if (i < 0)              
                    break;
                if (pCrawl.children[i] == null)
                {
                    pCrawl.children[i] = getTrieNode(); 
                }
                pCrawl = pCrawl.children[i];
            }
            if (pCrawl.isEndOfWord != true) { return 0; }
            else { pCrawl.isEndOfWord = false; return 1; };
        }

        public int TrieSearch(string key)
        {
            int level;
            int length = key.Length;
            int i;
            TrieNode pCrawl = root;

            for (level = 0; level < length; level++)
            {
                i = key[level] - 'a';
                if (pCrawl.children[i] == null) return 0;  
                pCrawl = pCrawl.children[i];
            }
            if (pCrawl != null)
            {
                if (pCrawl.isEndOfWord == true) return 1;   
                return 0;       
            }
            else return 0; 
        }

        public class TrieNode
        {
            const int ALPHABET_SIZE = 26;
            public TrieNode[] children = new TrieNode[ALPHABET_SIZE];
            public bool isEndOfWord;
            public TrieNode()
            {
                for (int i = 0; i < ALPHABET_SIZE; i++)
                {
                    children[i] = null; 
                }
            }
        }
    }
}
