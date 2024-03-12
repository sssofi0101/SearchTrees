using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    internal class BST
    {
        private Node4 root;

        public BST()
        {
            root = null;
        }

        public void BSTInsert(string i)
        {
            Node4 newNode = new Node4();
            newNode.Data = i;
            if (root == null)
                root = newNode;
            else
            {
                Node4 current = root;
                Node4 parent;
                while (true)
                {
                    parent = current;
                    if (String.Compare(i, current.Data) < 0)
                    {
                        current = current.LeftChild;
                        if (current == null)
                        {
                            parent.LeftChild = newNode;
                            break;
                        }
                    }

                    if (String.Compare(i, current.Data) > 0)
                    {
                        current = current.RightChild;
                        if (current == null)
                        {
                            parent.RightChild = newNode;
                            break;
                        }
                    }
                    if (String.Compare(i, current.Data) == 0) { break; }
                }
            }
        }

        public int BSTDelete(string i)
        {
            Node4 current = root;
            Node4 parent;
            parent = current;
            if (current == null) { return 0; }
            while (current.Data != i)
            {
                if (String.Compare(i, current.Data) < 0) { parent = current; current = current.LeftChild; }
                else { parent = current; current = current.RightChild; }
                if (current == null) break;
            }
            if (current == null) { return 0; }
            else
            {
                if ((current.LeftChild == null) && (current.RightChild == null))
                {
                    if (current == root) { root = null; }
                    else
                    {
                        if ((parent.LeftChild != null) && (i == parent.LeftChild.Data)) { parent.LeftChild = null; }
                        else { parent.RightChild = null; }
                    }
                }
                else
                {
                    if ((current.LeftChild == null) || (current.RightChild == null))
                    {
                        if (current.LeftChild == null)
                        {
                            if (current == root) { root = current.RightChild; } else { parent.RightChild = current.RightChild; }
                        }
                        else 
                        {
                            if (current == root) { root = current.LeftChild; } else parent.LeftChild = current.LeftChild; 
                        }
                    }
                    else
                    {
                        if ((current.LeftChild != null) || (current.RightChild != null))
                        {
                            string temp = minValue(current.RightChild);
                            BSTDelete(temp);
                            current.Data = temp;
                        }
                    }
                }
                return 1;
            }
        }

        private string minValue(Node4 begining)
        {
            string minv = begining.Data;
            while (begining.LeftChild != null)
            {
                minv = begining.LeftChild.Data;
                begining = begining.LeftChild;
            }
            return minv;
        }

        public int BSTSearch(string i)
        {
            if (root == null) { return 0; }
            Node4 current = root;
            bool found = false;
            while (current != null)
            {
                Repetition(i, current, out Node4 newcurrent, out found); 
                current = newcurrent;
            }
            if (found == true) { return 1; } else { return 0; }

        }


        private void Repetition(string i, Node4 current, out Node4 newcurrent, out bool found)
        {
            found = false;
            newcurrent = null;
            if (current == null) { found = false; return; }
            if (i == current.Data) { found = true; return; }
            else
            {
                if (String.Compare(i, current.Data) < 0) { current = current.LeftChild; }
                else
                {
                    if (String.Compare(i, current.Data) > 0)
                    {
                        current = current.RightChild;
                    }
                }
            }
            newcurrent = current;
        }
    }

    public class Node4
    {
        public string Data;
        public Node4 LeftChild;
        public Node4 RightChild;
    }

}