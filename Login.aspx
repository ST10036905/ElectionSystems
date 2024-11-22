<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ElectionSystems.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <!-- My style sheet-->
    <link rel="stylesheet" href="~/MyCSS/StyleSheet.css" />
    <style type="text/css">
            :root {
                --primary-color: #007bff; 
                --secondary-color: #0056b3; 
                --background-color: #f4f6f9; 
                --card-background-color: white;
                --error-color: red; 
                --success-color: green; 
            }

            body {
                background-color: var(--background-color);
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                display: flex;
                justify-content: center;
                align-items: center;
                height: 100vh;
                margin: 0;
            }

            .login-container {
                background-color: var(--card-background-color);
                padding: 2rem;
                border-radius: 10px;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                max-width: 400px;
                width: 100%;
                text-align: center;
            }

            h1 {
                color: var(--primary-color);
                margin-bottom: 1rem;
            }

            label {
                display: block;
                margin-bottom: 0.5rem;
                text-align: left;
                color: var(--primary-color);
            }

            input[type="text"],
            input[type="password"],
            .asp-button {
                width: 100%;
                padding: 0.75rem;
                margin-bottom: 1rem;
                border: 1px solid #ccc;
                border-radius: 5px;
            }

            .asp-button {
                background-color: var(--primary-color);
                color: white;
                border: none;
                cursor: pointer;
                transition: background-color 0.3s;
            }

            .asp-button:hover {
                background-color: var(--secondary-color);
            }

            .button-container {
                display: flex;
                justify-content: space-between;
            }

            .button-container .asp-button {
                width: 48%;
            }

            .cancel-button {
                background-color: var(--error-color); 
                color: white; 
                border: none;
                cursor: pointer;
                transition: background-color 0.3s;
            }

            .cancel-button:hover {
                background-color: darkred; 
            }


            .create-account a {
                color: var(--primary-color);
                text-decoration: none;
            }

            .create-account a:hover {
                text-decoration: underline;
            }

            .error-message {
                color: var(--error-color);
            }

            .success-message {
                color: var(--success-color);
            }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h1>Login to your account</h1>
            <label for="UsernameTxtBox">Username:</label>
            <asp:TextBox ID="EmailTxtBox" runat="server" placeholder="Enter your email"></asp:TextBox>

            <label for="PasswordTxtBox">Password:</label>
            <asp:TextBox ID="PasswordTxtBox" type="password" runat="server" placeholder="Enter your password"></asp:TextBox>

             <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red"></asp:Label>
             <asp:Label ID="SucessMessageLabel" runat="server" ForeColor="Green"></asp:Label>
          

            <div class="button-container">
                <asp:Button ID="LoginBtn" runat="server" Text="Login" CssClass="asp-button" OnClick="LoginBtn_Click" />
                <asp:Button ID="CancelBtn" runat="server" Text="Cancel" CssClass="asp-button cancel-button" OnClick="CancelBtn_Click" />
            </div>

            <div class="create-account">
                <asp:HyperLink ID="CreateAccountLink" runat="server" NavigateUrl="Register.aspx">Don't have an account? Create one</asp:HyperLink>
            </div>

        </div>
    </form>
 <!-- Bootstrap JS Bundle -->
 <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
         integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
         crossorigin="anonymous"></script>
</body>
</html>


