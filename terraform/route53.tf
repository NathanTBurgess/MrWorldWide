# Route53 Record for ACM validation
resource "aws_route53_zone" "mrworldwide" {
  provider = aws.new_account
  name = "mrworldwide.today"
}

resource "aws_route53_record" "wildcard" {
  provider = aws.new_account
  count   = length(aws_acm_certificate.wildcard.domain_validation_options)
  zone_id = aws_route53_zone.mrworldwide.id
  name    = aws_acm_certificate.wildcard.domain_validation_options[count.index].resource_record_name
  type    = aws_acm_certificate.wildcard.domain_validation_options[count.index].resource_record_type
  records = [aws_acm_certificate.wildcard.domain_validation_options[count.index].resource_record_value]
  ttl     = 60
}