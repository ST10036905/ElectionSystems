<%@ Page Title="View Candidates" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vote.aspx.cs" Inherits="ElectionSystems.Vote" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- My style sheet -->
    <link rel="stylesheet" href="~/MyCSS/StyleSheet.css" />
    <main class="container py-5">
        <!-- Hero Section -->
        <section class="text-center bg-primary text-white p-5 rounded-3 mb-5">
            <h1 class="display-4 fw-bold">Candidates</h1>
            <p class="lead">Your voice matters! Cast your vote and shape the future of your community.</p>
        </section>

        <!-- Adding an inline CSS for consistent image sizes -->
        <style>
            .card-img-top {
                width: 100%;
                height: 400px;
                object-fit: cover;
            }
        </style>

        <!-- Static Candidates Section -->
        <section class="mb-5">
            <h2 class="text-center fw-bold mb-4">Meet the Candidates</h2>
            <div class="row g-4">
                <!-- Candidate 1 -->
                <div class="col-md-4">
                    <div class="card shadow-sm h-100">
                        <img src="SampleCandidates/candidate_1.jpg" class="card-img-top" alt="Jane Smith">
                        <div class="card-body text-center">
                            <h5 class="card-title fw-bold">Jane Smith</h5>
                            <p class="card-text">An experienced leader advocating for innovation and community growth.</p>
                        </div>
                        <div class="card-footer d-flex justify-content-between">
                            <button class="btn btn-danger w-100" onclick="vote('Jane Smith')">Vote</button>
                        </div>
                    </div>
                </div>

                <!-- Candidate 2 -->
                <div class="col-md-4">
                    <div class="card shadow-sm h-100">
                        <img src="SampleCandidates/candidate_2.jpg" class="card-img-top" alt="John Doe">
                        <div class="card-body text-center">
                            <h5 class="card-title fw-bold">John Doe</h5>
                            <p class="card-text">A visionary with a focus on environmental sustainability and education.</p>
                        </div>
                        <div class="card-footer d-flex justify-content-between">
                            <button class="btn btn-danger w-100" onclick="vote('John Doe')">Vote</button>
                        </div>
                    </div>
                </div>

                <!-- Candidate 3 -->
                <div class="col-md-4">
                    <div class="card shadow-sm h-100">
                        <img src="SampleCandidates/candidate_3.jpg" class="card-img-top" alt="Alex Johnson">
                        <div class="card-body text-center">
                            <h5 class="card-title fw-bold">Alex Johnson</h5>
                            <p class="card-text">A dedicated advocate for economic development and social equality.</p>
                        </div>
                        <div class="card-footer d-flex justify-content-between">
                            <button class="btn btn-danger w-100" onclick="vote('Alex Johnson')">Vote</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Dynamic Candidates Section -->
        <section class="mb-5">
            <asp:Repeater ID="RepeaterCandidates" runat="server" Visible="false">
                <ItemTemplate>
                    <div class="col-md-4">
                        <div class="card shadow-sm h-100">
                            <img src='<%# Eval("ImageUrl") %>' class="card-img-top" alt='<%# Eval("Name") %>'>
                            <div class="card-body text-center">
                                <h5 class="card-title fw-bold"><%# Eval("Name") %></h5>
                                <p class="card-text"><%# Eval("Description") %></p>
                            </div>
                            <div class="card-footer d-flex justify-content-between">
                                <button class="btn btn-success w-100 vote-btn" data-candidate='<%# Eval("Name") %>'>Vote</button>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </section>

    </main>

    <!-- Bootstrap JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
            crossorigin="anonymous"></script>
</asp:Content>
