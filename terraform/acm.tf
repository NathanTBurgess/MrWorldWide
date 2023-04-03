# ACM Certificate
resource "aws_acm_certificate" "wildcard" {
  domain_name       = "*.mrworldwide.today"
  validation_method = "DNS"
}

resource "aws_acm_certificate_validation" "wildcard" {
  certificate_arn         = aws_acm_certificate.wildcard.arn
}