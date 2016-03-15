using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Xml;


namespace Proyecto1_Tel.Code
{

    public partial class Cliente : System.Web.UI.Page
    {

        Conexion conexion;
        protected void Page_Load(object sender, EventArgs e)
        {
            conexion = new Conexion();

            tab_clientes.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                                 "<h6>Clientes</h6>" +
                                                    "  <div class=\"nav pull-right\">" +
                                                        "<a href=\"#\" class=\"dropdown-toggle just-icon\" data-toggle=\"dropdown\"><i class=\"font-cog\"></i></a>" +
                                                            "<ul class=\"dropdown-menu pull-right\">" +
                                                                "<li><a href=\"#\"><i class=\"font-heart\"></i>Favorite it</a></li>" +
                                                                "<li><a href=\"#\"><i class=\"font-refresh\"></i>Reload page</a></li>" +
                                                                 "<li><a href=\"#\"><i class=\"font-link\"></i>Attach something</a></li>" +
                                                            "</ul>" +
                                                      "</div>" +
                                              "</div>" +
                                        "</div>" + Llenar_Roles();


        }

        protected String Llenar_Roles()
        {

            DataSet clientes = conexion.Mostrar("Cliente ", " * ");
            String data = "No hay Clientes Disponibles";
            if (clientes.Tables.Count>0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Codigo Cliente</th>" +
                               " <th  align =\"center\">Nombre</th>" +
                               " <th  align =\"center\">Apellido</th>" +
                                "<th align =\"center\">N.I.T.</th>" +
                                " <th  align =\"center\">Direccion</th>" +
                                " <th  align =\"center\">Telefono</th>" +
                                "<th align =\"center\">Acciones</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    data += "<tr><td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Cliente"].ToString() +
                        "</td><td id=\"nombre\" runat=\"server\" align =\"Center\" >" + item["Nombre"].ToString() + "</td><td id=\"apellido\" runat=\"server\" align =\"Center\">" + item["Apellido"].ToString() +
                    "</td><td id=\"nit\" runat=\"server\" align =\"Center\" >" + item["Nit"].ToString() + "</td><td id=\"direccion\" runat=\"server\" align =\"Center\">" + item["Direccion"].ToString() +
                    "<td id=\"telefono\" runat=\"server\" align =\"Center\">" + item["Telefono"].ToString() + "</td>";
                        data += " <td>" +
                                        "<ul class=\"table-controls\">" +
                                          " <li><a href=\"javascript:Mostrar_cliente(" + item["Cliente"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>" +
                                          "<li><a  href=\"javascript:Eliminar_Cliente(" + item["Cliente"].ToString() + ")\" class=\"tip\" CssClass=\"Elim\" title=\"Eliminar\"><i class=\"fam-cross\"></i></a> </li>" +
                                    "</td>";
                    data += "</tr>";
                }


                data += "</tbody>" +
                        "</table>" +
                    "</div>" +
                "</div>";

            }
            return data;
        }


        //BUSCA ID DEL CLIENTE
        [WebMethod]
        public static string BuscarCliente(string id)
        {
            Conexion conn = new Conexion();

            DataSet roles = conn.Buscar_Mostrar("Cliente", "Cliente" + "= " + id);
            string[] cliente = new string[5];
            

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(roles.GetXml());

            XmlNodeList Cliente = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList lista = ((XmlElement)Cliente[0]).GetElementsByTagName("Cliente_x003D__x0020_" + id);

            XmlNodeList nNombre = ((XmlElement)lista[0]).GetElementsByTagName("Nombre");
            XmlNodeList nNit = ((XmlElement)lista[0]).GetElementsByTagName("Nit");
            XmlNodeList nApellido = ((XmlElement)lista[0]).GetElementsByTagName("Apellido");
            XmlNodeList nDireccion = ((XmlElement)lista[0]).GetElementsByTagName("Direccion");
            XmlNodeList nTelefono = ((XmlElement)lista[0]).GetElementsByTagName("Telefono");

            cliente[0] = nNombre[0].InnerText;            

           if(nNit.Count == 0){ //si no tiene nit
               cliente[1] = "";
           }
           else
           {
               cliente[1] = nNit[0].InnerText;
           }

           if (nApellido.Count == 0) //si no tiene apellido
           {
               cliente[2] = "";
           }
           else
           {
               cliente[2] = nApellido[0].InnerText;
           }

           if (nDireccion.Count == 0) //si no tiene direccion
           {
               cliente[3] = "";
           }
           else
           {
               cliente[3] = nDireccion[0].InnerText;
            
           }

           if (nTelefono.Count == 0) //si no tiene telefono
           {
               cliente[4] = "";
           }
           else
           {
               cliente[4] = nTelefono[0].InnerText;
           }
            
            

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cliente);
            return  json;
        }

        [WebMethod]
        public static bool EliminarCliente(string id)
        {
            Conexion conn = new Conexion();
            bool respuesta;

            respuesta = conn.Eliminar("Cliente", "Cliente" + "= " + id);
            

            return respuesta;
        }

        [WebMethod]
        public static bool EditCliente(string id, string nombre, string nit , string apellido, string direccion, string telefono)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(id) from [Cliente] where id=\'" + id + "\';");
            


            if (ds != 0)
            {
                int cantnit = conn.Count("Select count(nit) from [Cliente] where nit=\'" + nit + "\'  And Cliente!= " + id + ";");
                if (cantnit == 0)
                {
                    conn.Modificar("Cliente", "Nombre" + "=" + "\'" + nombre + "\' " + "," + " Nit " + " = " + "\'" + nit + "\' " + "," + " Apellido " + " = " + "\'" + apellido + "\' " + "," + " Direccion " + " = " + "\'" + direccion + "\' " + "," + " Telefono " + " = " + "\'" + telefono + "\' ", "Cliente" + "= " + id);
                    return true;
                }
                
            }

            return false;
        }

        //AGREGAR CLIENTE

        [WebMethod]
        public static bool Add(string nombre, string nit, string apellido, string direccion, string telefono)
        {



            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(nit) from [cliente] where nit=\'" + nit + "\';");

            if (ds == 0)
            {
                conn.Crear("Cliente", "Nombre , Nit, Apellido, Direccion, Telefono ", "\'" + nombre + "\','" + nit + "\','" + apellido + "\','" + direccion + "\','" + telefono + "\'");
                return true;
            }

            return false;
        }

    }
}