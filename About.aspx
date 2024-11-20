<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ElectionSystems.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <!-- My style sheet-->
 <link rel="stylesheet" href="~/MyCSS/StyleSheet.css" />
    <main class="container py-5">
        <section class="text-center mb-5">
            <h1 class="display-4 fw-bold text-primary">About page</h1>
            <p class="lead">Learn more about our mission, values, and the purpose behind this application.</p>
        </section>

        <section class="row mb-5">
            <div class="col-md-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-secondary fw-bold">Vision</h5>
                        <p class="card-text">We aim to empower democracy through innovative technology, ensuring secure, transparent, and seamless elections for all. Our platform is built to make election processes simple and accessible for everyone.</p>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-secondary fw-bold">What is offered</h5>
                        <ul class="list-unstyled">
                            <li><i class="bi bi-check-circle-fill text-success"></i> Secure and efficient election management</li>
                            <li><i class="bi bi-check-circle-fill text-success"></i> Real-time poll results</li>
                            <li><i class="bi bi-check-circle-fill text-success"></i> Candidate information and voter engagement tools</li>
                        </ul>
                    </div>
                </div>
            </div>
        </section>

            <h2 class="text-center fw-bold text-primary mb-4">Why choose us?</h2>
            <div class="row text-center">
                <div class="col-md-4 mb-4">
                    <div class="card shadow-sm">
                        <div class="card-body">
                            <i class="bi bi-shield-lock-fill text-primary fs-1"></i>
                            <h5 class="card-title mt-3">Security</h5>
                            <p class="card-text">Top-notch security features to protect voter data and ensure election integrity.</p>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 mb-4">
                    <div class="card shadow-sm">
                        <div class="card-body">
                            <i class="bi bi-graph-up-arrow text-success fs-1"></i>
                            <h5 class="card-title mt-3">Transparency</h5>
                            <p class="card-text">Real-time updates and transparent processes for trustworthy results.</p>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 mb-4">
                    <div class="card shadow-sm">
                        <div class="card-body">
                            <i class="bi bi-people-fill text-danger fs-1"></i>
                            <h5 class="card-title mt-3">Accessibility</h5>
                            <p class="card-text">A user-friendly platform designed for voters and administrators alike.</p>
                        </div>
                    </div>
                </div>
            </div>
    </main>
     <!-- Bootstrap JS Bundle -->
 <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
         integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
         crossorigin="anonymous"></script>
 </asp:content>
