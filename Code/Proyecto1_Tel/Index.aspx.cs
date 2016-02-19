using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
namespace Proyecto1_Tel
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        [WebMethod]
        public static string SayHello(string name)
        {
            return "Hello " + name;
        }
    }
}