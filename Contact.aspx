<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ElectionSystems.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" 
          integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" 
          crossorigin="anonymous">
    
    <main class="container py-5">
        <section class="text-center mb-5">
            <h1 class="display-4 fw-bold text-primary">Contact Us</h1>
            <p class="lead">Should you have any queries, reach out to us using the details below.</p>
        </section>

        <section class="row">
            <div class="col-md-6 mb-4">
                <div class="card shadow-sm h-100">
                    <div class="card-body text-center">
                        <h2 class="h5 fw-bold text-primary">Our Address</h2>
                        <p class="card-text">
                            One Microsoft Way<br />
                            Redmond, WA 98052-6399<br />
                        </p>
                        <p>
                            <span class="fw-bold">Phone:</span> 090333000
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-6 mb-4">
                <div class="card shadow-sm h-100">
                    <div class="card-body text-center">
                        <h2 class="h5 fw-bold text-primary">Email Us</h2>
                        <p class="card-text">
                            <strong>Support:</strong> <a href="mailto:Support@example.com" class="text-decoration-none">support@example.com</a><br />
                            <strong>Marketing:</strong> <a href="mailto:Marketing@example.com" class="text-decoration-none">marketing@example.com</a>
                        </p>
                    </div>
                </div>
            </div>
        </section>

      <section class="text-center mt-5">
        <h3 class="fw-bold text-secondary">Follow Us</h3>
        <div class="d-flex justify-content-center gap-3 mt-3">
            <a href="https://www.facebook.com" target="_blank" class="text-decoration-none">
                <img src="SampleCandidates/facebook.jpg" alt="Facebook" class="rounded-circle" style="width: 50px; height: 50px;">
            </a>
            <a href="https://www.twitter.com" target="_blank" class="text-decoration-none">
                <img src="SampleCandidates/twitter.jpg" alt="Twitter" class="rounded-circle" style="width: 50px; height: 50px;">
            </a>
            <a href="https://www.linkedin.com" target="_blank" class="text-decoration-none">
                <img src="SampleCandidates/instagram.jpg" alt="Instagram" class="rounded-circle" style="width: 50px; height: 50px;">
            </a>
        </div>
    </section>


    </main>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
            crossorigin="anonymous"></script>
</asp:Content>
