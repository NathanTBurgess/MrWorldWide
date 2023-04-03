# Default VPC and Subnets
resource "aws_default_vpc" "default" {
}

data "aws_availability_zones" "available" {
  state = "available"
}

resource "aws_default_subnet" "default" {
  count             = length(data.aws_availability_zones.available.names)
  availability_zone = data.aws_availability_zones.available.names[count.index]
}