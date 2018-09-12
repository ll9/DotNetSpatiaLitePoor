using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpatialiteDataTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = GetConnection())
            {
                using (var builder = new SQLiteCommand("create table test (id, geometry);", connection))
                {
                    builder.ExecuteNonQuery();
                }
                using (var builder = new SQLiteCommand("SELECT InitSpatialMetaData();", connection))
                {
                    builder.ExecuteNonQuery();
                }
                using (var builder = new SQLiteCommand("select RecoverGeometryColumn('test','geometry',4326,'POINT');", connection))
                {
                    builder.ExecuteNonQuery();
                }
                using (var builder = new SQLiteCommand("insert into test values (1,ST_GeomFromText('POINT (0 0)',4326));", connection))
                {
                    builder.ExecuteNonQuery();
                }
                using (var builder = new SQLiteCommand("insert into test values (2,ST_GeomFromText('POINT (0 0)',4326));", connection))
                {
                    builder.ExecuteNonQuery();
                }
                using (var builder = new SQLiteCommand("insert into test values (3,ST_GeomFromText('POINT (0 1)',4326));", connection))
                {
                    builder.ExecuteNonQuery();
                }
                using (var builder = new SQLiteCommand("update test set geometry=ST_GeomFromText('POINT (2 4)',4326);", connection))
                {
                    builder.ExecuteNonQuery();
                }
            }
        }

        private static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection("Data Source=dummy3.db");
            connection.Open();
            connection.EnableExtensions(true);
            connection.LoadExtension("mod_spatialite");
            return connection;
        }
    }
}
