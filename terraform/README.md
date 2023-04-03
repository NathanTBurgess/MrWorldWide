# MrWorldWide Infrastructure

This repository contains the Terraform code for the infrastructure used by MrWorldWide, a simple web application.

## Overview

The infrastructure consists of the following resources:

- Amazon Virtual Private Cloud (VPC)
- Amazon Elastic Container Service (ECS) for hosting the web server's containers
- Elastic Load Balancer (ELB) to route traffic to the ECS service
- Amazon Simple Storage Service (S3) for storing static files and the SPA web app
- Amazon Elastic Compute Cloud (EC2) instances for hosting a Postgres image
- Amazon Route 53 for managing the DNS records
- Amazon Certificate Manager (ACM) for managing SSL/TLS certificates
- Amazon CloudWatch for logs
- Amazon CloudFront as a CDN for the web application

## Installation

To install this infrastructure, you must have the following:

- AWS account
- Terraform installed

First, clone the repository:

```bash
git clone https://github.com/NathanTBurgess/MrWorldWide.git
```

Then, navigate to the `terraform/` directory and initialize the Terraform modules:
```bash
terraform plan
terraform apply
```

## Usage

To access the web application, navigate to the ELB URL provided by the output of `terraform apply`.

To update the infrastructure, simply update the Terraform code and run `terraform apply`.
