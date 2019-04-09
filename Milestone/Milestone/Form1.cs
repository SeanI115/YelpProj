/*
 * Sean Inouye and Jhenna Foronda 
 * Cpts451 - Milestone1
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Media;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Milestone
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            changeState();
            addUsers();

            radioButton1.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButton3.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButton4.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButton5.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
        }

        string connstring = "Server=localhost; Username= postgres; Password=Sweety12; Database=milestone2";

        private void Form1Load(object sender, EventArgs e)
        {

        }

        private void changeState()
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct state FROM business ORDER BY state;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString(0));
                        }

                    }
                }
            }
        }

        private void addUsers()
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct name FROM acc ORDER BY name;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            listBox3.Items.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
        }

        private void changeCity()
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct city FROM business ORDER BY city;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader.GetString(0));
                        }

                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            comboBox2.Items.Clear();
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct city FROM business WHERE state = '" + comboBox1.SelectedItem.ToString().ToUpper() + "' ORDER by city;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader.GetString(0));
                        }

                    }
                }
                conn.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                comboBox3.Items.Clear();
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct zipcode FROM business WHERE city = '" + comboBox2.SelectedItem.ToString() + "' ORDER BY zipcode;";
                    //cmd.CommandText = "SELECT name, state, city FROM business WHERE city = '"+ comboBox2.SelectedItem.ToString() + "' AND state = '" + comboBox1.SelectedItem.ToString() + "'ORDER BY name;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox3.Items.Add(reader.GetString(0));
                            //this.dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                        }

                    }
                }
                conn.Close();
            }
            //comboBox2.Items.Clear();
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                listBox1.Items.Clear();
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct category_name FROM categories WHERE business_id IN (SELECT business_id FROM business WHERE zipcode= '" + comboBox3.SelectedItem.ToString() + "') ORDER BY category_name;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader.GetString(0));
                        }
                    }
                }

                conn.Close();
                /*
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, stars FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category = '" + listBox1.SelectedItem.ToString() + "'); ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while(reader.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = reader.GetString(0);
                            dataGridView1.Rows[i].Cells[1].Value = reader.GetString(1);
                            dataGridView1.Rows[i].Cells[2].Value = reader.GetString(2);
                            dataGridView1.Rows[i].Cells[3].Value = reader.GetString(3);
                            dataGridView1.Rows[i].Cells[4].Value = reader.GetString(4);
                            dataGridView1.Rows[i].Cells[5].Value = reader.GetInt32(5);
                            dataGridView1.Rows[i].Cells[6].Value = reader.GetInt32(6);
                            dataGridView1.Rows[i].Cells[7].Value = reader.GetInt32(7);
                            dataGridView1.Rows[i].Cells[8].Value = reader.GetBoolean(8);
                            dataGridView1.Rows[i].Cells[9].Value = reader.GetDouble(9);
                            i++;
                        }
                    }
                }
                conn.Close();
                */
               

            }
        }

        private void updateGrid()
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    //string[] blist = listBox4.Items.OfType<string>().ToArray();
                    var blist = new List<string>();
                    foreach(var item in listBox4.Items)
                    {
                        blist.Add(item.ToString());
                    }
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox1.SelectedItem.ToString() + "'); ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();
                        int i = 0;
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = reader.GetString(0);
                            dataGridView1.Rows[i].Cells[1].Value = reader.GetString(1);
                            dataGridView1.Rows[i].Cells[2].Value = reader.GetString(2);
                            dataGridView1.Rows[i].Cells[3].Value = reader.GetString(3);
                            dataGridView1.Rows[i].Cells[4].Value = reader.GetString(4);
                            dataGridView1.Rows[i].Cells[5].Value = reader.GetInt32(5);
                            dataGridView1.Rows[i].Cells[6].Value = reader.GetInt32(6);
                            dataGridView1.Rows[i].Cells[7].Value = reader.GetInt32(7);
                            dataGridView1.Rows[i].Cells[8].Value = reader.GetBoolean(8);
                            dataGridView1.Rows[i].Cells[9].Value = reader.GetDouble(9);
                            i++;
                        }
                    }
                }
                conn.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*   
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox1.SelectedItem.ToString() + "'); ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();
                        int i = 0;
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = reader.GetString(0);
                            dataGridView1.Rows[i].Cells[1].Value = reader.GetString(1);
                            dataGridView1.Rows[i].Cells[2].Value = reader.GetString(2);
                            dataGridView1.Rows[i].Cells[3].Value = reader.GetString(3);
                            dataGridView1.Rows[i].Cells[4].Value = reader.GetString(4);
                            dataGridView1.Rows[i].Cells[5].Value = reader.GetInt32(5);
                            dataGridView1.Rows[i].Cells[6].Value = reader.GetInt32(6);
                            dataGridView1.Rows[i].Cells[7].Value = reader.GetInt32(7);
                            dataGridView1.Rows[i].Cells[8].Value = reader.GetBoolean(8);
                            dataGridView1.Rows[i].Cells[9].Value = reader.GetDouble(9);
                            i++;
                        }
                    }
                }
                conn.Close();
            }
            */
            listBox5.Items.Add(listBox1.SelectedItem.ToString());

        }

        string bId = null;
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var val = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            textBox2.Clear();
            //listBox2.Items.Clear();
            bId = val.ToString();

            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT date, user_id, text FROM review WHERE business_id= '" + val.ToString() + "';";

                    using (var reader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            //listBox2.Items.Add(reader.GetString(0));
                            dataGridView2.Rows.Add();
                            dataGridView2.Rows[i].Cells[0].Value = reader.GetString(0);
                            dataGridView2.Rows[i].Cells[1].Value = reader.GetString(1);
                            dataGridView2.Rows[i].Cells[2].Value = reader.GetString(2);
                            i++;
                        }
                    }
                }
                conn.Close();

                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT name FROM business WHERE business_id= '" + val.ToString() + "';";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            textBox2.Text = reader.GetString(0);
                        }
                    }
                }
                

                
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct day from checkins WHERE business_id= '" + val.ToString() + "';";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            comboBox4.Items.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();

            }

        }

        int rat = 0;

        private string RanString(int size)
        {
            StringBuilder b = new StringBuilder();
            Random ran = new Random();
            char ch;

            for(int i =0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * ran.NextDouble() + 65)));
                b.Append(ch);
            }
            return b.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    string r_id = RanString(22);
                    cmd.CommandText = "INSERT INTO review (user_id, business_id, review_id, text, stars) VALUES ( '" + listBox4.SelectedItem.ToString() + "','" + bId + "','" + r_id + "','" + textBox1.Text.ToString() + "','" + rat.ToString() + "');";
                    cmd.ExecuteNonQuery();
                }

                conn.Close();

            }

            updateGrid();

        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if(radioButton1.Checked)
            {
                rat = 1;
            }
            else if(radioButton2.Checked)
            {
                rat = 2;
            }
            else if(radioButton3.Checked)
            {
                rat = 3;
            }
            else if(radioButton4.Checked)
            {
                rat = 4;
            }
            else if(radioButton5.Checked)
            {
                rat = 5;
            }

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                listBox4.Items.Clear();
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT user_id FROM acc WHERE name='" + listBox3.SelectedItem.ToString() + "' ORDER BY user_id;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listBox4.Items.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text += listBox4.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //updateGrid();
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    int c = 0;
                    foreach(var item in listBox5.Items)
                    {
                        c++;
                    }
                    cmd.Connection = conn;
                    if (c == 1)
                    {
                        cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox5.Items[0].ToString() + "'); ";
                    } else if(c == 2)
                    {
                        cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox5.Items[0].ToString() + "')" + " AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox5.Items[1].ToString() + "');";
                    } else if(c== 3)
                    {
                        cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox5.Items[0].ToString() + "')" + " AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox5.Items[1].ToString() + "')" + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox5.Items[2].ToString() + "');"; 
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();
                        int i = 0;
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = reader.GetString(0);
                            dataGridView1.Rows[i].Cells[1].Value = reader.GetString(1);
                            dataGridView1.Rows[i].Cells[2].Value = reader.GetString(2);
                            dataGridView1.Rows[i].Cells[3].Value = reader.GetString(3);
                            dataGridView1.Rows[i].Cells[4].Value = reader.GetString(4);
                            dataGridView1.Rows[i].Cells[5].Value = reader.GetInt32(5);
                            dataGridView1.Rows[i].Cells[6].Value = reader.GetInt32(6);
                            dataGridView1.Rows[i].Cells[7].Value = reader.GetInt32(7);
                            dataGridView1.Rows[i].Cells[8].Value = reader.GetBoolean(8);
                            dataGridView1.Rows[i].Cells[9].Value = reader.GetDouble(9);
                            i++;
                        }
                    }
                }
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE checkins.count SET checkins.count = checkins.count + 1 WHERE business_id= '" + bId + "' AND day= '" + comboBox4.SelectedItem.ToString() + "' AND time= '" + comboBox4.SelectedItem.ToString() + "';";
                }
                conn.Close();
            }
            updateGrid();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT time FROM checkins WHERE business_id= '" + bId + "' AND day= '" + comboBox4.SelectedItem.ToString() + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox5.Items.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT SUM(count), day from checkins WHERE business_id= '" + bId + "' GROUP BY day;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chart1.Series["Count"].Points.AddXY(reader.GetString(1), reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
        }

        
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    //string[] blist = listBox4.Items.OfType<string>().ToArray();
                    var blist = new List<string>();
                    foreach (var item in listBox4.Items)
                    {
                        blist.Add(item.ToString());
                    }
                    var sortB = comboBox6.SelectedIndex;
                    cmd.Connection = conn;
                    if (sortB == 0)
                    {
                        cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox1.SelectedItem.ToString() + "') ORDER BY name ASC;";
                    }
                    else if (sortB == 1)
                    {
                        cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox1.SelectedItem.ToString() + "') ORDER BY reviewRating DESC;";

                    }
                    else if (sortB == 2)
                    {
                        cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox1.SelectedItem.ToString() + "') ORDER BY review_count DESC;";

                    }
                    else if (sortB == 3)
                    {
                        cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, reviewRating FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' AND business_id IN (SELECT business_id FROM categories WHERE category_name = '" + listBox1.SelectedItem.ToString() + "') ORDER BY num_checkins DESC";

                    }
                    using (var reader = cmd.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();
                        int i = 0;
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = reader.GetString(0);
                            dataGridView1.Rows[i].Cells[1].Value = reader.GetString(1);
                            dataGridView1.Rows[i].Cells[2].Value = reader.GetString(2);
                            dataGridView1.Rows[i].Cells[3].Value = reader.GetString(3);
                            dataGridView1.Rows[i].Cells[4].Value = reader.GetString(4);
                            dataGridView1.Rows[i].Cells[5].Value = reader.GetInt32(5);
                            dataGridView1.Rows[i].Cells[6].Value = reader.GetInt32(6);
                            dataGridView1.Rows[i].Cells[7].Value = reader.GetInt32(7);
                            dataGridView1.Rows[i].Cells[8].Value = reader.GetBoolean(8);
                            dataGridView1.Rows[i].Cells[9].Value = reader.GetDouble(9);
                            i++;
                        }
                    }
                }
                conn.Close();
            }
        }
    }
}
