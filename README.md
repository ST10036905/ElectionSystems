## Mayra Selemane
## Electoral Systems Application
## 22/11/2024

Version Control : https://github.com/ST10036905/ElectionSystems.git

The robust election systems was designed in the most user friendly way to accommodate the different users accessing the application. 
The interaction between the front end and the back end was made simple and precise.

Features : 
## Admin Registration and Login
Admins can register and log in to access the admin dashboard.

## Voter Registration and Login
Voters can register be registered in the "Add voter" where the admin is entitled to do so, they can then log in to participate in the election process.

## Add New Voter
Admins can add new voters to the system to allow them to vote.

## Add New Candidate
Admins can add new candidates who will be part of the election process.

## Voting System
Registered voters can vote for their preferred candidates.

## Member (Commissioner) Dashboard
Admins (commissioners) can view a dashboard that shows the total number of voters and candidates added, as well as track election progress.

## Real-Time Poll Results
Display live poll results as votes are cast, with progress bars showing percentages for each candidate.

## MongoDB Authentication
This project uses MongoDB for storing election-related data such as voter and candidate information. The MongoDB database is authenticated using the connection string provided in the class declarations.
Database Connection: The MongoDB connection is configured using a MongoDB Atlas cloud instance. The connection string contains authentication credentials (username and password) to securely access the database.

## How Authentication Works:
When the admin logs in, their credentials are verified against MongoDB's user records, additionally , libraries were used to enforce the password by hashing it with the SHA-256 algorithm.
MongoDB Atlas handles authentication and connection security, ensuring that only authorized users can access the database.

Steps on how to run and compile the application : 
Admin Registration
1.Navigate to the Admin Registration page (/Register.aspx).
2.Fill in the required details (e.g., username, password) and click Register.
3.After registration, go to the Admin Login page (/Login.aspx) to log in.
Voter Registration
1.Navigate to the Voter Registration page (/AddVoter.aspx).
2.Enter voter details (e.g., name, email, password) and click Register.
3.Once registered, log in as a voter via the Voter Login page (/AddVoter.aspx).
Admin Dashboard (Commissioner)
1.After logging in as an admin, you'll be redirected to the Admin Dashboard (Member dashboard).
2.From here, you can view the list of added voters and candidates, and access features to add new voters and candidates.
Add Voter
1.As an admin, navigate to the Add Voter page (AddVoter.aspx).
2.Fill in the voter details and click Add Voter.
Add Candidate
1.Admins can add candidates to the election by going to the Add Candidate page (AddCandidate.aspx).
2.Enter the candidate’s details and click Add Candidate.
Voting
1.Registered voters can log in and go to the Voting Page (Vote.aspx).
2.Select the candidate they wish to vote for and click Submit Vote.
Real-Time Poll Results
1.The Poll Results page (/Default.aspx) shows the live vote count for each candidate, with progress bars displaying voting percentages.
This data is updated every 5 seconds.

## License
This project is licensed under the MIT License - see the LICENSE file for details.
