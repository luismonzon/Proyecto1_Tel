<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="ClienteMasGasta.aspx.cs" Inherits="Proyecto1_Tel.Code.ClienteMasGasta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <li><a href="ClienteMasGasta.aspx">Clientes Que Mas Gastan</a></li>    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h4 class="widget-name"><i class="icon-columns"></i>Clientes Que Mas Compran</h4>
    <!-- Some controlы -->
    <div class="widget" id="tab_roles" runat="server">
    </div>
    <!-- /some controlы -->
    <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
    <div id="modalprodcliente" runat="server">
        
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    
    <script type="text/javascript">
        
        function VerProductos(id)
        {
            document.getElementById("<% = codigo.ClientID%>").value = id;
            var identi = document.getElementById("<% = codigo.ClientID%>").value;
            $.ajax({
                type: 'POST',
                url: 'ClienteMasGasta.aspx/Mostrar',
                data: JSON.stringify({ id: identi}),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var $modal = $('#ContentPlaceHolder1_modalprodcliente');
                    $modal.html(response.d);
                    $('#modal-pago').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });
                    
                    //$('#modal-pago').modal('hide').data('bs.modal', null);
                }
            });
        }
        
    </script>
  
</asp:Content>
