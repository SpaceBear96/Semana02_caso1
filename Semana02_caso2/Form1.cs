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
namespace Semana02_caso2
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
            ListaAnios();
        }

        public void ListaAnios()
        {
            using (SqlCommand cmd = new SqlCommand("usp_lista_pedidos_anios", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "Pedidos");
                        dgPedidos.DataSource = df.Tables["Pedidos"];
                    }
                }
            }
        }


        private void dgPedidos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgPedidos_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int Codigo;
            Codigo = Convert.ToInt32(dgPedidos.CurrentRow.Cells[0].Value);
            using (SqlCommand cmd = new SqlCommand("usp_detalle_pedido", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idpedido", Codigo);
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "Detalles");
                        dgDetalle.DataSource = df.Tables["Detalles"];

                    }
                }
            }
        }
    }
}
