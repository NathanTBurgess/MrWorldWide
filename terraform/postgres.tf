# EC2 Instance for PostgreSQL
resource "aws_instance" "postgres" {
  provider = aws.new_account
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