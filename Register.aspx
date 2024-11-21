<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ElectionSystems.Register" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Register</title>
    <!-- My style sheet link -->
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

        .register-container {
            background-color: var(--card-background-color);
            padding: 2rem;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            max-width: 600px; 
            max-height: 90vh; 
            width: 100%;
            text-align: center;
            overflow-y: auto; 
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
        input[type="email"],
        input[type="number"],
        .asp-button,
        select {
            width: calc(100% - 2rem);
            padding: 0.75rem;
            margin-bottom: 1rem;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 0.9rem;
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

        .cancel-button {
            background-color: var(--error-color);
        }

        .cancel-button:hover {
            background-color: darkred;
        }

        .button-container {
            display: flex;
            justify-content: space-between;
        }

        .button-container .asp-button {
            width: 48%;
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
        <div class="register-container">
            <h1>Electoral Systems Registration Form</h1>
            <label for="EmailTxtBox">E-mail:</label>
            <asp:TextBox ID="EmailTxtBox" runat="server" placeholder="Enter email" TextMode="Email"></asp:TextBox>

            <label for="NameTxtBox">Name:</label>
            <asp:TextBox ID="NameTxtBox" runat="server" placeholder="Enter name"></asp:TextBox>

            <label for="AgeTxtBox">Age:</label>
            <asp:TextBox ID="AgeTxtBox" runat="server" type="number" placeholder="Enter age"></asp:TextBox>

            <label for="DropDownListRole">Role:</label>
            <asp:DropDownList ID="DropDownListRole" runat="server">
                <asp:ListItem Text="Select Role" Value="" />
                <asp:ListItem Text="Commissioner" Value="Commissioner" />
                <asp:ListItem Text="Voter" Value="Voter" />
            </asp:DropDownList>

            <label for="PasswordTxtBox">Password:</label>
            <asp:TextBox ID="PasswordTxtBox" runat="server" type="password" placeholder="Enter password"></asp:TextBox>

            <label for="ReEnterPasswordTxtBox">Re-enter Password:</label>
            <asp:TextBox ID="ReEnterPasswordTxtBox" runat="server" type="password" placeholder="Enter password"></asp:TextBox>
            
            <label for="AddressTxtBox">Address:</label>
            <asp:TextBox ID="AddressTxtBox" runat="server" placeholder="Enter address"></asp:TextBox>
            
            <asp:Label ID="SucessMessageLabel" runat="server" ForeColor="Green"></asp:Label>
            <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red"></asp:Label>

            <div class="button-container">
                <asp:Button ID="SaveBtn" runat="server" Text="Save" CssClass="asp-button" OnClick="SaveBtn_Click" />
                <asp:Button ID="CancelBtn" runat="server" Text="Cancel" CssClass="asp-button cancel-button" OnClick="CancelBtn_Click" />
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>

