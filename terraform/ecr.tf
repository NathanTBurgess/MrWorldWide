# Elastic Container Registry
resource "aws_ecr_repository" "app" {
  name = "mrworldwide-today"
  image_tag_mutability = "MUTABLE"
  image_scanning_configuration {
    scan_on_push = true
  }
}