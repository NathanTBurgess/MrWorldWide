# Default VPC and Subnets
resource "aws_default_vpc" "default" {
  provider = aws.new_account
}

data "aws_availability_zones" "available" {
  state = "available"
}

resource "aws_default_subnet" "default" {
  provider          = aws.new_account
  count             = length(data.aws_availability_zones.available.names)
  default_vpc_id    = aws_default_vpc.default.id
  availability_zone = data.aws_availability_zones.available.names[count.index]
}