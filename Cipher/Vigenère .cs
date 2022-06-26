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
    public partial class Vigenère : Form
    {
        public Vigenère()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
			label1.Text= Cipher(textBox1.Text,textBox2.Text, true);

		}
		private void button2_Click(object sender, EventArgs e)
        {
			label1.Text = Cipher(textBox1.Text, textBox2.Text, false);
		}
		private static int Mod(int a, int b)
		{
			return (a % b + b) % b;
		}

		private static string Cipher(string input, string key, bool encipher)
		{
			for (int i = 0; i < key.Length; ++i)
				if (!char.IsLetter(key[i]))
					return null; // Error

			string output = string.Empty;
			int nonAlphaCharCount = 0;

			for (int i = 0; i < input.Length; ++i)
			{
				if (char.IsLetter(input[i]))
				{
					bool cIsUpper = char.IsUpper(input[i]);
					char offset = cIsUpper ? 'A' : 'a';
					int keyIndex = (i - nonAlphaCharCount) % key.Length;
					int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
					k = encipher ? k : -k;
					char ch = (char)((Mod(((input[i] + k) - offset), 26)) + offset);
					output += ch;
				}
				else
				{
					output += input[i];
					++nonAlphaCharCount;
				}
			}

			return output;
		}

        private void button3_Click(object sender, EventArgs e)
        {
			Form form = new Form1();
			form.Show();
			this.Hide();
		}
    }
}
