output "ecr_repository_url" {
  description = "ECR Repository URL"
  value       = aws_ecr_repository.app.repository_url
}

output "web_app_url" {
  description = "URL for the React web app hosted on S3"
  value       = aws_s3_bucket.web_app.website_endpoint
}

output "blob_storage_bucket" {
  description = "S3 bucket for user-driven blob storage"
  value       = aws_s3_bucket.blob_storage.bucket
}

output "postgres_endpoint" {
  description = "PostgreSQL EC2 instance public IP"
  value       = aws_instance.postgres.public_ip
}

