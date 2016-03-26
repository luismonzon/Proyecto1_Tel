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
    public partial class Producto : System.Web.UI.Page
    {
         Conexion conexion;

         protected void Page_Load(object sender, EventArgs e)
         {
             if (!IsPostBack)
             {
                 if (Session["Usuario"] != null)
                 {
                     if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Producto"))
                     {
                         Response.Redirect("~/Index.aspx");
                     }
                 }
                 else
                 {
                     Response.Redirect("~/Index.aspx");
                 }

               

                 conexion = new Conexion();

                 tab_producto.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                                      "<h6>Productos</h6>" +
                                                         "  <div class=\"nav pull-right\">" +
                                                             "<a href=\"#\" class=\"dropdown-toggle just-icon\" data-toggle=\"dropdown\"><i class=\"font-cog\"></i></a>" +
                                                                 "<ul class=\"dropdown-menu pull-right\">" +
                                                                     "<li><a href=\"#\"><i class=\"font-heart\"></i>Favorite it</a></li>" +
                                                                     "<li><a href=\"#\"><i class=\"font-refresh\"></i>Reload page</a></li>" +
                                                                      "<li><a href=\"#\"><i class=\"font-link\"></i>Attach something</a></li>" +
                                                                 "</ul>" +
                                                           "</div>" +
                                                   "</div>" +
                                             "</div>" + Llenar_Productos();

                 //DataSet ds = conexion.Mostrar("Tipo", "Tipo,Descripcion");

                 /*tipopro.DataSource = ds;
                 tipopro.DataTextField = "Descripcion";
                 tipopro.DataValueField = "Tipo";
                 tipopro.DataBind();*/
                
             }

             Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
             Response.Cache.SetAllowResponseInBrowserHistory(false);
             Response.Cache.SetNoStore();
         }

        protected String Llenar_Productos()
        {
            DataSet productos = conexion.Mostrar("Producto P, Tipo T Where P.Tipo = T.tipo ", " P.PRODUCTO, P.ABREVIATURA, P.DESCRIPCION, P.PORCENTAJE,"+ 
             "P.ANCHO, P.MARCA, T.DESCRIPCION NOMBRETIPO ");
            String data = "No hay Productos Disponibles";
            if (productos != null)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Abreviatura</th>" +
                               " <th  align =\"center\">Descripcion</th>" +
                               " <th  align =\"center\">Tipo</th>" +
                                "<th align =\"center\">Porcentaje</th>" +
                                " <th  align =\"center\">Ancho</th>" +
                                " <th  align =\"center\">Marca</th>" +
                                "<th align =\"center\">Acciones</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in productos.Tables[0].Rows)
                 {
                    data += "<tr>"+
                        "<td id=\"abreviatura\" runat=\"server\" align =\"Center\">" + item["ABREVIATURA"].ToString() +"</td>"+
                        "<td id=\"descripcion\" runat=\"server\" >" + item["DESCRIPCION"].ToString() + "</td>" +
                        "<td id=\"nombretipo\" runat=\"server\" align =\"Center\">" + item["NOMBRETIPO"].ToString() + "</td>"+
                        "<td id=\"porcentaje\" runat=\"server\" align =\"Center\">" + item["PORCENTAJE"].ToString() + "</td>" +
                        "<td id=\"ancho\" runat=\"server\" align =\"Center\">" + item["ANCHO"].ToString() + "</td>" +
                        "<td id=\"marca\" runat=\"server\" align =\"Center\">" + item["MARCA"].ToString() + "</td> ";
                    data += " <td>" +
                                        "<ul class=\"table-controls\">" +
                                          " <li><a href=\"javascript:Mostrar_Producto(" + item["PRODUCTO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>" +
                                          "<li><a  href=\"javascript:Eliminar_Producto(" + item["PRODUCTO"].ToString() + ")\" class=\"tip\" CssClass=\"Elim\" title=\"Eliminar\"><i class=\"fam-cross\"></i></a> </li>" +
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
        public static string BuscarProducto(string id)
        {
            Conexion conn = new Conexion();

            DataSet Producto_ = conn.Buscar_Mostrar("Producto P", "Producto" + "= " + id);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Producto_.GetXml());

            XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList lista_producto = ((XmlElement)_Producto[0]).GetElementsByTagName("Producto_x003D__x0020_" + id);

            XmlNodeList nAbreviatura = ((XmlElement)lista_producto[0]).GetElementsByTagName("Abreviatura");
            XmlNodeList nDescripcion = ((XmlElement)lista_producto[0]).GetElementsByTagName("Descripcion");
            XmlNodeList nTipo = ((XmlElement)lista_producto[0]).GetElementsByTagName("Tipo");

            DataSet Tipo_Pro = conn.Buscar_Mostrar("Tipo", "Tipo" + "= " + nTipo[0].InnerText);
            XmlDocument xDocu = new XmlDocument();
            xDocu.LoadXml(Tipo_Pro.GetXml());
            XmlNodeList Tipopro = xDocu.GetElementsByTagName("NewDataSet");
            XmlNodeList lista_tipo = ((XmlElement)Tipopro[0]).GetElementsByTagName("Tipo_x003D__x0020_" + nTipo[0].InnerText);
            XmlNodeList nDescripcionTipo = ((XmlElement)lista_tipo[0]).GetElementsByTagName("Descripcion");
            

            XmlNodeList nPorcentaje = ((XmlElement)lista_producto[0]).GetElementsByTagName("Porcentaje");
            XmlNodeList nAncho = ((XmlElement)lista_producto[0]).GetElementsByTagName("Ancho");
            XmlNodeList nMarca = ((XmlElement)lista_producto[0]).GetElementsByTagName("Marca");

            string[] producto = new string[7];
            producto[0] = nAbreviatura[0].InnerText;
            producto[1] = nDescripcion[0].InnerText;
            producto[2] = nTipo[0].InnerText;
           
            if (nPorcentaje.Count == 0)
            { 
                producto[3] = "";
            }
            else
            {
                producto[3] = nPorcentaje[0].InnerText;
            }


            if (nAncho.Count == 0)
            { 
                producto[4] = "";
            }
            else
            {
                producto[4] = nAncho[0].InnerText;
            }

            producto[5] = nMarca[0].InnerText;
            producto[6] = nDescripcionTipo[0].InnerText;


            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(producto);
            return json;
        }

        [WebMethod]
        public static bool Add(string abrevia, string descripcion, string tipo, string marca, string ancho, string porc)
        {

            Boolean respuesta;
            Conexion conn = new Conexion();

            
            if (ancho == "")
            {
                ancho = "0";
            }

            if (porc == "")
            {
                porc = "0";
            }
            


            int ds = conn.Count("Select count(Abreviatura) from [Producto] where Abreviatura=\'" + abrevia + "\';");

            if (ds == 0)
            {
               respuesta = conn.Crear("Producto", "Abreviatura,Descripcion, Porcentaje,Tipo,Ancho, Marca", "\'" + abrevia + "',\'" + descripcion + "',\'" + porc + "'," + tipo + "," + ancho +"," + "\'" + marca + "\'" );
                return respuesta;
            }
            return false;
        }

        //Editar Usuario
        [WebMethod]
        public static bool EditPro(string id, string abrevia, string descripcion, string tipo, string marca, string ancho, string porc)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(Producto) from [Producto] where usuario=\'" + id + "\';");

            
            if (ancho == "")
            {
                ancho = "0";
            }

            if (porc == "")
            {
                porc = "0";
            }
            

            if (ds != 0)
            {

                int cantnick = conn.Count("Select count(Abreviatura) from [Producto] where Abreviatura=\'" + abrevia + "\'  And Producto!= " + id + ";");
                if (cantnick == 0)
                {
                    return conn.Modificar("Producto", "Abreviatura" + "=" + "\'" + abrevia + "\' " + "," + "Descripcion " + " = " + "\'" + descripcion + "\' , Tipo = " + tipo + "," + " Marca " + " = " + "\'" + marca + "\' " + ","  + " Ancho " + " = " + "" + ancho + ", " + " Porcentaje " + " = " + "'" + porc + "'" , " Producto" + "= " + id);
                }

            }
            return false;
        }


        //ELIMINAR PRODUCTO
        [WebMethod]

        public static bool DeleteProd(string id)
        {
            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(Producto) from [Producto] where Producto=\'" + id + "\';");

            if (ds != 0)
            {
                return conn.Eliminar("Producto", "Producto = " + id);
            }
            return false;
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
                "<h5>Administrar Producto</h5> \n" +
                "<span>Agregar o Editar un Producto </span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal

            innerhtml +=
                "<form id=\"formulario\" class=\"form-horizontal row-fluid well\"> \n" +
                "<div class=\"modal-body\"> \n" +
                "<table border=\"0\" width=\"100%\" > \n" +
                "<div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Abreviatura:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Abreviatura\" required=\"required\" style=\"font-size: 15px;\" type=\"text\" name=\"abreviatura\" id=\"abreviatura\" runat=\"server\" class=\"span12\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Descripcion:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" type=\"text\" placeholder=\"Descripcion\" id=\"descripcion\" name=\"descripcion\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Producto:</b></label> \n" +
                "<div class=\"controls\"><select data-placeholder=\"Buscar Producto...\" name=\"producto-select\" tabindex=\"2\" class=\"select\" onChange=\"cambio()\" runat=\"server\" required=\"required\" id=\"tipopro\"></select></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Marca:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" type=\"text\" placeholder=\"Marca\" id=\"marca\" name=\"marca\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" id=\"divporc\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Porcentaje:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" type=\"text\" data-mask=\"99%\" id=\"porc\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" id=\"divtam\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Tamaño:</b></label> \n" +
                "<div class=\"controls\"><select id=\"tamano\" runat=\"server\" class=\"select\" tabindex=\"2\"> <option value=\"pequeno\">Pequeño</option> <option value=\"grande\">Grande</option> </select></div> \n" +
                "</div> \n" +
                "<tr> \n" +
                "<td colspan=\"2\"> \n" +
                "<div id=\"mensaje\"></div> \n" +
                "<div class=\"alert margin\"> \n" +
                "<button type=\"button\"  class=\"close\" data-dismiss=\"alert\">×</button> \n" +
                "Campos Obligatorios (*) \n" +
                "</div> \n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"reloadTable();\" id=\"cerrar\">Cerrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-success\" onclick=\"AddProduct();\" name=\"reg\" id=\"reg\">Registrar</button>\n" +
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

        public static string Fill()
        {
            Conexion con = new Conexion();

            DataSet data = con.Mostrar("Tipo", "Tipo,Descripcion");

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(data.GetXml());

            XmlNodeList _Deudas = xDoc.GetElementsByTagName("NewDataSet");


            string deuda = "";
            XmlNodeList lista = ((XmlElement)_Deudas[0]).GetElementsByTagName("Tipo_x002C_Descripcion");
            int cant = lista.Count;
            for (int i = 0; i < cant; i++)
            {

                XmlNodeList key = ((XmlElement)lista[i]).GetElementsByTagName("Tipo");
                XmlNodeList value = ((XmlElement)lista[i]).GetElementsByTagName("Descripcion");

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