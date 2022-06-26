using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cipher
{
    public partial class Ceaser : Form
    {
        public Ceaser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = null;
            int key = int.Parse(textBox2.Text);
            foreach (char ch in textBox1.Text)
                label1.Text += cipher(ch, key);
        }
        
        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }
            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = null;
            int key = 26 - int.Parse(textBox2.Text);
            foreach (char ch in textBox1.Text)
                label1.Text += cipher(ch, key);

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
