# ACM Certificate
data "aws_acm_certificate" "wildcard" {
  domain = "*.mrworldwide.today"
}
