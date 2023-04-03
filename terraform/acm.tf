# ACM Certificate
resource "aws_acm_certificate" "wildcard" {
  provider = aws.new_account
  domain_name       = "*.mrworldwide.today"
  validation_method = "DNS"
}

resource "aws_acm_certificate_validation" "wildcard" {
  provider = aws.new_account
  certificate_arn         = aws_acm_certificate.wildcard.arn
  validation_record_fqdns = aws_route53_record.wildcard.*.fqdn
}