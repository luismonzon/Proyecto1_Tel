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
    public partial class Rol : System.Web.UI.Page
    {
        Conexion conexion;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                    if (Session["Usuario"] != null)
                    {
                        if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Roles"))
                        {
                            Response.Redirect("~/Index.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Index.aspx");
                    }

                    conexion = new Conexion();
                    Load();

            }

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();

        }

        public void Load(){
            tab_roles.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                                 "<h6>Roles de Usuarios</h6>" +
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

        [WebMethod]
        protected String Llenar_Roles()
        {

            DataSet roles = conexion.Mostrar(" Rol ", " * ");
            String data = "No hay Roles Disponibles";
            if (roles.Tables.Count>0)
            {
               

                    data = "<div class=\"table-overflow\"> " +
                                        "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                                            "<thead>" +
                                                "<tr>" +
                                                   " <th  align =\"center\">Codigo</th>" +
                                                    "<th align =\"center\">Nombre</th>" +
                                                   /* "<th align =\"center\">Acciones</th>" +*/
                                                "</tr>" +
                                            "</thead>" + "<tbody>";

                    
                
                foreach (DataRow item in roles.Tables[0].Rows)
                {
                    data += "<tr><td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Rol"].ToString() +
                        "</td><td>" + item["Nombre"].ToString() + "</td>";
                    
                       
                    data += "</tr>";
                    
                }


                data += "</tbody>" +
                        "</table>" +
                    "</div>" +
                "</div>";

            }
            return data;
        }



        [WebMethod]
        public static bool InsertRol(string name)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(nombre) from [Rol] where nombre=\'" + name + "\';");

            if (ds == 0)
            {
                conn.Crear("Rol", "Nombre", "\'" + name + "\'");
                return true;
            }

            return false;

        }

        [WebMethod]
        public static bool EditRol(string name, string id)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(nombre) from [Rol] where nombre=\'" + name + "\';");

            if (ds == 0)
            {
                conn.Modificar("Rol", "Nombre" + "=" + "\'" + name + "\' ", "Rol" + "= " + id);
                return true;
            }

            return false;
        }



        [WebMethod]
        public static string BuscarRol(string id)
        {
            Conexion conn = new Conexion();

            DataSet roles = conn.Buscar_Mostrar("Rol" , "Rol" + "= " + id);

            
            XmlDocument xDoc =  new XmlDocument();
            xDoc.LoadXml(roles.GetXml());

            XmlNodeList Roles = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList lista = ((XmlElement)Roles[0]).GetElementsByTagName("Rol_x003D__x0020_"+id); 

            XmlNodeList nNombre = ((XmlElement)lista[0]).GetElementsByTagName("Nombre");    


            return nNombre[0].InnerText;
        }

        [WebMethod]
        public static bool EliminarRol(string id)
        {
            Conexion conn = new Conexion();

            bool respuesta;

            respuesta = conn.Eliminar("Rol" , "Rol" + "= " + id);
            

            return respuesta;
        }
    }
}