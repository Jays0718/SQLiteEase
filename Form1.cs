using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DemoApp
{
    public partial class Form1 : Form
    {
        SQLiteConnection conn = new SQLiteConnection("Data Source=DemoApp.db;Version=3;");
        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn.Open();
            var cmd = new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS Users(Id INTEGER PRIMARY KEY, Name TEXT, Age INTEGER);", conn);
            cmd.ExecuteNonQuery();
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Users(Name, Age) VALUES('{txtName.Text}', {txtAge.Text})", conn);
            cmd.ExecuteNonQuery();
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var cmd = new SQLiteCommand($"DELETE FROM Users WHERE Id = {txtId.Text}", conn);
            cmd.ExecuteNonQuery();
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var cmd = new SQLiteCommand($"UPDATE Users SET Name = '{txtName.Text}', Age = {txtAge.Text} WHERE Id = {txtId.Text}", conn);
            cmd.ExecuteNonQuery();
            LoadData();
        }

        private void LoadData()
        {
            var adapter = new SQLiteDataAdapter("SELECT * FROM Users", conn);
            var dt = new DataTable();
            adapter.Fill(dt);
            dgvUsers.DataSource = dt;
        }
    }
}
