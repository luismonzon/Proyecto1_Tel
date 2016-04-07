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
    public class Product{
        public String cantidad;
        public String codigo;
        public String nombre;
        public string precio;
        public Product(String cant, String cod, String nombre,string precio){
            this.precio = precio;
        cantidad=cant;
        codigo=cod;
        this.nombre = nombre;
        }


    }

    public partial class Venta : System.Web.UI.Page
    {
        public static List<Product> carrito; 
        Conexion conexion;
        static String usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

                usuario = Session["Usuario"].ToString();
            {   carrito= new List<Product>();   
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
                DataSet Productos = conexion.Consulta("select * from Producto where Producto in(select Producto from Inventario)");
                String html = "<select style=\"font-size: 15px;\" data-placeholder=\"Agregar producto\" class=\"select\"  onChange=\"cambio()\"  id=\"cmbproductos\" tabindex=\"2\">";
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
        public static string Agrega(string idcliente)
        {

            return "";
        }
        [WebMethod]
        public static string Busca(string idcliente)
        {

            Conexion conexion = new Conexion();
            DataSet cliente;
            string respuesta = "";
            cliente = conexion.Buscar_Mostrar("cliente", "cliente=" + idcliente);
            if (cliente.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow item in cliente.Tables[0].Rows)
                {
                    respuesta += item["nombre"].ToString() + "," + item["nit"].ToString() + "," + item["apellido"].ToString() + "," + item["cliente"].ToString() + "," + item["direccion"].ToString() + "," + item["telefono"].ToString();
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
                "<button type=\"button\" onclick=\"closeModal();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>C</i> \n" +
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
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"closeModal();\" id=\"cerrar\">Cerrar</button>\n" +
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

        [WebMethod]
        public static string MostrarModalPago(string cliente)
        {
            Conexion nueva = new Conexion();
            int total = 0;
            foreach (var item in carrito)
            {
                total+=Int32.Parse((Double.Parse(item.cantidad)*Double.Parse(item.precio))+"");
            }

            string innerhtml =
                "<div class=\"modal fade\" id=\"ModalPago\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" onclick=\"closeModalPago();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>C</i> \n" +
                "<h5>Area Pago</h5> \n" +
                "<span>realizar compra</span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal

            innerhtml +=
                "<form id=\"formulario_modal\" class=\"form-horizontal row-fluid well\"> \n" +
                "<div class=\"modal-body\"> \n" +
                "<table border=\"0\" width=\"100%\" > \n" +
                "<div> \n" +
                //ID DEL CLIENTE
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Total:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Total\" disabled =\"disabled\" readonly=\"readonly\" style=\"font-size: 15px;\" value=\""+total+"\"type=\"text\" name=\"total\" id=\"totalpago\" runat=\"server\" class=\"span8\" /></div> \n" +
                "</div> \n" +
                //
              
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Codigo Cliente</b></label> \n" +
                "<div class=\"controls\"><input  style=\"font-size: 15px disabled =\"disabled\" readonly=\"readonly\"  type=\"text\" value=\"" + cliente + "\" id=\"codclientepago\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Tipo Pago</b></label> \n" +
                "<div class=\"controls\"><select style=\"font-size: 15px;\" data-placeholder=\"Agregar producto\" class=\"select\"  onChange=\"cambio()\"  id=\"cmbpago\" tabindex=\"2\"><option value=\"\">Elegir Tipo</option><option value=\"1\">Efectivo</option><option value=\"2\">Deuda</option> </select></div> \n" +
                "</div> \n" +

                "<tr> \n" +
                "<td colspan=\"2\"> \n" +
                "<div id=\"mensaje_modal\"></div> \n" +
                "<div class=\"alert margin\"> \n" +
                "<button type=\"button\"  class=\"close\" data-dismiss=\"alert\">×</button> \n" +
                "Campos Obligatorios (*) \n" +
                "</div> \n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"closeModalPago();\" id=\"cerrar_modal\">Cerrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-success\" onclick=\"AddPago();\" name=\"reg\" id=\"reg_modal\">Registrar</button>\n" +
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
        public static bool AddPago(string total,string cliente, string tipopago)
        {
            Conexion nueva = new Conexion();
            
            nueva.Crear("Venta", "Cliente, Usuario, Fecha, Total",cliente + "," + usuario+",GETDATE(),"+total);
              foreach (var item in carrito)
	            {
		             nueva.Crear("DetalleVenta", "Venta, producto, cantidad","(select max(Venta) from venta),"+item.codigo+","+item.cantidad);
                    
                }

            if(tipopago.Equals("2"){
                
                 nueva.Crear("Deuda", "Cliente, cantidad, venta",cliente+","+total+",(select max(Venta) from venta)");
            }
            
            return true;
        }


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


        [WebMethod]
        public static string CleanCarrito()
        {
            carrito.Clear();

            return Graficar();

        }
        [WebMethod]
        public static string removecarrito(String codigo)
        {
            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].codigo.Equals(codigo)) {
                    carrito.RemoveAt(i);
                    break;
                }

            }
            return Graficar();
        }
        [WebMethod]
        public static string AddProducto(String producto,String cantidad, String codigo)
        {
            Boolean band = true;
            
            foreach (var item in carrito)
            {
                if(item.codigo==codigo){

                    band = false;
                }
            }

            if (band)
            {
                Conexion nueva = new Conexion();
                DataSet datos = nueva.Consulta("select precio from inventario where producto=" + codigo);
                carrito.Add(new Product( cantidad,codigo,producto,datos.Tables[0].Rows[0][0]+""));
            }
            return Graficar();
        }        



        public  static string Graficar(){

            string str = "<div class=\"widget\">" +
                    "<div class=\"navbar\">" +
                    "    <div class=\"navbar-inner\">" +
                    "       <h6>Detalle</h6>" +
                    "        <div class=\"nav pull-right\">" +
                    "        </div>" +
                    "    </div>" +
                    "</div>" +
                    "<div class=\"table-overflow\">" +
                    "    <table class=\"table table-striped table-bordered align-center\">" +
                    "        <thead>" +
                    "           <tr>" +
                    "                <th>Nombre</th>" +
                    "                <th>Cantidad</th>" +
                    "                <th>Precio Unitario</th>" +
                    "                <th>Precio Total</th>" + 
                    "                <th>Acciones</th>" +
                    "            </tr>" +
                    "        </thead>" +
                    "        <tbody>";

            for (int i = 0; i < carrito.Count; i++)
            {


                str += "            <tr>" +
                "                <td>" + carrito[i].codigo + "</td>" +
                "                <td>" +
                                    carrito[i].cantidad +
                "                </td>" +
                "               <td>"+carrito[i].precio+"</td>"  + 
                "               <td>"+Int32.Parse((Double.Parse(carrito[i].precio)*Double.Parse(carrito[i].cantidad))+"")+"</td>"   +

                                "<td>" +
                                    "   <ul class=\"table-controls\">" +
                              "          <li><a href=\"javascript:removecarrito('" + carrito[i].codigo + "')\"class=\"tip\" title=\"Remover\"><i class=\"fam-cross\"></i></a> </li>" +
                             "       </ul>" +
                            "    </td>" +
                            " </tr>";

            }

            str += "</tbody>" +
        " </table>" +
    " </div>" +
 "</div>";
            return str;
        
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