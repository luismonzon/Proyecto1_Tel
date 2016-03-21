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
    public partial class ClientesDeuda : System.Web.UI.Page
    {

        Conexion conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Conexion();
            Cargar();
        }

        protected void Cargar() {
            tab_roles.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
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
                                            "</div>" + LLenar_Tabla();
        }

        [WebMethod]
        private string LLenar_Tabla()
        {
            string columnas = " c.Cliente, c.Nombre, c.Apellido, sum(d.Cantidad) Credito , sc.Abono Abonado, SUM(d.Cantidad) - sc.Abono Deuda \n ";
            string condicion =
                " Deuda d join Cliente c on c.Cliente = d.Cliente left join ( \n" +
	            "   select d2.Cliente, SUM(p2.Abono) Abono \n"+
	            "   from Pago p2, Deuda d2 \n "+
	            "   where p2.Deuda = d2.Deuda \n "+
	            "   group by d2.Cliente \n"+
                ") sc on sc.Cliente = d.Cliente \n "+
                "group by c.Cliente,c.Nombre,c.Apellido, sc.Abono \n"+
                "having SUM(d.Cantidad) > sc.Abono or sc.Abono is Null \n"+
                "order by SUM(d.Cantidad) desc \n"
            ;
            DataSet roles = conn.Mostrar(condicion, columnas);
            String data = "No hay Productos Disponibles";
            if (roles.Tables.Count > 0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                               " <th  align =\"center\">Nombre</th>" +
                                "<th align =\"center\">Apellido</th>" +
                                "<th align =\"center\">Credto</th>" +
                                "<th align =\"center\">Abonado</th>" +
                                "<th align =\"center\">Deuda</th>" +
                                "<th align =\"center\">Abonar</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in roles.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>" +
                        "<td>" + item["Apellido"].ToString() + "</td>" +
                        "<td>" + item["Credito"].ToString() + "</td>";

                    if (item["Abonado"].ToString().Equals(""))
                    {
                        data += "<td> 0,00 </td>" +
                            "<td>" + item["Credito"].ToString() + "</td>" 
                            ;
                    }
                    else 
                    {
                        data += "<td>" + item["Abonado"].ToString() + "</td>" +
                            "<td>" + item["Deuda"].ToString() + "</td>" 
                            ;
                    }
                    data += " <td>" +
                    "<ul class=\"table-controls\">" +
                      " <li><a href=\"javascript:AbonarPago("+ item["Cliente"].ToString() +")\" id=\"add\" class=\"tip\" CssClass=\"Edit\" title=\"Abonar\"><i class=\"fam-add\"></i></a> </li>" +
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

        public static string Mostrar(string id)
        {

            Conexion con = new Conexion();

            /*
            select d.Deuda, d.Cantidad - sc.Abono  Debe
            from Deuda d join (
	            select d2.Deuda, case when sum(p2.Abono) is null then 0 else SUM(p2.Abono) end Abono
	            from Pago p2 right join Deuda d2 on p2.Deuda = d2.Deuda
	            where d2.Cliente = 502
	            group by d2.Deuda
            ) sc on sc.Deuda = d.Deuda
            and (d.Cantidad - sc.Abono) > 0 
            and d.Cliente = 502
             */

            string columnas = " d.Deuda, d.Cantidad - sc.Abono  Debe \n";
            string condicion = 
                " Deuda d join ( \n"+
	                "select d2.Deuda, case when sum(p2.Abono) is null then 0 else SUM(p2.Abono) end Abono \n"+
	                "from Pago p2 right join Deuda d2 on p2.Deuda = d2.Deuda \n"+
	                "where d2.Cliente = "+id+" \n"+
	                "group by d2.Deuda \n"+
                ") sc on sc.Deuda = d.Deuda \n"+
                "and (d.Cantidad - sc.Abono) > 0  \n"+
                "and d.Cliente = "+id+" \n"
            ;

            DataSet data = con.Mostrar(condicion,columnas);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(data.GetXml());

            XmlNodeList _Deudas = xDoc.GetElementsByTagName("NewDataSet");

            
            string deuda = "";
            XmlNodeList lista = ((XmlElement)_Deudas[0]).GetElementsByTagName("_x0020_d.Deuda_x002C__x0020_d.Cantidad_x0020_-_x0020_sc.Abono_x0020__x0020_Debe_x0020__x000A_");
            int cant = lista.Count;
            for (int i = 0; i < cant; i++) 
            {
                
                XmlNodeList nDeuda = ((XmlElement)lista[i]).GetElementsByTagName("Deuda");
                XmlNodeList nDebe = ((XmlElement)lista[i]).GetElementsByTagName("Debe");

                deuda += "{ \"key\":\"" + nDeuda[0].InnerText+"\",\"value\":\""+nDebe[0].InnerText+"\"}";
                if (i != cant - 1) {
                    deuda += ",";
                }
            
            }

            deuda = "[" + deuda + "]";
            //string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(deuda);
                        
            return deuda;
        }

        [WebMethod]

        public static bool Add(int id, float Cantidad) 
        {

            Conexion con = new Conexion();

            return con.Crear(" Pago ", " Abono , Deuda , Fecha ", " " + Cantidad + " , " + id + " , CONVERT (date, GETDATE()) ");

        }
    
    }
}