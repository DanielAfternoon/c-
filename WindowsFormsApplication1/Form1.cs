using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        const Int32 answerLength = 4;
        int[] answer = new int[4];
        int[] temp = new int[4];
        int t = 0;
       public int c = 0;
        public string s;
        public Form1()
        {
            InitializeComponent();  //初始化組件
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            rand();
            richTextBox1.Text = "現在電腦已想好了一個4位數(數字0~9且不重複)請你猜猜看，猜完後我會給你一個提示，例如：1A2B，表示其中有1個數字不但猜對了，位置也正確，另有2個數字被猜中了，但位置不正確，請用最少的次數把這個數猜出來吧！";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            // Scrolls the contents of the control to the current caret position.
            richTextBox1.ScrollToCaret();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            t = 0;
            label2.Enabled = true;
            label1.Enabled = true;
            textBox1.Enabled = true;
            timer1.Enabled = true;
            label3.Enabled = true;
            button1.Enabled = true;
            richTextBox1.Clear();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text.Length != answerLength)
            {
                    label3.Text = "輸入錯誤,請輸入四個位數\n";
                    textBox1.Clear();
            }
            else
                {
                label3.Text = "";//1234
                    int t = Convert.ToInt32(textBox1.Text);
                    temp[0] = t / 1000;//1
                    temp[1] =( t / 100)%10;//2
                    temp[2] =(t /10)%10 ;//3
                    temp[3] = t % 10;//4

                    int a = 0, b = 0;
                    for (int i = 0; i < 4; ++i)   //利用雙迴圈是判斷位置是否相同
                    {

                        for (int j = 0; j < 4; ++j)
                            if (temp[i] == answer[j])
                                if (i == j)   //如果相同 a就+1
                                    a++;
                                else
                                    b++;     //如果不同 b就+1
                    }
                    if (a == 4)//如果a=4代表數字都猜中位置也正確
                {
                    
                    c++;
                    s = label2.Text;
                    textBox1.Text = "";
                    richTextBox1.Text = "";
                    textBox1.Enabled = false;
                    timer1.Enabled = false;
                    button1.Enabled = false;
                    Form2 f2 = new Form2();//傳label2到表單2
                    f2.f1 = this;
                    f2.ShowDialog();//打開表單2
                    
                }
                    else
                    {
                    c++;
                        richTextBox1.Text += "第"+c+"次猜 "+textBox1.Text+"  "+a+"a"+b+"b" + "\n";
                        textBox1.Clear();
                    }
                }
            }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar > 47 & (int)e.KeyChar < 58)& textBox1.Text.IndexOf(e.KeyChar)<0 | (int)e.KeyChar==13| (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
                else
            {
                    e.Handled = true;//控制只能輸入0~9,且不可以輸入重複的數字
                    label3.Text = "請輸入0~9,且不重複的數";
            }
        }
        private void rand()
        {
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                answer[i] =rnd.Next(0, 10);   //產生亂數1~9之間
                for (int j = 0; j < i; j++)
                    if (answer[i] == answer[j])
                        i--;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)//計時
        {
            label2.Text ="計時(秒):  "+ t.ToString();
            t++;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//產生yes no視窗,詢問使用者要不要離開
        {
            DialogResult exit = MessageBox.Show("確定要離開嗎?", "離開", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (exit == DialogResult.Yes)
            {
                e.Cancel = false;
               
            }
            else if (exit == DialogResult.No)
                e.Cancel = true;
            
        }
         void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender,e);
            } 
        }
    }
}
