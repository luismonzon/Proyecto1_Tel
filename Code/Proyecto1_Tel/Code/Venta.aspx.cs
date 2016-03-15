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
        Conexion conexion;
        protected void Page_Load(object sender, EventArgs e)
        {

            conexion = new Conexion();
            DataSet Productos = conexion.Mostrar("Producto", "producto, abreviatura");
            String html = "<select data-placeholder=\"Agregar producto\" class=\"select\" tabindex=\"2\">";
            html+="<option value=\"\"></option> ";

            foreach (DataRow item in Productos.Tables[0].Rows)
            {
                html += "<option value=\""+item["producto"]+"\">"+item["Abreviatura"]+"</option> ";
              
            }

            html += "</select>";
            
          
           this.productos.InnerHtml = html;

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