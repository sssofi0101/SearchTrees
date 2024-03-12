namespace Trees
{
    internal class TST
    {
        private TSTNode root;
        private readonly char chrNull = '\0';
        private readonly bool index = true;
        private bool add = false;
        private bool searchChar = false;
        public TST()
        {
            root = new TSTNode(chrNull);
        }
        public void TSTInsert(string str)
        {
            InsertSinceNode(ref root, str);
        }
        public int TSTSearch(string str)
        {
            return SearchSinceNode(ref root, str);
        }
        public int TSTDelete(string str)
        {
            return DeleteSinceNode(ref root, str);
        }
        private void InsertSinceNode(ref TSTNode cNode, string strWord)
        {
            if (strWord == "")
            {
                return;
            }
            if (add == false)
            {
                cNode.chr = chrNull;
                add = true;
            }
            if (strWord[0] > cNode.chr)
            {
                if (cNode.down != null)
                {
                    InsertSinceNode(ref cNode.down, strWord);
                }
                else
                {
                    TSTNode cDown = new TSTNode(strWord[0]);
                    cNode.down = cDown;
                    if (strWord.Length == 1)
                    {
                        InsertSinceNode(ref cDown, strWord);
                        cNode.index = index;
                    }
                    if (strWord.Length > 1)
                    {
                        InsertSinceNode(ref cDown, strWord);
                    }
                }
            }
            else if (strWord[0] < cNode.chr)
            {
                if (cNode.up != null)
                {
                    InsertSinceNode(ref cNode.up, strWord);
                }
                else
                {
                    TSTNode cUp = new TSTNode(strWord[0]);
                    cNode.up = cUp;
                    if (strWord.Length == 1)
                    {
                        InsertSinceNode(ref cUp, strWord);
                        cNode.index = index;
                    }
                    if (strWord.Length > 1)
                    {
                        InsertSinceNode(ref cUp, strWord);
                    }
                }
            }
            else if (strWord[0] == cNode.chr)
            {
                if (strWord.Length > 1)
                {
                    if (cNode.next != null)
                    {
                        if (strWord.Length == 1)
                        {
                            cNode.index = index;
                        }
                        InsertSinceNode(ref cNode.next, strWord.Substring(1));
                    }
                    else
                    {
                        TSTNode cNext = new TSTNode(strWord[1]);
                        cNode.next = cNext;
                        InsertSinceNode(ref cNext, strWord.Substring(1));
                    }
                }
                if (strWord.Length == 1)
                {
                    cNode.index = index;
                }
            }

        }
        private int SearchSinceNode(ref TSTNode cNode, string strWord)
        {
            if (strWord == "")
            {
                return 0;
            }
            if (searchChar == false)
            {
                cNode.chr = chrNull;
                searchChar = true;
            }
            int search = 0;
            if (cNode == null)
            {
                return search;
            }
            if (strWord[0] > cNode.chr)
            {
                if (cNode.down != null)
                {
                    return SearchSinceNode(ref cNode.down, strWord);
                }
                else
                {
                    searchChar = false;
                    return search;
                }
            }
            else if (strWord[0] < cNode.chr)
            {
                if (cNode.up != null)
                {
                    return SearchSinceNode(ref cNode.up, strWord);
                }
                else
                {
                    searchChar = false;
                    return search;
                }
            }
            else if (strWord[0] == cNode.chr)
            {
                if ((strWord.Length == 1) && (cNode.index == true))
                {
                    searchChar = false;
                    return search + 1;
                }
                else if ((strWord.Length == 1) && (cNode.index == false))
                {
                    searchChar = false;
                    return search;
                }
                else
                {
                    return SearchSinceNode(ref cNode.next, strWord.Substring(1));
                }
            }
            searchChar = false;
            return search;

        }

        private bool flag = false;
        private int DeleteSinceNode(ref TSTNode cNode, string strWord)
        {
            if (strWord == "")
            {
                flag = false;
                return 0;
            }
            int delete = 0;
            if (strWord[0] == 0)
            {
                flag = false;
                return 0;
            }
            if ((strWord != null) && (flag == true) && (cNode == null))
            {
                flag = false;
                return 0;
            }

            if (strWord[0] > cNode.chr)
            {
                if (cNode.down != null)
                {
                    flag = true;
                    return DeleteSinceNode(ref cNode.down, strWord);
                }
                else
                {
                    flag = false;
                    return delete;
                }

            }
            else if (strWord[0] < cNode.chr)
            {
                if (cNode.up != null)
                {
                    flag = true;
                    return DeleteSinceNode(ref cNode.up, strWord);
                }
                else
                {
                    flag = false;
                    return delete;
                }
            }
            else if (strWord[0] == cNode.chr)
            {
                if ((strWord.Length == 1) && (cNode.index == true))
                {
                    cNode.index = false;
                    return delete + 1;
                }
                else if ((strWord.Length == 1) && (cNode.index == false))
                {
                    flag = false;
                    return delete;
                }
                else
                {
                    flag = true;
                    return DeleteSinceNode(ref cNode.next, strWord.Substring(1));
                }
            }
            flag = false;
            return delete;
        }
    }
    public class TSTNode
    {
        public TSTNode down;
        public TSTNode up;
        public TSTNode next;
        public bool index;
        public char chr;
        public TSTNode(char c)
        {
            chr = c;
        }
        public TSTNode(bool i)
        {
            index = i;
        }
    }
}




