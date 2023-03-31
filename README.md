# MrWorldWide

Welcome to MrWorldWide - the go-to place to keep tabs on a diginom digital nomster!
This app allows our globe-trotting friend to share their current location, adventures, 
and experiences with friends and family. With continued development, MrWorldWide will 
evolve into a feature-rich blog and photo journal.

## Features

- Real-time location tracking
- Blog for sharing stories and experiences
- Photo journal for documenting adventures

## Technical Overview

MrWorldWide is built using the following technologies:

- C# web server using ASP.NET Core
- React web application for frontend
- AWS infrastructure for hosting and storage
- Terraform for infrastructure-as-code


### Setup and Deployment

1. Clone the repository and navigate to the project root.
2. Install the required dependencies for the server and app.
3. Set up your AWS infrastructure using the provided Terraform files.
4. Deploy the C# web server using the appropriate Docker container and push it to the ECR repository.
5. Deploy the React web application to the S3 bucket for hosting.
6. Configure the server to communicate with the PostgreSQL database hosted on the EC2 instance.

For more detailed instructions, please refer to the individual README files in each directory.
