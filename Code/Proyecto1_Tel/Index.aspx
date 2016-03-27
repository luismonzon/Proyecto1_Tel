<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Proyecto1_Tel.Index" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    
    <!-- Bootstrap Core CSS and Custom CSS-->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Code/css/login.css" rel="stylesheet" />
    <script type="text/javascript">
        window.onload = function () {
            Objeto = document.getElementsByTagName("a");
            for (a = 0; a < Objeto.length; a++) {
                Objeto[a].onclick = function () {
                    location.replace("Index.aspx");
                    return false;
                }
            }
        }

        if (history.foward()) {
            location.replace(history.foward());
        }

</script>


</head>
<body>
   <div class="login-body">
    <article class="container-login center-block">
		<section>
			<div class="tab-content tabs-login col-lg-12 col-md-12 col-sm-12 cols-xs-12">
				<div id="login-access" class="tab-pane fade active in">
					<h2><i class="glyphicon glyphicon-log-in"></i> Proteccion Solar</h2>						
				
                    		<div class="form-group ">
							<label for="login" class="sr-only">Usuario</label>
								<input type="text" class="form-control" name="login" id="user" 
									placeholder="Usuario" tabindex="1" value="" />
						</div>
						<div class="form-group ">
							<label for="password" class="sr-only">Contraseña</label>
								<input type="password" class="form-control" name="password" id="password"
									placeholder="Password" value="" tabindex="2" />
						</div>
                        <div class="form-group">
						<div id="mensaje"></div>
                        </div>
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


            var user = document.getElementById('user').value;
            var pass = document.getElementById('password').value;
            
            $.ajax({
                type: 'POST',
                url: 'Index.aspx/Log',
                data: JSON.stringify({ usuario: user, password: pass }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    // Notice that msg.d is used to retrieve the result object
                    if (msg.d == "1") {
                        window.location.href = "/Code/Inicio.aspx";

                    }
                    else {
                        $('#mensaje').removeClass();
                        $('#mensaje').addClass('alert alert-danger').html('Usuario o Contraseña Incorrecta').show(200).delay(2500).hide(200);
                    }
                }
            });
        });
    </script>
</body>
</html>
