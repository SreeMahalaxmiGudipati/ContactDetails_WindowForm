using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form2proj
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        
        contactClass c = new contactClass(); 
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                ErrorProvider errorProvider = new ErrorProvider();
                errorProvider.SetError(textBox1, "ContactID required");
            }
            else if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                ErrorProvider errorProvider = new ErrorProvider();
                errorProvider.SetError(textBox4, "First Name required");
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                ErrorProvider errorProvider = new ErrorProvider();
                errorProvider.SetError(textBox2, "Contact Number required");
            }

            else 
            {  
            c.ContactID = int.Parse(textBox1.Text);
            c.FirstName = textBox4.Text;
            c.LastName = textBox3.Text;
            c.ContactNo = textBox2.Text;
            c.Address = richTextBox1.Text;
            c.Gender = comboBox1.Text;

            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("Successfully inserted");
                Clear();
            }
            else
            {
                MessageBox.Show("Failes to insert.Try again.");
            }

            DataTable dt = c.Select();
            dataGridView1.DataSource= dt;
            }
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;
        }
        public void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            richTextBox1.Text = "";
            textBox5.Text = "";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            textBox1.Text= dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
            richTextBox1.Text= dataGridView1.Rows[rowindex].Cells[4].Value.ToString();
            comboBox1.Text=dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            c.ContactID=int.Parse(textBox1.Text);
            c.FirstName = textBox4.Text;
            c.LastName = textBox3.Text;
            c.ContactNo = textBox2.Text;
            c.Address = richTextBox1.Text;
            c.Gender = comboBox1.Text;
            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("successfully updated");
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failes to insert.Try again.");
            }
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            c.ContactID= int.Parse(textBox1.Text);

            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("successfully deleted");
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failes to insert.Try again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox5.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlDataAdapter sda=new SqlDataAdapter("SELECT * FROM contact1 where FirstName LIKE '%"+keyword+ "%' OR LastName LIKE '%"+keyword+ "%' OR Address LIKE '%"+keyword+"%'",conn);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.TextLength > 0)
                {
                    textBox4.Focus();
                }
                else
                {
                    textBox1.Focus();
                }
            }
        }

        private void textBox4_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox4.TextLength > 0)
                {
                    textBox3.Focus();
                }
                else
                {
                    textBox4.Focus();
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox4.TextLength > 0)
                {
                    textBox2.Focus();
                }
                else
                {
                    textBox4.Focus();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox4.TextLength > 0)
                {
                    richTextBox1.Focus();
                }
                else
                {
                    textBox4.Focus();
                }
            }
        }
    }
}
