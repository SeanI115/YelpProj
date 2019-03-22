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

        string connstring = "Server=localhost;Port=5432;User Id = postgres; Password=eraser2;Database=postgres";

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
                    cmd.CommandText = "SELECT distinct companystate FROM companies ORDER BY companystate;";
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
                    cmd.CommandText = "SELECT distinct companycity FROM companies ORDER BY companycity;";
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
                    cmd.CommandText = "SELECT distinct companycity FROM companies WHERE companystate = '" + comboBox1.SelectedItem.ToString() + "' ORDER by companycity;";
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connstring))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT companyname, companystate, companycity FROM companies WHERE companycity = '"+ comboBox2.SelectedItem.ToString() + "' AND companystate = '" + comboBox1.SelectedItem.ToString() + "'ORDER BY companyname;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                        }

                    }
                }
            }
            comboBox2.Items.Clear();
        }
    }
}
