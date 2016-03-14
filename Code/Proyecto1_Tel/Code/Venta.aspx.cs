using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
namespace Proyecto1_Tel.Code
{
    public partial class Venta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        
        }


        [WebMethod]
        public static string Busca(string idcliente)
        {

            Conexion conexion = new Conexion();
            DataSet cliente;
            string respuesta = "";
            cliente =conexion.Buscar_Mostrar("cliente", "cliente=" + idcliente);
            if (cliente.Tables[0].Rows.Count > 0) {

                foreach (DataRow item in cliente.Tables[0].Rows)
                {
                    respuesta += item["nombre"].ToString() + "," + item["nit"].ToString() + "," + item["apellido"].ToString() + "," + item["cliente"].ToString();
                }
                return respuesta;
            }
            

            return "0";
        }
    }
}