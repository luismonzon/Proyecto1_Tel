using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Xml;
namespace Proyecto1_Tel.Code
{
    public partial class Venta : System.Web.UI.Page
    {
        Conexion conexion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Venta"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }




                conexion = new Conexion();
                DataSet Productos = conexion.Mostrar("Producto", "producto, abreviatura");
                String html = "<select data-placeholder=\"Agregar producto\" class=\"select\" tabindex=\"2\">";
                html += "<option value=\"\"></option> ";

                foreach (DataRow item in Productos.Tables[0].Rows)
                {
                    html += "<option value=\"" + item["producto"] + "\">" + item["Abreviatura"] + "</option> ";

                }

                html += "</select>";


                this.productos.InnerHtml = html;
            }

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
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

        [WebMethod]

        public static string MostrarModal(string id)
        {
            string innerhtml =
                "<div class=\"modal fade\" id=\"Modal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" onclick=\"reloadTable();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>R</i> \n" +
                "<h5>Agregar Cliente</h5> \n" +
                "<span>Agregar un Cliente </span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal

            innerhtml +=
                "<form id=\"formulario\" class=\"form-horizontal row-fluid well\"> \n" +
                "<div class=\"modal-body\"> \n" +
                "<table border=\"0\" width=\"100%\" > \n" +
                "<div> \n" +
                //ID DEL CLIENTE
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Codigo:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Codigo\" readonly=\"readonly\" style=\"font-size: 15px;\" type=\"text\" name=\"codigo\" id=\"codigo\" runat=\"server\" class=\"span8\" /></div> \n" +
                "</div> \n" +
                //
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Nombres:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Nombre\" required=\"required\" style=\"font-size: 15px;\" type=\"text\" name=\"nombre\" id=\"nombre\" runat=\"server\" class=\"span12\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Apellidos:</b></label> \n" +
                "<div class=\"controls\"><input  style=\"font-size: 15px;\" type=\"text\" placeholder=\"Apellidos\" id=\"apellido\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Nit:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Nit\"  style=\"font-size: 15px;\" type=\"text\" name=\"nit\" id=\"nit\" runat=\"server\" class=\"span12\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Direccion:</b></label> \n" +
                "<div class=\"controls\"><input  style=\"font-size: 15px;\" type=\"text\" placeholder=\"Direccion\" id=\"direccion\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Telefono:</b></label> \n" +
                "<div class=\"controls\"><input style=\"font-size: 15px;\" type=\"text\" placeholder=\"Telefono\" id=\"telefono\" runat=\"server\" data-mask=\"9999-9999\" /></div> \n" +
                "</div> \n" +
                "<tr> \n" +
                "<td colspan=\"2\"> \n" +
                "<div id=\"mensaje\"></div> \n" +
                "<div class=\"alert margin\"> \n" +
                "<button type=\"button\"  class=\"close\" data-dismiss=\"alert\">×</button> \n" +
                "Campos Obligatorios (*) \n" +
                "</div> \n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"reloadTable();\" id=\"cerrar\">Cerrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-success\" onclick=\"AddClient();\" name=\"reg\" id=\"reg\">Registrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-warning\" onclick=\"Edit();\" name=\"edi\" id=\"edi\">Editar</button>\n" +
                "</td> \n" +
                "</tr> \n" +
                "</div> \n" +
                "</table> \n" +
                "</div> \n"
                ;


            //footer del modal
            innerhtml += "</div>\n" +
                "</div>\n" +
                "</div>\n"
            ;

            return innerhtml;
        }

        //AGREGAR CLIENTE

        [WebMethod]
        public static bool Add(string nombre, string nit, string apellido, string direccion, string telefono)
        {



            Conexion conn = new Conexion();

            if (nit != "")
            {
                int ds = conn.Count("Select count(nit) from [cliente] where nit=\'" + nit + "\';");

                if (ds == 0)
                {
                    return conn.Crear("Cliente", "Nombre , Nit, Apellido, Direccion, Telefono ", "\'" + nombre + "\','" + nit + "\','" + apellido + "\','" + direccion + "\','" + telefono + "\'");

                }


            }
            else
            {
                return conn.Crear("Cliente", "Nombre , Nit, Apellido, Direccion, Telefono ", "\'" + nombre + "\','" + nit + "\','" + apellido + "\','" + direccion + "\','" + telefono + "\'");
            }

            return false;
        }


        //BUSCA ID DEL CLIENTE
        [WebMethod]
        public static string BuscarCliente(string nombre, string nit, string apellido, string direccion, string telefono)
        {
            Conexion conn = new Conexion();

            DataSet roles = conn.Buscar_Mostrar("Cliente", "Nombre" + "= '" + nombre + "' AND Nit= '" + nit + "' AND Apellido='" + apellido + "' AND Direccion='" + direccion + "' AND Telefono= '" + telefono + "'");
            string[] cliente = new string[1];


            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(roles.GetXml());

            XmlNodeList Cliente = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList nCliente = ((XmlElement)Cliente[0]).GetElementsByTagName("Cliente");
            XmlNodeList nNit = ((XmlElement)Cliente[0]).GetElementsByTagName("Nit");
            XmlNodeList nApellido = ((XmlElement)Cliente[0]).GetElementsByTagName("Apellido");
            XmlNodeList nDireccion = ((XmlElement)Cliente[0]).GetElementsByTagName("Direccion");
            XmlNodeList nTelefono = ((XmlElement)Cliente[0]).GetElementsByTagName("Telefono");

            cliente[0] = nCliente[0].InnerText;

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cliente);
            return json;
        }




    }
}