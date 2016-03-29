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
    public partial class Inv_Bodega : System.Web.UI.Page
    {

        Conexion conexion;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Inv_Bodega"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

               

                conexion = new Conexion();

                tab_bodega.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                                     "<h6>Productos en Bodega</h6>" +
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


                

            }


            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }
        /*
                DataSet ds = conexion.Mostrar("Producto", "Producto,Abreviatura");

                producto.DataSource = ds;
                producto.DataTextField = "Abreviatura";
                producto.DataValueField = "Producto";
                producto.DataBind();
         
         */

        protected String Llenar_Productos()
        {
            DataSet productos = conexion.Mostrar("Producto P, Tipo T , Bodega B Where P.Producto = B.Producto  and T.Tipo = P.Tipo ", " P.PRODUCTO, P.ABREVIATURA, P.DESCRIPCION, P.PORCENTAJE," +
             "P.ANCHO, P.MARCA, T.DESCRIPCION NOMBRETIPO, B.CANTIDAD CANTIDAD, B.BODEGA");
            String data = "";
            string rol = (string)Session["Rol"];
            if (productos.Tables[0].Rows.Count > 0)
            {
                if (rol == "1")
                {
                    data = "<div class=\"table-overflow\"> " +
                                       "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                                           "<thead>" +
                                               "<tr>" +
                                                   "<th  align =\"center\">Abreviatura</th>" +
                                                  " <th  align =\"center\">Descripcion</th>" +
                                                  " <th  align =\"center\">Tipo</th>" +
                                                   " <th  align =\"center\">Marca</th>" +
                                                   "<th align =\"center\">Cantidad Disponible</th>" +
                                                   "<th align =\"center\">Acciones</th>" +
                                               "</tr>" +
                                           "</thead>" + "<tbody>";
                }
                else
                {
                    data = "<div class=\"table-overflow\"> " +
                   "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                       "<thead>" +
                           "<tr>" +
                               "<th  align =\"center\">Abreviatura</th>" +
                              " <th  align =\"center\">Descripcion</th>" +
                              " <th  align =\"center\">Tipo</th>" +
                               " <th  align =\"center\">Marca</th>" +
                             "<th align =\"center\">Cantidad Disponible</th>" +
                             //  "<th align =\"center\">Acciones</th>" +
                           "</tr>" +
                       "</thead>" + "<tbody>";

                }
               
                foreach (DataRow item in productos.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"abre\" runat=\"server\" align =\"Center\">" + item["ABREVIATURA"].ToString() + "</td>" +
                        "<td id=\"desc\" runat=\"server\" >" + item["DESCRIPCION"].ToString() + "</td>" +
                        "<td id=\"nombretipo\" runat=\"server\" align =\"Center\">" + item["NOMBRETIPO"].ToString() + "</td>" +
                        "<td id=\"marc\" runat=\"server\" align =\"Center\">" + item["MARCA"].ToString() + "</td>" +
                        "<td id=\"cant\" runat=\"server\" align =\"Center\">" + item["CANTIDAD"].ToString() + "</td> ";


                    if (rol == "1")
                    {
                        data += " <td>" +
                                        "<ul class=\"table-controls\">" +
                                          " <li><a href=\"javascript:Editar_Bodega(" + item["PRODUCTO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>" +
                                          " <li><a href=\"javascript:Eliminar_Bodega(" + item["PRODUCTO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Eliminar\"><i class=\"fam-cross\"></i></a> </li>" +
                                    "</td>";
                    }
                    
                    
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
        public static bool Add(string producto, string cantidad)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(producto) from [Bodega] where producto=\'" + producto + "\';");

            if (ds == 0)
            {
                return conn.Crear("Bodega", "Producto, Cantidad ", "\'" + producto + "\'," + cantidad );

            }
            else
            {
                DataSet Producto_ = conn.Buscar_Mostrar("Bodega", "Producto" + "= " + producto);

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(Producto_.GetXml());

                XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
                XmlNodeList cant = ((XmlElement)_Producto[0]).GetElementsByTagName("Cantidad");
                string cantid = cant[0].InnerText;
                
                return conn.Modificar("Bodega","Cantidad =" + (Convert.ToInt64(cantid) + Convert.ToInt64(cantidad)), "Producto=" + producto);            
            }
           

        }


        [WebMethod]
        public static bool Rest(string producto, string cantidad)
        {


            Conexion conn = new Conexion();

           
                DataSet Producto_ = conn.Buscar_Mostrar("Bodega", "Producto" + "= " + producto);

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(Producto_.GetXml());

                XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
                XmlNodeList cant = ((XmlElement)_Producto[0]).GetElementsByTagName("Cantidad");
                string cantid = cant[0].InnerText;

                if (Convert.ToInt64(cantidad) > Convert.ToInt64(cantid))
                {
                    return false;
                }
                else
                {
                    return conn.Modificar("Bodega", "Cantidad =" + (Convert.ToInt64(cantid) - Convert.ToInt64(cantidad)), "Producto=" + producto);
                }   

                
            


        }



        [WebMethod]
        public static string Busca_Descripcion(string id)
        {
            Conexion conn = new Conexion();

            DataSet Producto_ = conn.Buscar_Mostrar("Producto", "Producto" + "= " + id);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Producto_.GetXml());
            XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList lista_producto = ((XmlElement)_Producto[0]).GetElementsByTagName("Producto_x003D__x0020_" + id);



            DataSet Bodega_ = conn.Buscar_Mostrar("Bodega", "Producto" + "= " + id);
            XmlDocument xBod = new XmlDocument();
            xBod.LoadXml(Bodega_.GetXml());
            XmlNodeList _Bodega = xBod.GetElementsByTagName("NewDataSet");
            XmlNodeList Cantidad = ((XmlElement)_Bodega[0]).GetElementsByTagName("Cantidad");


            XmlNodeList nDescripcion = ((XmlElement)lista_producto[0]).GetElementsByTagName("Descripcion");
            

            string[] producto = new string[2];
            producto[0] = nDescripcion[0].InnerText;

            try
            {

                producto[1] = Cantidad[0].InnerText;

            }
            catch (Exception ex)
            {
               string MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();
            }

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(producto);
            return json;
        }

        //ELIMINAR PRODUCTO DE TIENDA
        [WebMethod]

        public static bool DeleteProd(string id)
        {
            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(Producto) from [Bodega] where Producto=\'" + id + "\';");

            if (ds != 0)
            {
                return conn.Eliminar("Bodega", "Producto = " + id);
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
                "<i>B</i> \n" +
                "<h5>Administrar Producto en Bodega</h5> \n" +
                "<span>Agregar o Editar Productos en Bodega</span> \n" +
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
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Producto:</b></label> \n" +
                "<div class=\"controls\"><select tabindex=\"2\" style=\"font-size: 15px;\" data-placeholder=\"Buscar Producto...\" name=\"producto-select\" class=\"select\" onChange=\"cambio();\" runat=\"server\"  id=\"producto\"></select></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Descripcion:</b></label> \n" +
                "<div class=\"controls\"><input readonly=\"readonly\" style=\"font-size: 14px;\" type=\"text\" placeholder=\"Descripcion\" id=\"descripcion\" name=\"descripcion\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" id=\"divcantidad\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Cantidad Disponible:</b></label> \n" +
                "<div class=\"controls\"><input  readonly=\"readonly\" required=\"required\" style=\"font-size: 14px;\" type=\"number\"  id=\"cantdisponible\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" > \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Cantidad:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" placeholder=\"Cantidad\" type=\"number\"  id=\"cantidad\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div id=\"radio\"> \n" +
                "<label class=\"radio\">\n" +
                "<input type=\"radio\" name=\"opcion\" id=\"agregar\" class=\"styled\" value=\"1\" checked=\"checked\">\n" +
                "Agregar\n" +
                "</label>\n" +
                "<label class=\"radio\">\n" +
                "<input type=\"radio\" name=\"opcion\" id=\"quitar\"  class=\"styled\" value=\"2\">\n" +
                "Quitar\n" +
                "</label>\n" +
                "</div>\n" +
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

            DataSet data = con.Mostrar("Producto", "Producto,Abreviatura");

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(data.GetXml());

            XmlNodeList _Deudas = xDoc.GetElementsByTagName("NewDataSet");


            string deuda = "";
            XmlNodeList lista = ((XmlElement)_Deudas[0]).GetElementsByTagName("Producto_x002C_Abreviatura");
            int cant = lista.Count;
            for (int i = 0; i < cant; i++)
            {

                XmlNodeList key = ((XmlElement)lista[i]).GetElementsByTagName("Producto");
                XmlNodeList value = ((XmlElement)lista[i]).GetElementsByTagName("Abreviatura");

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