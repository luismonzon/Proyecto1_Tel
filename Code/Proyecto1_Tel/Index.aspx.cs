using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Proyecto1_Tel.Code;
namespace Proyecto1_Tel
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
        }
        
        [WebMethod]
        public static string Log(string usuario, string password)
        {

            Conexion conexion = new Conexion();

            if (conexion.Entrar(usuario, password))
                return "1";


            return "0";
        }
    }
}