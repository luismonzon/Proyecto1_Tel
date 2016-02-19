<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Proyecto1_Tel.Index" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio</title>
    
    <!-- Bootstrap Core CSS and Custom CSS-->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Code/css/login.css" rel="stylesheet" />
    
</head>
<body>
   <div class="login-body">
    <article class="container-login center-block">
		<section>
			<ul id="top-bar" class="nav nav-tabs nav-justified">
				<li class="active"><a href="Index.aspx">Inicio de Sesion</a></li>
				<li></li>
			</ul>
			<div class="tab-content tabs-login col-lg-12 col-md-12 col-sm-12 cols-xs-12">
				<div id="login-access" class="tab-pane fade active in">
					<h2><i class="glyphicon glyphicon-log-in"></i> Inicio de Sesion</h2>						
				
                    		<div class="form-group ">
							<label for="login" class="sr-only">Usuario</label>
								<input type="text" class="form-control" name="login" id="login_value" 
									placeholder="Email" tabindex="1" value="" />
						</div>
						<div class="form-group ">
							<label for="password" class="sr-only">Contraseñia</label>
								<input type="password" class="form-control" name="password" id="password"
									placeholder="Password" value="" tabindex="2" />
						</div>
						<div class="checkbox">
								<label class="control-label" for="remember_me">
									<input type="checkbox" name="remember_me" id="remember_me" value="1" class="" tabindex="3" /> Recordarme
								</label>
						</div>
						<br/>
						<div class="form-group ">				
								<button type="submit" name="log-me-in" id="submit" tabindex="5" class="btn btn-lg btn-primary">Entrar</button>
						</div>
						
				</div>
			</div>
		</section>
	</article>
</div>
    <!-- jQuery and Bootstrap Core-->
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        $('#submit').on('click', function () {
            $.ajax({
                type: 'POST',
                url: 'Index.aspx/sayhello',
                data: JSON.stringify({ name: 'John' }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    // Notice that msg.d is used to retrieve the result object
                    alert(msg.d);
                }
            });
        });
    </script>
</body>
</html>
