<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberDashboard.aspx.cs" MasterPageFile="~/Site.Master" Inherits="ElectionSystems.MemberDashboard" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="~/MyCSS/StyleSheet.css" />
    <style type="text/css">
        :root {
            --primary-color: #007bff;
            --secondary-color: #0056b3;
            --background-color: #f4f6f9;
            --card-background-color: white;
            --text-color: #333;
        }

        body {
            background-color: var(--background-color);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
        }

        .dashboard-container {
            background-color: var(--card-background-color);
            max-width: 900px;
            width: 100%;
            padding: 2rem;
            margin: 2rem auto;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        h1 {
            color: var(--primary-color);
            text-align: center;
            margin-bottom: 1.5rem;
        }

        .content-section {
            margin-bottom: 1.5rem;
        }

        .content-section h2 {
            font-size: 1.25rem;
            color: var(--primary-color);
            border-bottom: 1px solid #ccc;
            margin-bottom: 1rem;
        }

        .locations-list {
            list-style: none;
            padding: 0;
        }

        .locations-list li {
            margin: 0.5rem 0;
            color: var(--text-color);
        }

       
        .action-buttons {
            display: flex;
            justify-content: center;
            gap: 1rem;
            margin-top: 1.5rem;
        }

        .action-buttons a {
            text-decoration: none;
            color: white;
            background-color: #0056b3;
            padding: 10px 20px;
            border-radius: 5px;
            font-size: 1rem;
            text-align: center;
        }

        .action-buttons a:hover {
            background-color: var(--secondary-color);
        }

        .content-section {
            text-align: center;
            margin-top: 2rem;
        }

        #VoterCountLabel {
            font-weight: bold;
            margin-top: 0.5rem;
            display: block;
        }
    </style>

    <div class="dashboard-container">
        <h1>Commission member dashboard</h1>
        <div class="content-section">
            <h2>Welcome</h2>
            <p style="color: var(--text-color);">
                Use the navigation bar to manage voters, candidates, and other data related to the election process. Please ensure all data entered is accurate.
            </p>
        </div>

        <div class="content-section">
            <h2>Our Locations</h2>
            <ul class="locations-list">
                <li>Western Cape</li>
                <li>Gauteng</li>
                <li>Eastern Cape</li>
                <li>KwaZulu-Natal</li>
                <li>Northern Cape</li>
                <li>Limpopo</li>
                <li>Mpumalanga</li>
            </ul>
        </div>

         <div class="action-buttons">
            <a href="AddVoter.aspx">Add Voter</a>
            <a href="AddCandidate.aspx">Add Candidate</a>
        </div>

         <div class="content-section">
         <h2>Voter Statistics</h2>
         <asp:Label ID="VoterCountLabel" runat="server" style="font-size: 1.2rem; color: var(--primary-color);"></asp:Label>
         </div>

         <div class="content-section">
         <h2>Candidates Statistics</h2>
         <asp:Label ID="CandidatesCountLabel" runat="server" style="font-size: 1.2rem; color: var(--primary-color);"></asp:Label>
         </div>
    </div>
</asp:Content>

