using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Proyecto1_Tel.Code
{
    public partial class User : System.Web.UI.Page
    {

        Conexion conexion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                conexion = new Conexion();

                tab_usuarios.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                                     "<h6>Usuarios</h6>" +
                                                        "  <div class=\"nav pull-right\">" +
                                                            "<a href=\"#\" class=\"dropdown-toggle just-icon\" data-toggle=\"dropdown\"><i class=\"font-cog\"></i></a>" +
                                                                "<ul class=\"dropdown-menu pull-right\">" +
                                                                    "<li><a href=\"#\"><i class=\"font-heart\"></i>Favorite it</a></li>" +
                                                                    "<li><a href=\"#\"><i class=\"font-refresh\"></i>Reload page</a></li>" +
                                                                     "<li><a href=\"#\"><i class=\"font-link\"></i>Attach something</a></li>" +
                                                                "</ul>" +
                                                          "</div>" +
                                                  "</div>" +
                                            "</div>" + Llenar_Usuarios();



                DataSet ds = conexion.Mostrar("Rol", "Rol,Nombre");

                user_rol.DataSource = ds;
                user_rol.DataTextField = "Nombre";
                user_rol.DataValueField = "Rol";
                user_rol.DataBind();
                
                
       
            } 
        }

        protected String Llenar_Usuarios()
        {
            DataSet clientes = conexion.Mostrar("USUARIO U, ROL R where U.ROL = R.ROL ", " U.USUARIO , U.NOMBRE, U.CONTRASENIA, R.NOMBRE ROL ");
            String data = "No hay Usuarios Disponibles";
            if (clientes != null)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">ID</th>" +
                               " <th  align =\"center\">Nombre</th>" +
                                "<th align =\"center\">Contraseña</th>" +
                                "<th align =\"center\">Rol</th>" +
                                "<th align =\"center\">Acciones</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    data += "<tr>"+
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["USUARIO"].ToString() +"</td>"+
                        "<td id=\"codigo\" runat=\"server\" >" + item["NOMBRE"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["CONTRASENIA"].ToString() + "</td>"+
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["ROL"].ToString() + "</td> ";
                    data += " <td>" +
                                        "<ul class=\"table-controls\">" +
                                          " <li><a href=\"javascript:Mostrar_cliente(" + item["USUARIO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>" +
                                          "<li><a  href=\"javascript:Eliminar_Usuario(" + item["USUARIO"].ToString() + ")\" class=\"tip\" CssClass=\"Elim\" title=\"Eliminar\"><i class=\"fam-cross\"></i></a> </li>" +
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

        //BUSCA Usuario
        [WebMethod]
        public static string BuscarUsuario(string id)
        {
            Conexion conn = new Conexion();

            DataSet Usuario = conn.Buscar_Mostrar("Usuario", "Usuario" + "= " + id);


            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Usuario.GetXml());

            XmlNodeList Cliente = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList lista = ((XmlElement)Cliente[0]).GetElementsByTagName("Usuario_x003D__x0020_" + id);

            XmlNodeList nNombre = ((XmlElement)lista[0]).GetElementsByTagName("Nombre");
            XmlNodeList nPass = ((XmlElement)lista[0]).GetElementsByTagName("Contrasenia");
            XmlNodeList nRol = ((XmlElement)lista[0]).GetElementsByTagName("Rol");
            string[] usuario = new string[3];
            usuario[0] = nNombre[0].InnerText;
            usuario[1] = nPass[0].InnerText;
            usuario[2] = nRol[0].InnerText;
            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(usuario);
            return json;
        }

        //Editar Usuario
        [WebMethod]
        public static bool EditUser(string id, string nombre, string rol, string pass)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(usuario) from [Usuario] where usuario=\'" + id + "\';");

            if (ds != 0)
            {
                return conn.Modificar("Usuario", "Nombre" + "=" + "\'" + nombre + "\' " + "," + " Contrasenia " + " = " + "\'" + pass + "\' , Rol = " + rol + " ", "Usuario" + "= " + id);
            }
            return false;
        }

        [WebMethod]

        public static bool DeleteUser(string id) 
        {
            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(usuario) from [Usuario] where usuario=\'" + id + "\';");

            if(ds!=0)
            {
                return conn.Eliminar("Usuario", "usuario = "+id);
            }
            return false;
        }

    }
}