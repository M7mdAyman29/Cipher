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
    public partial class Playfair : Form
    {
        public Playfair()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String str = textBox1.Text.ToString();
            if (check_PF_str(str) == 1)
            {
                label1.Text = null;
                label1.Text = PlayFair_encrypt(str);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            String str = textBox1.Text.ToString();
            label1.Text = null;
            label1.Text = PlayFair_decrypt(str);

        }
        private int check_PF_str(string str)
        {
  
            for(int j = 0; j < str.Length; j++) 
            {
                if (!(((str[j] >= 'a') && (str[j] <= 'z')) || ((str[j] >= 'A') && (str[j] <= 'Z')) || (str[j] == ' ')))
                {
                    MessageBox.Show("Error: Pleaase enter a valid Play Fair message", "ERROR");
                    return 0;
                }
            }

            return 1;
        }
        String PlayFair_encrypt(String str)
        {
            StringBuilder tmp_str = new StringBuilder(str.ToUpper());
            int[] space_indexes = new int[20];
            int space_count = 0;

            //first we remove any space in the message
            for (int count1 = 0; count1 < tmp_str.Length; count1 += 1)
            { 
                if (tmp_str[count1] == ' ')
                {
                    tmp_str = tmp_str.Remove(count1, 1);
                    space_indexes[space_count] = count1;
                    space_count++;
                }

            }
            space_indexes[space_count] = -1;

            for (int count1 = 0; count1 < tmp_str.Length; count1 += 1)
            {

                if (tmp_str[count1] == 'j')
                {
                    tmp_str[count1] = 'i';
                }

                if (tmp_str[count1] == 'J')
                {
                    tmp_str[count1] = 'I';
                }

            }
            for (int count1 = 0; ((count1 < tmp_str.Length) && ((count1 + 1) < tmp_str.Length)); count1 += 2)
            {

                if (tmp_str[count1] == tmp_str[count1 + 1])
                {
                    tmp_str.Insert(count1 + 1, "x");
                }

            }
            if ((tmp_str.Length % 2) == 1)
            {
                tmp_str.Append("x");
            }

            for (int count1 = 0; count1 < tmp_str.Length; count1++)
            {

                if (tmp_str[count1] >= 'a' && tmp_str[count1] <= 'z') //if the letter is small letter
                {
                    tmp_str[count1] -= (char)((int)'a' - (int)'A'); //convert it into capital letter
                }

            }
            String key = textBox2.Text.ToString();
            StringBuilder tmp_key = new StringBuilder(key);
            for (int count1 = 0; count1 < tmp_key.Length; count1 += 1)
            {

                if (tmp_key[count1] == 'j')
                {
                    tmp_key[count1] = 'i';
                }

                if (tmp_key[count1] == 'J')
                {
                    tmp_key[count1] = 'I';
                }

            }
            for (int count1 = 0; count1 < tmp_key.Length; count1++)
            {
                for (int count2 = 0; count2 < count1; count2++)
                {

                    if (tmp_key[count1] == tmp_key[count2]) 
                    {
                        tmp_key = tmp_key.Remove(count1, 1); 
                        count1--;
                        break;
                    }

                }

            }
            for (int count1 = 0; count1 < tmp_key.Length; count1++)
            {

                if (tmp_key[count1] >= 'a' && tmp_key[count1] <= 'z') 
                {
                    tmp_key[count1] -= (char)((int)'a' - (int)'A'); 
                }

            }

            int keyword_stored_flag = 0; 
            int exists_in_keyord = 0; 

            char[,] matrix = new char[5, 5];

            for (int row_count = 0, alphabet_counter = 0; row_count < 5; row_count++)
            {

                for (int col_count = 0; col_count < 5; col_count++)
                {

                    if ((((row_count * 5) + col_count) < tmp_key.Length) && (keyword_stored_flag == 0))
                    {
                        matrix[row_count, col_count] = tmp_key[(row_count * 5) + col_count];
                    }
                    else 
                    {
                        keyword_stored_flag = 1;
                        exists_in_keyord = 0; 
                        for (int count1 = 0; count1 < tmp_key.Length; count1++)
                        {

                            if ('A' + alphabet_counter == tmp_key[count1]) 
                            {
                                exists_in_keyord = 1;
                                break;
                            }

                        }
                        if ((exists_in_keyord == 0) && (('A' + alphabet_counter) != 'J'))
                        {
                            matrix[row_count, col_count] = (char)((int)'A' + alphabet_counter);
                        }
                        else
                        {
                            col_count--;
                        }

                        alphabet_counter++;
                    }

                }

            }
            int letter1_row = 0, letter1_col = 0, letter2_row = 0, letter2_col = 0;

            for (int m_count = 0; m_count < tmp_str.Length; m_count += 2)
            {

                get_index(matrix, tmp_str[m_count], ref letter1_row, ref letter1_col);
                get_index(matrix, tmp_str[m_count + 1], ref letter2_row, ref letter2_col);

                if (letter1_row == letter2_row)
                {
                    tmp_str[m_count] = matrix[letter1_row, (letter1_col + 1) % 5];
                    tmp_str[m_count + 1] = matrix[letter2_row, (letter2_col + 1) % 5];
                }
                else if (letter1_col == letter2_col)
                {
                    tmp_str[m_count] = matrix[(letter1_row + 1) % 5, letter1_col];
                    tmp_str[m_count + 1] = matrix[(letter2_row + 1) % 5, letter2_col];
                }
                else
                {
                    tmp_str[m_count] = matrix[letter1_row, letter2_col];
                    tmp_str[m_count + 1] = matrix[letter2_row, letter1_col];
                }

            }

            for (int count = 0; space_indexes[count] != -1; count++)
            {
                tmp_str.Insert(space_indexes[count] + count, " ");
            }

            str = tmp_str.ToString();

            return str;


        }

        String PlayFair_decrypt(String str)
        {
            StringBuilder tmp_str = new StringBuilder(str.ToUpper());
            int[] space_indexes = new int[20];
            int space_count = 0;

            for (int count1 = 0; count1 < tmp_str.Length; count1 += 1)
            {

                if (tmp_str[count1] == ' ')
                {
                    tmp_str = tmp_str.Remove(count1, 1);
                    space_indexes[space_count] = count1;
                    space_count++;
                }

            }

            space_indexes[space_count] = -1;

            String key = textBox2.Text.ToString();
            StringBuilder tmp_key = new StringBuilder(key.ToUpper());

            for (int count1 = 0; count1 < tmp_key.Length; count1 += 1)
            {

                if (tmp_key[count1] == 'J')
                {
                    tmp_key[count1] = 'I';
                }

            }

            for (int count1 = 0; count1 < tmp_key.Length; count1++)
            {

                for (int count2 = 0; count2 < count1; count2++)
                {

                    if (tmp_key[count1] == tmp_key[count2])
                    {
                        tmp_key = tmp_key.Remove(count1, 1); 
                        count1--;
                        break; 
                    }

                }

            }

            int keyword_stored_flag = 0; 
            int exists_in_keyord = 0; 
            char[,] matrix = new char[5, 5];

            for (int row_count = 0, alphabet_counter = 0; row_count < 5; row_count++)
            {

                for (int col_count = 0; col_count < 5; col_count++)
                {

                    if ((((row_count * 5) + col_count) < tmp_key.Length) && (keyword_stored_flag == 0))
                    {
                        matrix[row_count, col_count] = tmp_key[(row_count * 5) + col_count];
                    }
                    else 
                    {
                        keyword_stored_flag = 1;
                        exists_in_keyord = 0;
                        for (int count1 = 0; count1 < tmp_key.Length; count1++)
                        {

                            if ('A' + alphabet_counter == tmp_key[count1])
                            { 
                                exists_in_keyord = 1;
                                break;
                            }

                        }
                        if ((exists_in_keyord == 0) && (('A' + alphabet_counter) != 'J'))
                        {
                            matrix[row_count, col_count] = (char)((int)'A' + alphabet_counter);
                        }
                        else
                        {
                            col_count--;
                        }

                        alphabet_counter++;
                    }

                }

            }
            int letter1_row = 0, letter1_col = 0, letter2_row = 0, letter2_col = 0;

            for (int m_count = 0; m_count < tmp_str.Length; m_count += 2)
            {

                get_index(matrix, tmp_str[m_count], ref letter1_row, ref letter1_col);
                get_index(matrix, tmp_str[m_count + 1], ref letter2_row, ref letter2_col);

                if (letter1_row == letter2_row)
                {
                    tmp_str[m_count] = matrix[letter1_row, (letter1_col + 4) % 5];
                    tmp_str[m_count + 1] = matrix[letter2_row, (letter2_col + 4) % 5];
                }
                else if (letter1_col == letter2_col)
                {
                    tmp_str[m_count] = matrix[(letter1_row + 4) % 5, letter1_col];
                    tmp_str[m_count + 1] = matrix[(letter2_row + 4) % 5, letter2_col];
                }
                else
                {
                    tmp_str[m_count] = matrix[letter1_row, letter2_col];
                    tmp_str[m_count + 1] = matrix[letter2_row, letter1_col];
                }


            }

            for (int i = tmp_str.Length - 1; i >= 0; i--)
            {
                if (tmp_str[i] == 'X')
                {

                    if (i > 0)
                    {
                        if (i == (tmp_str.Length - 1)) 
                        {
                            tmp_str.Remove(i, 1); 
                        }
                        else if (tmp_str[i - 1] == tmp_str[i + 1]) 
                        {
                            tmp_str.Remove(i, 1);
                        }
                    }

                }
            }

            for (int count = 0; space_indexes[count] != -1; count++)
            {
                tmp_str.Insert(space_indexes[count] + count, " ");
            }

            str = tmp_str.ToString();
            return str;
        }

        void get_index(char[,] matrix, char chr, ref int row, ref int col)
        {
            for (int row_count = 0, char_match_flag = 0; char_match_flag == 0; row_count++)
            {

                for (int col_count = 0; col_count < 5; col_count++)
                {

                    if (matrix[row_count, col_count] == chr)
                    {
                        char_match_flag = 1;
                        col = col_count;
                        row = row_count;
                        break;
                    }

                }

            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}