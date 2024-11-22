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
                            <button class="btn btn-success w-100 vote-btn" onclick="vote('Jane Smith')">Vote</button>
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
                            <button class="btn btn-success w-100 vote-btn" onclick="vote('John Doe')">Vote</button>
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
                            <button class="btn btn-success w-100 vote-btn" onclick="vote('Alex Johnson')">Vote</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- Repeater to display candidates dynamically -->
        <section>
            <div class="container">
                <div class="row g-4">
                    <asp:Repeater ID="CandidatesRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-md-4">
                                <div class="card shadow-sm h-100">
                                    <!-- Image from the repeater -->
                                    <img src="<%# Eval("Image") %>" class="card-img-top" alt="<%# Eval("Name") %>">
                                    <div class="card-body text-center">
                                        <h5 class="card-title fw-bold"><%# Eval("Name") %></h5>
                                        <p class="card-text"><%# Eval("Description") %></p>
                                    </div>
                                    <div class="card-footer d-flex justify-content-between">
                                        <!-- Vote button -->
                                        <button class="btn btn-success w-100 vote-btn" 
                                                runat="server" 
                                                commandname="Vote" 
                                                commandargument='<%# Eval("Name") %>'>Vote</button>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </section>
    </main>

     <!-- Hidden Thank You Message -->
    <div id="thankYouMessage" style="display: none;">
        <p class="alert alert-success">Thank you for your vote! Your vote has been submitted successfully.</p>
    </div>
        <!-- Error and Success Messages -->
    <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="Red" CssClass="error-message"></asp:Label>
    <asp:Label ID="SucessMessageLabel" runat="server" ForeColor="Green" CssClass="success-message"></asp:Label>

    <script>
        function vote(candidateName, button) {
            // Check if the user is logged in (you can check it with ASP.NET Identity or session)
            var isLoggedIn = <%: User.Identity.IsAuthenticated ? "true" : "false" %>;

            // If the user is not logged in, show error message
            if (!isLoggedIn) {
                document.getElementById('ErrorMessageLabel').innerText = "You must be logged in to vote.";
                return;
            }

            // Disable the button after voting
            button.disabled = true;

            // Make an AJAX request to save the vote
            fetch('/Vote.aspx/VoteForCandidate', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ candidateName: candidateName })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        // Display a success message and redirect to the home page
                        document.getElementById('SuccessMessageLabel').innerText = "Thank you for your vote! You will be redirected to the homepage.";
                        setTimeout(function () {
                            window.location.href = '/Default.aspx';  // Change to your homepage URL
                        }, 2000);
                    } else {
                        // Display error if something went wrong
                        document.getElementById('ErrorMessageLabel').innerText = 'There was an error submitting your vote.';
                    }
                });
        }

        function updateVoteProgress(candidateName) {
            // Fetch the updated vote counts for all candidates (or just the selected one)
            fetch('/Vote.aspx/GetVoteCount', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    // Find the candidate's progress bar by ID
                    var candidate = data.find(item => item.name === candidateName);
                    if (candidate) {
                        var progressBar = document.getElementById('progress_' + candidateName);
                        var percentage = (candidate.votes / candidate.totalVotes) * 100; // Calculate the percentage
                        progressBar.style.width = percentage + '%';
                        progressBar.setAttribute('aria-valuenow', percentage);
                        progressBar.textContent = Math.round(percentage) + '%'; // Display the percentage text
            }
    </script>

    <!-- Bootstrap JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
            crossorigin="anonymous"></script>
</asp:Content>
