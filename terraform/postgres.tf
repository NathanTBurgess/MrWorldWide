resource "aws_key_pair" "ec2_key_pair" {
  key_name   = "ssh-key-pair"
  public_key = file(var.public_key_path)
}

resource "aws_security_group" "postgres" {
  name_prefix = "mrworldwide-today-postgres-sg"

  ingress {
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]  # SSH access from anywhere
  }

  ingress {
    from_port       = 5432
    to_port         = 5432
    protocol        = "tcp"
    security_groups = [aws_security_group.app_sg.id] # allow traffic from ECS
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Name = "mrworldwide-today-postgres-sg"
  }
}
# EC2 Instance for PostgreSQL
resource "aws_instance" "postgres" {
  ami                         = "ami-007855ac798b5175e" # Ubuntu, 22.04 LTS
  instance_type               = "t2.micro"
  associate_public_ip_address = true
  key_name                    = aws_key_pair.ec2_key_pair.key_name
  user_data                   = <<-EOF
              #!/bin/bash
              sudo apt-get update
              sudo apt-get install postgresql postgresql-contrib
              EOF

  tags = {
    Name = "mrworldwide-today-postgres"
  }

  depends_on = [
    aws_security_group.postgres
  ]
}
