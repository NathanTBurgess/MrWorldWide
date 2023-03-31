provider "aws" {
  region = "us-west-2"
}

# Elastic Container Registry
resource "aws_ecr_repository" "app" {
  name = "mrworldwide-today"
}

# ECS Cluster
resource "aws_ecs_cluster" "app" {
  name = "mrworldwide-today-cluster"
}

# ECS Task Definition
resource "aws_ecs_task_definition" "app" {
  family                   = "mrworldwide-today"
  requires_compatibilities = ["FARGATE"]
  network_mode             = "awsvpc"
  cpu                      = "256"
  memory                   = "512"
  execution_role_arn       = aws_iam_role.ecs_execution_role.arn
  task_role_arn            = aws_iam_role.ecs_task_role.arn

  container_definitions = jsonencode([
    {
      name  = "webserver"
      image = "${aws_ecr_repository.app.repository_url}:latest"
      portMappings = [
        {
          containerPort = 80
          hostPort      = 80
          protocol      = "tcp"
        }
      ]
    }
  ])
}

# S3 Bucket for hosting React web app
resource "aws_s3_bucket" "web_app" {
  bucket = "mrworldwide-today-webapp"
  acl    = "public-read"

  website {
    index_document = "index.html"
    error_document = "error.html"
  }
}

# S3 Bucket for user-driven blob storage
resource "aws_s3_bucket" "blob_storage" {
  bucket = "mrworldwide-today-blob-storage"
  acl    = "private"
}

# EC2 Instance for PostgreSQL
resource "aws_instance" "postgres" {
  ami           = "ami-0c55b159cbfafe1f0" # Amazon Linux 2 LTS AMI
  instance_type = "t2.micro"

  user_data = <<-EOF
              #!/bin/bash
              sudo amazon-linux-extras install -y postgresql10
              sudo systemctl enable postgresql
              sudo systemctl start postgresql
              EOF

  tags = {
    Name = "mrworldwide-today-postgres"
  }
}

# Output PostgreSQL endpoint
output "postgres_endpoint" {
  value = aws_instance.postgres.public_ip
}

# ECS Service
resource "aws_ecs_service" "app" {
  name            = "mrworldwide-today-service"
  cluster         = aws_ecs_cluster.app.id
  task_definition = aws_ecs_task_definition.app.arn
  launch_type     = "FARGATE"

  desired_count = 1

  network_configuration {
    subnets = aws_default_subnet.default.*.id
  }
}

# IAM Roles
resource "aws_iam_role" "ecs_execution_role" {
  name = "ecs_execution_role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Effect = "Allow"
        Principal = {
          Service = "ecs-tasks.amazonaws.com"
        }
      }
       ]
  })
}

resource "aws_iam_role_policy_attachment" "ecs_execution_role" {
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
  role       = aws_iam_role.ecs_execution_role.name
}

resource "aws_iam_role" "ecs_task_role" {
  name = "ecs_task_role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Effect = "Allow"
        Principal = {
          Service = "ecs-tasks.amazonaws.com"
        }
      }
    ]
  })
}

resource "aws_security_group" "ecs_service" {
  name        = "ecs_service"
  description = "Allow inbound traffic to ECS service"

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_default_vpc" "default" {}

resource "aws_default_subnet" "default" {
  count = length(aws_default_vpc.default.cidr_block_association_set)
  default_vpc_id = aws_default_vpc.default.id
}

output "web_app_url" {
  value = aws_s3_bucket.web_app.website_endpoint
}

