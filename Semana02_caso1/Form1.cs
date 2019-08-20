using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace Semana02_caso1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseDeDatos"].ConnectionString);

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("usp_nombre_apellido", cn))
            {   
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "listanomape");
                        dgDatos.DataSource = df.Tables["listanomape"];
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("usp_filtro_nomape", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@nomape",lblFiltro.Text);
                    var.Text = lblFiltro.Text;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "listanomape");
                        dgDatos.DataSource = df.Tables["listanomape"];
                    }
                }
            }
        }
    }
}
