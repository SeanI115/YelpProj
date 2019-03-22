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
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT business_id, name, state, city, address, zipcode, review_count, num_checkins, is_open, stars FROM business WHERE zipcode = '" + comboBox3.SelectedItem.ToString() + "' AND city= '" + comboBox2.SelectedItem.ToString() + "' ORDER BY zipcode; ";

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
            }
        }
    }
}
