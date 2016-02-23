<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Proyecto1_Tel.Code.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
			<!-- Content wrapper -->
		    <div class="wrapper">

    	<!-- Basic inputs -->

	            <h5 class="widget-name"><i class="icon-align-justify"></i>Roles</h5>

	            <form class="form-horizontal" action="#">
					<fieldset>

						<!-- General form elements -->
						<div class="widget row-fluid">
						    <div class="navbar">
						        <div class="navbar-inner">
						            <h6>Full size inputs</h6>
	                                <ul class="navbar-icons">
	                                    <li><a href="#" class="tip" title="Add new option"><i class="icon-plus"></i></a></li>
	                                    <li><a href="#" class="tip" title="View statistics"><i class="icon-reorder"></i></a></li>
	                                    <li><a href="#" class="tip" title="Parameters"><i class="icon-cogs"></i></a></li>
	                                </ul>
						        </div>
						    </div>

						    <div class="well">

						    	<div class="alert margin">
						    		<button type="button" class="close" data-dismiss="alert">×</button>
						    		This is an example of full width input fields. Please find the fixed size examples below
						    	</div>
						    
						        <div class="control-group">
						            <label class="control-label">Usual input field:</label>
						            <div class="controls"><input type="text" name="regular" class="span12" /></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Password:</label>
						            <div class="controls"><input class="span12" type="password" name="pass" /></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">With placeholder:</label>
						            <div class="controls"><input class="span12" type="text" name="placeholder" placeholder="placeholder" /></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Read only field:</label>
						            <div class="controls"><input class="span12" type="text" name="readonly" readonly value="read only" /></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Disabled field:</label>
						            <div class="controls"><input type="text" name="disabled" disabled="disabled" value="disabled" class="span12" /></div>
						        </div>
	        
						        <div class="control-group">
						            <label class="control-label">Helpers:</label>
						            <div class="controls">
						                <div class="row-fluid">
						                	<div class="span4">
						                    	<input type="text" name="regular" class="span12" /><span class="help-block">Left aligned helper</span>
						                    </div>
						                	<div class="span4">
						                    	<input type="text" name="regular" class="span12" /><span class="help-block align-center">Centered helper</span>
						                    </div>
						                	<div class="span4">
						                    	<input type="text" name="regular" class="span12" /><span class="help-block align-right">Right aligned helper</span>
						                    </div>
						            	</div>
						            </div>
						        </div>

						        <div class="control-group">
						            <label class="control-label">Predefined value:</label>
						            <div class="controls"><input type="text" name="regular" value="http://" class="span12" /></div>
						        </div>

						        <div class="control-group">
						            <label class="control-label">Field with tooltip on focus:</label>
						            <div class="controls"><input type="text" name="regular" title="Tooltip on focus" class="focustip span12" /></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Field with tooltip on hover:</label>
						            <div class="controls"><input type="text" name="regular" title="Tooltip on hover" class="tip span12" /></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Max 10 characters:</label>
						            <div class="controls"><input type="text" name="regular" maxlength="10" placeholder="max 10 characters" class="span12" /></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label"><i class="icon-cog"></i>With icons:</label>
						            <div class="controls"><input type="text" name="regular" class="span12" /><i class="icon-cog field-icon"></i></div>
						        </div>
						        
						        <div class="control-group">
						            <label for="labelfor" class="control-label">Clickable label:</label>
						            <div class="controls"><input type="text" name="labelfor" id="labelfor" class="span12" /></div>
						        </div>
						        
						        <div class="control-group info">
						            <label for="inputInfo" class="control-label">Info input:</label>
						            <div class="controls"><input type="text" id="inputInfo" class="span12" /><span class="help-block">Info help line</span></div>
						        </div>
						        
						        <div class="control-group warning">
						            <label for="inputWarning" class="control-label">Warning input:</label>
						            <div class="controls"><input type="text" id="inputWarning" class="span12" /><span class="help-block">Something may have gone wrong</span></div>
						        </div>
						        
						        <div class="control-group error">
						            <label for="inputError" class="control-label">Warning input:</label>
						            <div class="controls"><input type="text" id="inputError" class="span12" /><span class="help-block">Please correct the error</span></div>
						        </div>
						        
						        <div class="control-group success">
						            <label for="inputSuccess" class="control-label">Success input:</label>
						            <div class="controls"><input type="text" id="inputSuccess" class="span12" /><span class="help-block">Woohoo!</span></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Predefined width:</label>
						            <div class="controls">
						                <input class="input-mini" type="text" placeholder=".input-mini" style="display: block;" />
						                <input class="input-small" type="text" placeholder=".input-small" style="display: block; margin-top: 6px;" />
						                <input class="input-medium" type="text" placeholder=".input-medium" style="display: block; margin-top: 6px;" />
						                <input class="input-large" type="text" placeholder=".input-large" style="display: block; margin-top: 6px;" />
						                <input class="input-xlarge" type="text" placeholder=".input-xlarge" style="display: block; margin-top: 6px;" />
						                <input class="input-xxlarge" type="text" placeholder=".input-xxlarge" style="display: block; margin-top: 6px;" />
						                <input class="input-block-level" type="text" placeholder=".input-block-level" style="display: block; margin-top: 6px;" />
						            </div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Prepend:</label>
						            <div class="controls">
						            	<div class="input-prepend">
						            		<span class="add-on">@</span><input id="prependedInput" type="text" placeholder=".input-prepend" />
						            	</div>
						        	</div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Append:</label>
						            <div class="controls">
						            	<div class="input-append">
						                	<input type="text" placeholder=".input-append" />
						                	<span class="add-on">
						                		<i class="icon-cog"></i>
						                	</span>
						            	</div>
						            </div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Both:</label>
						            <div class="controls">
						            	<div class="input-prepend input-append">
						                    <span class="add-on">
						                    	<i class="icon-bell"></i>
						                    </span>
						                    <input type="text" placeholder=".input-append .input-prepend" />
						                    <span class="add-on">@</span>
						                </div>
						            </div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Appended buttons:</label>
						            <div class="controls">
						                <div class="input-append">
						                	<input id="appendedInputButton" type="text">
						                	<button class="btn" type="button">Go!</button>
						                </div>
						                <div class="input-append" style="display: block; margin-top: 6px;">
						                	<input id="appendedInputButtons" type="text">
						                	<button class="btn" type="button">Options</button>
						                	<button class="btn" type="button">Search</button>
						                </div>
						            </div>
						        </div>

						        <div class="control-group">
						            <label class="control-label">Appended dropdowns:</label>
						            <div class="controls">

						                <!-- Prepend -->
						                <div class="input-prepend">
						                    <div class="btn-group">
						                        <button class="btn dropdown-toggle" data-toggle="dropdown">Action <span class="caret dd-caret"></span></button>
						                        <ul class="dropdown-menu">
		                                            <li><a href="#"><i class="icon-plus"></i>Add new option</a></li>
		                                            <li><a href="#"><i class="icon-reorder"></i>View statement</a></li>
		                                            <li><a href="#"><i class="icon-cogs"></i>Parameters</a></li>
						                        </ul>
						                    </div>
						                    <input id="prependedDropdownButton" type="text">
						                </div>
						                <!-- /prepend -->

						                <!-- Append, prepend -->
						                <div class="input-prepend input-append" style="display: block; margin-top: 6px;">
						                    <div class="btn-group">
						                        <button class="btn dropdown-toggle" data-toggle="dropdown">Action <span class="caret dd-caret"></span></button>
						                        <ul class="dropdown-menu">
		                                            <li><a href="#"><i class="icon-plus"></i>Add new option</a></li>
		                                            <li><a href="#"><i class="icon-reorder"></i>View statement</a></li>
		                                            <li><a href="#"><i class="icon-cogs"></i>Parameters</a></li>
						                        </ul>
						                    </div>
						                    <input id="appendedPrependedDropdownButton" type="text">
						                    <div class="btn-group">
						                        <button class="btn dropdown-toggle" data-toggle="dropdown">Action <span class="caret dd-caret"></span></button>
						                        <ul class="dropdown-menu pull-right">
		                                            <li><a href="#"><i class="icon-plus"></i>Add new option</a></li>
		                                            <li><a href="#"><i class="icon-reorder"></i>View statement</a></li>
		                                            <li><a href="#"><i class="icon-cogs"></i>Parameters</a></li>
						                        </ul>
						                    </div>
						                </div>
						                <!-- /append, prepend -->

						                <!-- Append -->
						                <div class="input-append" style="display: block; margin-top: 6px;">
						                    <input id="appendedDropdownButton" type="text">
						                    <div class="btn-group">
						                        <button class="btn dropdown-toggle" data-toggle="dropdown">Action <span class="caret dd-caret"></span></button>
						                        <ul class="dropdown-menu pull-right">
		                                            <li><a href="#"><i class="icon-plus"></i>Add new option</a></li>
		                                            <li><a href="#"><i class="icon-reorder"></i>View statement</a></li>
		                                            <li><a href="#"><i class="icon-cogs"></i>Parameters</a></li>
						                        </ul>
						                    </div>
						                </div>
						                <!-- /append -->

						            </div>
						        </div>

						        <div class="control-group">
						            <label class="control-label">Textarea:</label>
						            <div class="controls"><textarea rows="5" cols="5" name="textarea" class="span12"></textarea></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Elastic textarea:</label>
						            <div class="controls"><textarea rows="5" cols="5" name="textarea" class="auto span12"></textarea></div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">With character counter:</label>
						            <div class="controls">
						                <textarea rows="5" cols="5" name="textarea" class="limited span12"></textarea>
						                <span class="help-block" id="limit-text">Field limited to 100 characters.</span>
						            </div>
						        </div>
						        
						        <div class="control-group">
						            <label class="control-label">Tags:</label>
						            <div class="controls"><input type="text" id="tags2" class="tags" value="these,are,sample,tags" /></div>
						        </div>

						        <div class="control-group">
						            <label class="control-label">Tags with autocomplete:</label>
						            <div class="controls"><input type="text" id="tags3" class="tags-autocomplete" value="tags,with,autocomplete" /></div>
						        </div>

						    </div>
						</div>
						<!-- /general form elements -->

					</fieldset> 

					<fieldset>

						<!-- HTML5 inputs -->
						<div class="widget row-fluid">
							<div class="navbar"><div class="navbar-inner"><h6>HTML5 inputs</h6></div></div>

							<div class="well">

						    	<div class="alert margin">
						    		<button type="button" class="close" data-dismiss="alert">×</button>
						    		These inputs are mostly for mobile devices. They open a necessary keyboard on iPhone/iPad. And in most cases are useless on desktop pc's
						    	</div>

								<div class="control-group">
									<label class="control-label">Datetime:</label>
									<div class="controls"><input class="span12" type="datetime" name="datetime" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Datetime local:</label>
									<div class="controls"><input class="span12" type="datetime-local" name="datetime-local" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Date:</label>
									<div class="controls"><input class="span12" type="date" name="date" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Month:</label>
									<div class="controls"><input class="span12" type="month" name="month" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Time:</label>
									<div class="controls"><input class="span12" type="time" name="time" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Week:</label>
									<div class="controls"><input class="span12" type="week" name="week" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Number:</label>
									<div class="controls"><input class="span12" type="number" name="number" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Email:</label>
									<div class="controls"><input class="span12" type="email" name="email" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Url:</label>
									<div class="controls"><input class="span12" type="url" name="url" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Search:</label>
									<div class="controls"><input class="span12" type="search" name="search" /></div>
								</div>

								<div class="control-group">
									<label class="control-label">Tel:</label>
									<div class="controls"><input class="span12" type="tel" name="tel" /></div>
								</div>

							</div>
						</div>
						<!-- /HTML5 inputs -->

					</fieldset>
				</form>
				<!-- /basic inputs -->

                </div>
          
    <div>
        <h1 style="font-family: Calibri; font-size: 50px"></h1>
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
