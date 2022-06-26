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
    public partial class Auto_Key : Form
    {
        public Auto_Key()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = null;
            string Message = textBox1.Text.ToLower();
            string[] mess;
            mess = Message.Split(' ');
            Message = null;
            foreach (string m in mess)
            {
                Message += m;
            }
            string Key = textBox2.Text.ToLower();
            string[] ke;
            ke = Key.Split(' ');
            Key = null;
            foreach (string k in ke)
            {
                Key += k;
            }
            List<char> alphabet = Enumerable.Range('a', 'z' - 'a' + 1).Select(x => (char)x).ToList();
            char[][] tabulaRecta = new char['z' - 'a' + 1][];
            for (int i = 0; i < tabulaRecta.Length; i++)
            {
                tabulaRecta[i] = alphabet.ToArray();
                var first = alphabet.First();
                alphabet.Remove(first);
                alphabet.Insert(alphabet.Count, first);
            }
            label1.Text = Cipher(Message, tabulaRecta, Key);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = null;
            string Message = textBox1.Text.ToLower();
            string[] mess;
            mess = Message.Split(' ');
            Message = null;
            foreach (string m in mess)
            {
                Message += m;
            }
            string Key = textBox2.Text.ToLower();
            string[] ke;
            ke = Key.Split(' ');
            Key = null;
            foreach (string k in ke)
            {
                Key += k;
            }
            List<char> alphabet = Enumerable.Range('a', 'z' - 'a' + 1).Select(x => (char)x).ToList();
            char[][] tabulaRecta = new char['z' - 'a' + 1][];
            for (int i = 0; i < tabulaRecta.Length; i++)
            {
                tabulaRecta[i] = alphabet.ToArray();
                var first = alphabet.First();
                alphabet.Remove(first);
                alphabet.Insert(alphabet.Count, first);
            }
            label1.Text = Decipher(Message, tabulaRecta, Key);
        }
        private static char[][] TransposeMatrix(char[][] matrix)
        {
            char[][] result = new char[matrix[0].Length][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new char[matrix.Length];
            }

            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    result[col][row] = matrix[row][col];
                }
            }

            return result;
        }
        private static int IndexOf(char[] array, char toFind)
        {
            int result = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == toFind)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
        private static string Cipher(
          string clearText, char[][] tabulaRecta, string keyword)
        {
            string result = string.Empty;

            keyword += clearText;
            keyword = keyword.Substring(0, clearText.Length);

            for (int i = 0; i < clearText.Length; i++)
            {
                int row = clearText[i] - 'a';
                int col = keyword[i] - 'a';

                result += tabulaRecta[row][col];
            }

            return result;
        }
        private static string Decipher(
            string cipherText, char[][] tabulaRecta, string keyword)
        {
            string result = string.Empty;

            tabulaRecta = TransposeMatrix(tabulaRecta);

            for (int i = 0; i < cipherText.Length; i++)
            {
                int row = keyword[i] - 'a';
                int col = IndexOf(tabulaRecta[row], cipherText[i]);

                result += tabulaRecta[0][col];
                keyword += tabulaRecta[0][col];
            }

            return result;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
