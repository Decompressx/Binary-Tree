using System;
using System.Collections.Generic;
using System.Collections;

namespace Laba_BinaryTree_
{
    class BinaryTree<T> : IEnumerable<T>
    {
        protected class Node<TData>
        {
            public TData Data {get;set;}
            public Node<TData> Left {get;set;}
            public Node<TData> Right{get;set;}

            public Node(TData data)
            {
                Data = data;
            }
        }
        protected Node<T> root;
        protected IComparer<T> comparer;

        public IEnumerator<T> GetEnumerator()
        {
            return LCR().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public BinaryTree() : this(Comparer<T>.Default)                                       //Нашел               
        {                                                                                     // 
        }                                                                                     //реализацю  comparera                
        public BinaryTree(IComparer<T> defaultComparer)                                       //              
        {                                                                                     //в
            if (defaultComparer == null)                                                      //
                throw new ArgumentNullException("По умолчанию 0");                            //Интернете
            comparer = defaultComparer;                                                       //
        }

        public void Clear()
        { root = null; }

        public IEnumerable<T> CLR()//прямой
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                Node<T> node = stack.Pop();
                yield return node.Data;
                if (node.Right != null)
                    stack.Push(node.Right);
                if (node.Left != null)
                    stack.Push(node.Left);
            }
        }

        public IEnumerable<T> LCR()//обратный
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> node = root;

            while (stack.Count > 0 | node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node.Data;
                    node = node.Right;
                }
                else
                {
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }
 
        public IEnumerable<T> LRC()//симметричный
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> node = root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else
                    {
                        yield return node.Data;
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                        stack.Push(node.Right);
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }

        public void Add(T item)
        {
            Node<T> node = new Node<T>(item);

            if (root == null)
                root = node;
            else
            {
                Node<T> current = root;
                Node<T> dad = null;

                while (current != null)
                {
                    dad = current;
                    if (comparer.Compare(item, current.Data) < 0)
                        current = current.Left;
                    else
                        current = current.Right;
                }

                if (comparer.Compare(item, dad.Data) < 0)
                    dad.Left = node;
                else
                    dad.Right = node;
            }
        }
        public bool Remove(T delElem)
        {
            if (root == null)
                return false;

                Node<T> current = root;
                Node<T> dad = null;
                int Elem;
                Elem = comparer.Compare(delElem, current.Data);
                while (Elem != 0)
                {
                    if (Elem < 0)
                    {
                        dad = current;
                        current = current.Left;
                    }
                    else if (Elem > 0)
                    {
                        dad = current;
                        current = current.Right;
                    }
                    if (current == null)
                        return false;
                    Elem = comparer.Compare(delElem, current.Data);
                }

                if (current.Right == null)
                {
                    if (current == root)
                        root = current.Left;
                    else
                    {
                        Elem = comparer.Compare(current.Data, dad.Data);
                        if (Elem < 0)
                            dad.Left = current.Left;
                        else
                            dad.Right = current.Left;
                    }
                }
                else
                if (current.Right.Left == null)
                {
                    current.Right.Left = current.Left;
                    if (current == root)
                        root = current.Right;
                    else
                    {
                        Elem = comparer.Compare(current.Data, dad.Data);
                        if (Elem < 0)
                            dad.Left = current.Right;
                        else
                            dad.Right = current.Right;
                    }
                }
            else
            {
                Node<T> Previos = current.Right;
                Node<T> PreviosMin = current.Right.Left;
                while (PreviosMin.Left != null)
                {
                    Previos = PreviosMin;
                    PreviosMin = Previos.Left;
                }
                Previos.Left = PreviosMin.Right;
                PreviosMin.Left = current.Left;
                PreviosMin.Right = current.Right;

                if (current == root)
                    root = PreviosMin;
                else
                {
                    Elem = comparer.Compare(current.Data, dad.Data);
                    if (Elem < 0)
                        dad.Left = PreviosMin;
                    else
                        dad.Right = PreviosMin;
                }
            }
            return true;
        }
    }
}
