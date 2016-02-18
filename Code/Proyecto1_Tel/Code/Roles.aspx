<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Proyecto1_Tel.Code.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1 style="font-family: Calibri; font-size: 50px">Roles</h1>
    </div>
    <div>

    </div>
     <form id="Grid" runat="server">
                <div align="center">
                     <asp:Button  class="btn btn-default navbar-btn" ID="Button1" runat="server" Text="Agregar Rol" Font-Bold="True" Font-Size="Medium" Height="45px" Width="140px" OnClick="Button1_Click" />
        
                </div>
    
                <asp:GridView  ID="GridView1" HorizontalAlign="Center" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Rol" DataSourceID="SqlDataSource4" ForeColor="#333333" GridLines="None">
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
               <Columns>
                   <asp:BoundField DataField="Rol" HeaderText="Rol" InsertVisible="False" ReadOnly="True" SortExpression="Rol" />
                   <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                   <asp:CommandField ButtonType="Image" ShowDeleteButton="True" ShowEditButton="True" HeaderText="Administrar" CancelImageUrl="~/img/cancelar-icono-9428-32.png" DeleteImageUrl="~/img/eliminar-un-grupo-icono-6756-32.png" EditImageUrl="~/img/editar-usuario-icono-9786-32.png" UpdateImageUrl="~/img/actualizar-restaure-todas-las-pestanas-icono-7808-32.png" NewImageUrl="~/img/agregar-usuarios-icono-3782-32.png" />
               </Columns>
               <EditRowStyle BackColor="#999999" />
               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#E9E7E2" />
               <SortedAscendingHeaderStyle BackColor="#506C8C" />
               <SortedDescendingCellStyle BackColor="#FFFDF8" />
               <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
           </asp:GridView>
           <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:TorreCon %>" SelectCommand="SELECT * FROM [Rol]" DeleteCommand="DELETE FROM Rol where Rol= @Rol" UpdateCommand="UPDATE ROL SET Nombre = @Nombre where Rol= @Rol;" InsertCommand="INSERT INTO Rol(Nombre) VALUES (@Nombre)">
               <DeleteParameters>
                   <asp:Parameter Name="Rol" />
               </DeleteParameters>
               <InsertParameters>
                   <asp:Parameter Name="Nombre" />
               </InsertParameters>
               <UpdateParameters>
                   <asp:Parameter Name="Nombre" />
                   <asp:Parameter Name="Rol" />
               </UpdateParameters>
           </asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDataSource3" runat="server"></asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </form>
        
</asp:Content>
