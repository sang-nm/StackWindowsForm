using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int DUT(string dau)
        {
            switch (dau)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                default:
                    return 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int i;
            Stack st = new Stack();
            string postfix = "";
            for (i = 0; i < txtbieuthuc.Text.Length; i++)
            {
                string kytu = Convert.ToString(txtbieuthuc.Text[i]);
                if (isOperator(kytu)) //neu txtbieuthuc.Text[i] la dau
                {
                    if (st.empty())
                    {
                        st.Push(kytu);
                    }
                    else
                    {
                            string top = st.Pop();
                            if (DUT(kytu) <= DUT(top))
                            {
                                postfix += top + " ";
                                st.Push(kytu);
                            }
                            else
                            {
                                st.Push(top);
                                st.Push(kytu);
                            }
                        
                        
                    }
                }
                else if (kytu == "(")
                {
                    st.Push(kytu);
                }
                else if (kytu == ")")
                {
                    string p = st.Pop();
                    while (p != "(")
                    {
                        postfix += p + " ";
                        p = st.Pop();
                    }
                }
                else  //so
                {
                    postfix += txtbieuthuc.Text[i] + " ";
                }
            }

            if (!st.empty())
            {
                string p;
                do
                {
                    p = st.Pop();
                    postfix += p + " ";
                } while (!st.empty());
            }

            label2.Text = postfix;

            //2. Tinh postfix
            TinhPostfix(postfix);

        }


        private void TinhPostfix(string postfix)
        {

            Stack st = new Stack();
            string[] arrPox = postfix.Substring(0, postfix.Length - 1).Split(' ');
            for (int i = 0; i < arrPox.Length; i++)
            {
                if (isOperator(arrPox[i]))//dau
                {
                    //doc 2 so dau tien trong stack,  tinh ket qua, pust kq vo stack
                    int y= Convert.ToInt32(st.Pop());
                    int x= Convert.ToInt32(st.Pop());
                    int kq = TinhBieuThuc(x, y, arrPox[i]);
                    st.Push(kq.ToString());
                }
                else //so
                {
                    st.Push(arrPox[i]);
                }

            }
            while (!st.empty())
            {
                label3.Text += st.Pop();
            }


        }

        private int TinhBieuThuc(int x, int y, string dau)
        {
            switch (dau)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                default:
                    return 0;
            }
        }


        private bool isOperator(string item)
        {
            return (item == "+" || item == "-" || item == "*" || item == "/");

        }

    }
    class Node
    {
        public string Data { get; set; }
        public Node Next { get; set; }
    }
    class Stack
    {
        Node Top;
        public void Push(string item)
        {
            Node newnode = new Node();
            newnode.Data = item;
            if (Top == null)
            {
                Top = newnode;
            }
            else
            {
                newnode.Next = Top;
                Top = newnode;
            }
        }
        public string Pop()
        {
            string kq = Top.Data;
            Top = Top.Next;
            return kq;
        }
        public string Peak()
        {
            string kq = Top.Data;
            return kq;
        }
        public bool empty()
        {
            return (Top == null);
        }
    }
}