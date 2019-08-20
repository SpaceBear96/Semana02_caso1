using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace Semana02_caso3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseDeDatos"].ConnectionString);

        private void cbAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("usp_encontrar_mes", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@anio", cbAnio.SelectedValue);
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "ListaMes");
                        cbMes.DataSource = df.Tables["ListaMes"];
                        cbMes.DisplayMember = "Mes";
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaAnios();
        }

        private void ListaAnios() {
            using (SqlCommand cmd = new SqlCommand("usp_encontrar_anio", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "ListaAnios");
                        cbAnio.DataSource = df.Tables["ListaAnios"];
                        cbAnio.DisplayMember = "Anios";
                        cbAnio.ValueMember = "Anios";
                    }
                }
            }
        }


    }
}
