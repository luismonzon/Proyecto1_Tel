<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="Proyecto1_Tel.Code.Venta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
        <li><a href="Rol.aspx">Roles</a></li>    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		            
		 

	    <h5 class="widget-name"><i class="icon-columns"></i>Ventas</h5>
    
    <div class="row-fluid">
	            
	            	<!-- Column -->
	                <div class="span6">
	                                <div class="widget">
	                            <div class="navbar"><div class="navbar-inner"><h6>Nueva Venta</h6></div></div>

	                            <div class="well">
	                            
	                                <div class="control-group">
	                                    <label class="control-label">Codigo Cliente</label>
	                                    <div class="controls">
                                            <input type="text" name="regular" class="span12" id="idcliente" placeholder="Cod. Cliente" />
	                                        <button  id="busca" class="btn btn-primary">Buscar</button>

	                                    </div>
	                                </div>
	                                 <div class="control-group">
	                                    <label class="control-label">Codigo Cliente</label>
	                               
                                            <input type="text" name="regular" disabled="disabled" class="span12" id="codigo_cliente" placeholder="Cod. Cliente" />

	                                 </div>
	                                <div class="control-group">
	                                    <label class="control-label">Nombre Cliente</label>
	                               
                                            <input type="text" name="regular"  disabled="disabled" class="span12" id="nombre_cliente" placeholder="Nombre Cliente" />

	                                </div>
	                                
	                                <div class="control-group">
	                                    <label class="control-label">Nit Cliente</label>
	                                      <input type="text" name="regular"  disabled="disabled" class="span8" id="nit_cliente" placeholder="Nit Cliente" />

	                                </div>
	                                
                                    <div  class="control-group">
                                        <label class="control-label">Agregar Producto</label>
                                        <div id="productos" runat="server">

                                        </div>
                                        <input type="text" name="regular"  disabled="disabled" class="span3" id="cantidad" placeholder="Nit Cliente" />

                                         <button  id="agregar" class="btn btn-primary">Agregar</button>
                                    </div>
	                              
	                               
	                                
	                                <div class="form-actions align-right">
	                                    <button type="submit" id="submit" class="btn btn-primary">Submit</button>
	                                    <button type="button" class="btn btn-danger">Cancel</button>
	                                    <button type="reset" class="btn">Reset</button>
	                                </div>

	                            </div>
	                            
	                        </div>
	                   

	                    
	                </div>
	                <!-- /column -->
	            	
	                <!-- Column -->
	                <div class="span6">
	                	
	                    <!-- Horizontal form -->
	                    <form action="#" class="form-horizontal">
	                        <div class="widget">
	                            <div class="navbar"><div class="navbar-inner"><h6>Horizontal form</h6></div></div>

	                            <div class="well">
	                            
	                                <div class="control-group">
	                                    <label class="control-label">Input field</label>
	                                    <div class="controls"><input type="text" name="regular" class="span12" placeholder="Regular field" /></div>
	                                </div>
	                                
	                                <div class="control-group">
	                                    <label class="control-label">Password field</label>
	                                    <div class="controls"><input type="password" name="regular" class="span12" placeholder="Regular field" /></div>
	                                </div>
	                                
	                                <div class="control-group">
	                                    <label class="control-label">Checkboxes</label>
	                                    <div class="controls">
	                                        <label class="checkbox"><input type="checkbox" value="" checked>Option one is this and that—be sure to include why it's great</label>
	                                        <div class="gap"></div>
	                                        <label class="radio"><input type="radio" name="optionsRadios" id="optionsRadios3" value="option1" checked>Option one is this and that—be sure to include why it's great</label>
	                                        <label class="radio"><input type="radio" name="optionsRadios" id="optionsRadios4" value="option2">Option two can be something else and selecting it will deselect option one</label>
	                                    </div>
	                                </div>
	                                
	                                <div class="control-group">
	                                    <label class="control-label">Inline checkboxes:</label>
	                                    <div class="controls">
	                                        <label class="checkbox inline"><input type="checkbox" id="inlineCheckbox10" value="option1" checked>1</label>
	                                        <label class="checkbox inline"><input type="checkbox" id="inlineCheckbox11" value="option2">2</label>
	                                        <label class="checkbox inline"><input type="checkbox" id="inlineCheckbox12" value="option3">3</label>
	                                    </div>
	                                </div>
	                                
	                                <div class="control-group">
	                                    <label class="control-label">Textarea:</label>
	                                    <div class="controls">
	                                        <textarea rows="5" cols="5" name="textarea" class="span12"></textarea>
	                                    </div>
	                                </div>
	                                
	                                <div class="form-actions align-right">
	                                    <button type="submit" class="btn btn-primary">Submit</button>
	                                    <button type="button" class="btn btn-danger">Cancel</button>
	                                    <button type="reset" class="btn">Reset</button>
	                                </div>

	                            </div>
	                            
	                        </div>
	                    </form>
	                    <!-- /horizontal form -->
	                	
	                    <!-- Form inline -->
	                    <form action="#" class="form-inline">
	                        <div class="widget">
	                            <div class="navbar"><div class="navbar-inner"><h6>Inline form</h6></div></div>
	                            
	                            <div class="well">

	                                <div class="control-group">
	                                    <input type="text" class="input-small" placeholder="Email">
	                                    <input type="password" class="input-small" placeholder="Password">
										<label class="checkbox"><input type="checkbox" class="styled">Remember me</label>
										<button type="submit" class="btn">Sign in</button>
	                                </div>
	                                <div class="control-group">
	                                    <label class="checkbox"><input type="checkbox" name="checkbox1" class="styled" value="" >Unchecked</label>
	                                    <label class="radio"><input type="radio" name="radio" class="styled" value="" >Unchecked</label>
	                                    <select name="select2" class="styled">
	                                        <option value="opt1">Usual select box</option>
	                                        <option value="opt2">Option 2</option>
	                                        <option value="opt3">Option 3</option>
	                                        <option value="opt4">Option 4</option>
	                                        <option value="opt5">Option 5</option>
	                                        <option value="opt6">Option 6</option>
	                                        <option value="opt7">Option 7</option>
	                                        <option value="opt8">Option 8</option>
	                                    </select>
	                                    <span class="help-inline">This is help note</span>
	                                    <button type="submit" class="btn btn-primary">Sign in</button>
	                                </div>

	                            </div>
	                        </div>
	                    </form>
	                    <!-- /form inline -->

	                </div>
	                <!-- /column --> 
	            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
       
      <!-- Horizontal form -->
	<script type="text/javascript">
	    $('#busca').on('click', function () {
            
	        var id_cliente = document.getElementById('idcliente').value;

	        $.ajax({
	            type: 'POST',
	            url: 'Venta.aspx/Busca',
	            data: JSON.stringify({ idcliente:  id_cliente }),
	            contentType: 'application/json; charset=utf-8',
	            dataType: 'json',
	            success: function (msg) {
	                // Notice that msg.d is used to retrieve the result object
	                if (msg.d == "0") {

	                    alert("no existe");

	                }
	                else {
	                    var datos = msg.d.split(",");

	                    $("#codigo_cliente").val(datos[3]);
	                    $("#nit_cliente").val(datos[1]);
	                    $("#nombre_cliente").val(datos[0]+" "+datos[2]);

	                }
	            }
	        });
	    });


	</script>                    
	                    <!-- /horizontal form -->

</asp:Content>

