using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_Tel.Code
{
    public class Validacion
    {
        public static bool validar_sesion(Sesion n, string pagina)
        {
            string tmp = n.Tipo.ToLower();

            switch (tmp)
            {
                case "1":
                    switch (pagina)
                    {
                            //Administrador tiene acceso a todas las paginas y todas las funciones
                        case "Cliente":
                        case "ClienteMasGasta":
                        case "ClienteDeuda":
                        case "Depositos":
                        case "Inicio":
                        case "Inv_Bodega":
                        case "Inv_tienda":
                        case "Producto":
                        case "MasVendidoAnio":
                        case "MasVendidoDia":
                        case "MasVendidoMes":
                        case "MasVendidoSemana":
                        case "Usuarios":
                        case "Venta":
                        case "VentaDiaria":
                        case "VentaSemanal":
                        case "VentaMensual":
                        case "VentaAnual":
                        case "Roles":
                            return true;
                        default:
                            return false;
                    }
                case "2":
                    switch (pagina)
                    {
                            //Bodega
                        case "Inv_Bodega":
                        case "Inicio":
                        case "Inv_tienda":
                        case "Producto":
                            return true;
                        default:
                            return false;
                    }

                case "3":
                    switch (pagina)
                    {
                            //Venta
                        case "Cliente":
                        case "Depositos":
                        case "Inv_tienda":
                        case "Venta":
                        case "ClienteDeuda":
                        case "Inicio":
                        case "Producto":
                        case "VentaDiaria":
                            return true;
                        default:
                            return false;
                    }

            }

            return false;   //si algun usuario con un rol no definido no tiene ningun permiso
        }
    }
}