# S3 Bucket for hosting React web app
resource "aws_s3_bucket" "web_app" {
  bucket = "www.mrworldwide.today"
}

resource "aws_s3_bucket_website_configuration" "web_app" {
  bucket = aws_s3_bucket.web_app.id

  index_document {
    suffix = "index.html"
  }

  error_document {
    key = "index.html"
  }
}

resource "aws_s3_bucket_public_access_block" "web_app" {
  bucket = aws_s3_bucket.web_app.id

  block_public_acls   = false
  block_public_policy = false
  ignore_public_acls  = false
  restrict_public_buckets = false
}

# S3 Bucket for user-driven blob storage
resource "aws_s3_bucket" "blob_storage" {
  bucket = "mrworldwide-today-blob-storage"
}

resource "aws_s3_bucket_public_access_block" "blob_storage" {
  bucket = aws_s3_bucket.blob_storage.id

  block_public_acls   = true
  block_public_policy = true
  ignore_public_acls  = true
  restrict_public_buckets = true
}

resource "aws_s3_bucket_policy" "web_app" {
  bucket = aws_s3_bucket.web_app.id

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "s3:GetObject"
        Effect = "Allow"
        Resource = "${aws_s3_bucket.web_app.arn}/*"
        Principal = {
          CanonicalUser = aws_cloudfront_origin_access_identity.web_app.s3_canonical_user_id
        }
      }
    ]
  })
}