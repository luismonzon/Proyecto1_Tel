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

                conexion = new Conexion();

                tab_bodega.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
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

                DataSet ds = conexion.Mostrar("Producto", "Producto,Abreviatura");

                producto.DataSource = ds;
                producto.DataTextField = "Abreviatura";
                producto.DataValueField = "Producto";
                producto.DataBind();
                

            }
        }

        protected String Llenar_Productos()
        {
            DataSet productos = conexion.Mostrar("Producto P, Tipo T , Bodega B Where P.Producto = B.Producto  and T.Tipo = P.Tipo ", " P.PRODUCTO, P.ABREVIATURA, P.DESCRIPCION, P.PORCENTAJE," +
             "P.LARGO, P.ANCHO, P.MARCA, T.DESCRIPCION NOMBRETIPO, B.CANTIDAD CANTIDAD, B.BODEGA");
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
                                " <th  align =\"center\">Marca</th>" +
                                "<th align =\"center\">Cantidad Disponible</th>" +
                                "<th align =\"center\">Acciones</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in productos.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"abre\" runat=\"server\" align =\"Center\">" + item["ABREVIATURA"].ToString() + "</td>" +
                        "<td id=\"desc\" runat=\"server\" >" + item["DESCRIPCION"].ToString() + "</td>" +
                        "<td id=\"nombretipo\" runat=\"server\" align =\"Center\">" + item["NOMBRETIPO"].ToString() + "</td>" +
                        "<td id=\"marc\" runat=\"server\" align =\"Center\">" + item["MARCA"].ToString() + "</td>" +
                        "<td id=\"cant\" runat=\"server\" align =\"Center\">" + item["CANTIDAD"].ToString() + "</td> ";
                    data += " <td>" +
                                        "<ul class=\"table-controls\">" +
                                          " <li><a href=\"javascript:Editar_Bodega(" + item["PRODUCTO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>" +
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

            int ds = conn.Count("Select count(producto) from [Bodega] where producto=\'" + producto + "\';");

            if (ds == 0)
            {
                return conn.Crear("Bodega", "Producto, Cantidad ", "\'" + producto + "\'," + cantidad);

            }
            else
            {
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
            producto[1] = Cantidad[0].InnerText;




            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(producto);
            return json;
        }
    }
}