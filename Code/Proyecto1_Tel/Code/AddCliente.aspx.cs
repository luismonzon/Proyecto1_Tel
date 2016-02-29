using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Proyecto1_Tel.Code
{
    public partial class AddCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static bool Add(string nombre, string nit)
        {

            

            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(nit) from [cliente] where nit=\'" + nit + "\';");

            if (ds == 0)
            {
                conn.Crear("Cliente", "Nombre , Nit ", "\'" + nombre + "\','" + nit + "\'");
                return true;
            }

            return false;
         }


    }
}