# Elastic Container Registry
resource "aws_ecr_repository" "app" {
  provider = aws.new_account
  name = "mrworldwide-today"
}