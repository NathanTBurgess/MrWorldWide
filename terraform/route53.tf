# Route53 Record for ACM validation
resource "aws_route53_zone" "mrworldwide" {
  name = "mrworldwide.today"
}