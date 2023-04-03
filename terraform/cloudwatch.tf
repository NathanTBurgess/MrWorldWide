# CloudWatch Log Group
resource "aws_cloudwatch_log_group" "app" {
  name = "mrworldwide-today"
}