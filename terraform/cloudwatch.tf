# CloudWatch Log Group
resource "aws_cloudwatch_log_group" "app" {
  provider = aws.new_account
  name = "mrworldwide-today"
}