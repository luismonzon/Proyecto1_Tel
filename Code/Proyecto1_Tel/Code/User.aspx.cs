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
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Usuarios"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

               

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


/*
                DataSet ds = conexion.Mostrar("Rol", "Rol,Nombre");

                Rol.DataSource = ds;
                Rol.DataTextField = "Nombre";
                Rol.DataValueField = "Rol";
                Rol.DataBind();
                
 */               
       
            }

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        
        }

        protected String Llenar_Usuarios()
        {
            DataSet clientes = conexion.Mostrar("USUARIO U, ROL R where U.ROL = R.ROL ", " U.USUARIO , U.NOMBRE, U.NICKNAME, U.APELLIDO, U.DPI, R.NOMBRE ROL ");
            String data = "No hay Usuarios Disponibles";
            if (clientes.Tables.Count>0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Nick Name</th>" +
                               " <th  align =\"center\">Nombre</th>" +
                               " <th  align =\"center\">Apellido</th>" +
                               " <th  align =\"center\">Dpi</th>"+
                                "<th align =\"center\">Rol</th>" +
                                "<th align =\"center\">Acciones</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    data += "<tr>"+
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["NICKNAME"].ToString() +"</td>"+
                        "<td id=\"codigo\" runat=\"server\" >" + item["NOMBRE"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["APELLIDO"].ToString() + "</td>"+
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["DPI"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["ROL"].ToString() + "</td> ";
                    data += " <td>" +
                                        "<ul class=\"table-controls\">" +
                                          " <li><a href=\"javascript:Mostrar_usuario(" + item["USUARIO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>" +
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

            XmlNodeList nNick = ((XmlElement)lista[0]).GetElementsByTagName("NickName");
            XmlNodeList nNombre = ((XmlElement)lista[0]).GetElementsByTagName("Nombre");    
            XmlNodeList nApellido = ((XmlElement)lista[0]).GetElementsByTagName("Apellido");
            XmlNodeList nDpi = ((XmlElement)lista[0]).GetElementsByTagName("Dpi");
            XmlNodeList nPass = ((XmlElement)lista[0]).GetElementsByTagName("Contrasenia");
            XmlNodeList nRol = ((XmlElement)lista[0]).GetElementsByTagName("Rol");



            string[] usuario = new string[6];
            usuario[0] = nNick[0].InnerText;
            usuario[1] = nNombre[0].InnerText;
            usuario[2] = nRol[0].InnerText;
            usuario[3] = nApellido[0].InnerText;
            if (nDpi.Count == 0)
            { //si no tiene nit
                usuario[4] = "";
            }
            else
            {
                usuario[4] = nDpi[0].InnerText;
            }

            usuario[5] = nPass[0].InnerText;

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(usuario);
            return json;
        }

        //Editar Usuario
        [WebMethod]
        public static bool EditUser(string id, string nickname, string nombre, string apellido, string rol, string pass, string dpi)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(usuario) from [Usuario] where usuario=\'" + id + "\';");

            if (ds != 0)
            {

                int cantnick = conn.Count("Select count(nickname) from [Usuario] where NickName=\'" + nickname + "\'  And Usuario!= " + id + ";");
                 if (cantnick == 0)
                 {
                     return conn.Modificar("Usuario", "Nombre" + "=" + "\'" + nombre + "\' " + "," + " Contrasenia " + " = " + "\'" + pass + "\' , Rol = " + rol + "," + " NickName " + " = " + "\'" + nickname + "\' " + "," + " Apellido " + " = " + "\'" + apellido + "\', " + " Dpi " + " = " + "\'" + dpi + "\' ", "Usuario" + "= " + id);
                 }

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


        [WebMethod]
        public static bool Add(string nickname, string nombre, string apellido, string dpi, string password, string rol)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(NickName) from [usuario] where NickName=\'" + nickname + "\';");

            if (ds == 0)
            {
                conn.Crear("Usuario", "NickName,Nombre, Apellido,Dpi,Rol,Contrasenia", "\'" + nickname + "',\'" + nombre + "',\'" + apellido + "',\'" + dpi + "\'," + rol + ",\'" + password + "\'");
                return true;
            }

            return false;
        }

        [WebMethod]

        public static string MostrarModal(string id) 
        {
            string innerhtml =
                "<div class=\"modal fade\" id=\"editar-usuario\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" onclick=\"reloadTable();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>R</i> \n" +
                "<h5>Administrar Usuario</h5> \n" +
                "<span>Agregar o Editar un Usuario </span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal

            innerhtml += 
                "<form id=\"formulario-usuario\" class=\"form-horizontal row-fluid well\"> \n" +
                "<div class=\"modal-body\"> \n" +
                "<table border=\"0\" width=\"100%\" > \n" +
                "<div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Usuario:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"NickName\" required=\"required\" style=\"font-size: 15px;\" type=\"text\" name=\"nickname\" id=\"nickname\" runat=\"server\" class=\"span12\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Nombre:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" type=\"text\" placeholder=\"Nombre\" id=\"nombre\" runat=\"server\" /></div> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Apellido:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" type=\"text\" placeholder=\"Apellido\" id=\"apellido\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*DPI:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"DPI\" required=\"required\" style=\"font-size: 15px;\" type=\"text\" name=\"dpi\" id=\"dpi\" runat=\"server\" class=\"span12\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Contraseña:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" type=\"password\" placeholder=\"Contraseña\" id=\"password\" runat=\"server\" /></div> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Repetir Contraseña:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" type=\"password\" placeholder=\"Repetir Contraseña\" id=\"password1\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Rol:</b></label> \n" +
                "<div class=\"controls\"><select name=\"Rol\" class=\"select\" id=\"Rol\"></select></div> \n" +
                "</div> \n" +
                "<tr> \n" +
                "<td colspan=\"2\"> \n" +
                "<div id=\"mensaje\"></div> \n" +
                "<div class=\"alert margin\"> \n" +
                "<button type=\"button\"  class=\"close\" data-dismiss=\"alert\">×</button> \n" +
                "Campos Obligatorios (*) \n" +
                "</div> \n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"reloadTable();\" id=\"cerrar\">Cerrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-success\" onclick=\"AddUser();\" name=\"reg\" id=\"reg\">Registrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-warning\" onclick=\"Edit();\" name=\"edi\" id=\"edi\">Editar</button>\n" +
                "</td> \n" +
                "</tr> \n" +
                "</div> \n" +
                "</table> \n" +
                "</div> \n"
                ;


            //footer del modal
            innerhtml += "</div>\n" +
                "<div class=\"modal-footer\">\n" +
                "</div>\n" +
                "</div>\n" +
                "</div>\n"
            ;

            return innerhtml;
        }

        [WebMethod]

        public static string Fill() 
        {
            Conexion con = new Conexion();

            DataSet data = con.Mostrar("Rol", "Rol,Nombre");

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(data.GetXml());

            XmlNodeList _Deudas = xDoc.GetElementsByTagName("NewDataSet");


            string deuda = "";
            XmlNodeList lista = ((XmlElement)_Deudas[0]).GetElementsByTagName("Rol_x002C_Nombre");
            int cant = lista.Count;
            for (int i = 0; i < cant; i++)
            {

                XmlNodeList key = ((XmlElement)lista[i]).GetElementsByTagName("Rol");
                XmlNodeList value = ((XmlElement)lista[i]).GetElementsByTagName("Nombre");

                deuda += "{ \"key\":\"" + key[0].InnerText + "\",\"value\":\"" + value[0].InnerText + "\"}";
                if (i != cant - 1)
                {
                    deuda += ",";
                }

            }

            deuda = "[" + deuda + "]";
            //string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(deuda);

            return deuda;
        }

    }


}