<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ElectionSystems._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <!-- My style sheet-->
 <link rel="stylesheet" href="~/MyCSS/StyleSheet.css" />
    <main class="container py-5">
        <!-- Hero Section -->
        <section class="text-center bg-primary text-white p-5 rounded-3 mb-5">
            <h1 class="display-4 fw-bold">Election Systems</h1>
            <p class="lead">Empowering democracy with seamless and secure elections.</p>
        </section>

         <!--Adding an inline CSS for consistent image sizes -->
        <style>
            .card-img-top {
                width: 100%;
                height: 400px; /* Fixed height */
                object-fit: cover; /* Ensuring the image scales and crops properly */
            }
        </style>

         <!-- Features Section -->
         <section class="row text-center mb-5">
             <div class="col-md-6">
                 <div class="card shadow-sm h-100">
                     <div class="card-body">
                         <h2 class="card-title h5">Login</h2>
                         <p class="card-text">Sign in to participate in the election process.</p>
                         <a href="Login.aspx" class="btn btn-outline-primary">Login &raquo;</a>
                     </div>
                 </div>
             </div>
             <div class="col-md-6">
                 <div class="card shadow-sm h-100">
                     <div class="card-body">
                         <h2 class="card-title h5">Live Poll Results</h2>
                         <p class="card-text">See real-time polling results and track voter participation percentages.</p>
                         <a href="#results" class="btn btn-outline-primary">View Results &raquo;</a>
                     </div>
                 </div>
             </div>
         </section>

        <!-- Meet the Candidates Section -->
        <section class="mb-5">
            <h2 class="text-center fw-bold mb-4">Meet the candidates</h2>
            <div class="row g-4">
                <!-- Candidate 1 -->
                <div class="col-md-4">
                    <div class="card shadow-sm h-100">
                        <img src="SampleCandidates/candidate_1.jpg" class="card-img-top" alt="John Doe">
                        <div class="card-body text-center">
                            <h5 class="card-title fw-bold">Jane Smith</h5>
                            <p class="card-text">An experienced leader advocating for innovation and community growth.</p>
                        </div>
                        <div class="card-footer d-flex justify-content-between">
                            <a href="#know-more-john" class="btn btn-primary w-50 me-1">Know More</a>
                            <a href="#vote-john" class="btn btn-danger w-50 ms-1">Vote</a>
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
                            <a href="#know-more-jane" class="btn btn-primary w-50 me-1">Know More</a>
                            <a href="#vote-jane" class="btn btn-danger w-50 ms-1">Vote</a>
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
                            <a href="#know-more-alex" class="btn btn-primary w-50 me-1">Know More</a>
                            <a href="#vote-alex" class="btn btn-danger w-50 ms-1">Vote</a>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Live Poll Results -->
        <section id="results" class="text-center bg-light p-5 rounded-3">
            <h2 class="mb-4">Live Poll Results</h2>
            <div class="progress mb-3">
                <div class="progress-bar bg-primary" role="progressbar" style="width: 50%" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100">Candidate A - 50%</div>
            </div>
            <div class="progress mb-3">
                <div class="progress-bar bg-success" role="progressbar" style="width: 30%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100">Candidate B - 30%</div>
            </div>
            <div class="progress mb-3">
                <div class="progress-bar bg-danger" role="progressbar" style="width: 20%" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100">Candidate C - 20%</div>
            </div>
            <p class="mt-4">Total Votes Cast: <strong>100</strong></p>
            <p>Percentage of Population Voted: <strong>60%</strong></p>
        </section>
    </main>

    <!-- Bootstrap JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
            crossorigin="anonymous"></script>
</asp:Content>
