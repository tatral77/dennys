<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LMS.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <style type="text/css">
        .logintext {
            border: none;
            border-bottom: solid thin;
            border-bottom-color: #fff;
            background-color: #808080;
            color: #fff;
        }

        .btn-cutom {
            background-color: #fff;
            color: #000;
        }

        .header {
            margin-top: 5px;
            margin-bottom: 5px;
            padding: 20px;
            border-bottom: solid thin;
        }

        .login {
            background-color: #FAF9F6;
            color: #000;
            padding: 10px;
            border-radius: 10px;
            font-family: Verdana;
            border:solid;
            border-color:#000;
        }

        .login-header {
            padding-bottom: 5px;
            padding-top: 5px;
            font-size: 20px;
            font-family: Verdana;
        }

        #errorlbl {
            padding-top: 10px;
            color: coral;
            font-weight: bold;
        }



        .pointer {
  position: relative;
  background: red;
}
.pointer:after {
  content: "";
  position: absolute;
  left: 0;
  bottom: 0;
  width: 0;
  height: 0;
  border-left: 40px solid white;
  border-top: 55px solid transparent;
  border-bottom: 55px solid transparent;
}
.pointer:before {
  content: "";
  position: absolute;
  right: -20px;
  bottom: 0;
  width: 0;
  height: 0;
  border-left: 20px solid red;
 
  border-top: 20px solid transparent;
  border-bottom: 20px solid transparent;
}
  

    </style>
  <%--  <h2><%: Title %>.</h2>--%>
    <div class="container">
          <h3> Login</h3>
    </div>
    <div class="container login" style="width: 50%; margin-top: 30px">
        
        <hr />
       <%-- <div class="row">
            <div class="col-md-12 login-header" style="text-align: center">
               
            </div>
        </div>--%>
           <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-3">
             
                    <asp:Label ID="Label1" runat="server" Text="User Name :"></asp:Label>
                </div>
                <div class="col-md-9">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="The email field is required." />
                </div>
            </div>
      
        <div class="row" style="margin-bottom: 5px;">
            <div class="col-md-3">
               
               
                    <asp:Label ID="Label2" runat="server" Text="Password :"></asp:Label>
                </div>
                <div class="col-md-9">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                </div>
            </div>
       
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <div class="checkbox">
                    <asp:CheckBox runat="server" ID="RememberMe" />
                    <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-default" />
            </div>
        </div>
    </div>









    <%-- <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                 <%--   <h4>Use a local account to log in.</h4>
                    <hr />
                  
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                        <div class="col-md-10">
                           
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                        <div class="col-md-10">
                          
                        </div>
                    </div>
                   
                </div>
             <%--   <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                </p>--%>
    <%-- <p>
                     Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                    
                </p>
            </section>
        </div>

      <%--  <div class="col-md-4">
            <section id="socialLoginForm">
                <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
            </section>
        </div>
    </div>--%>
</asp:Content>
