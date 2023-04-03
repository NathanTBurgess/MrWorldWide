resource "aws_organizations_account" "new_account" {
  name  = "mrworldwide-infrastructure"
  email = "fuqua.matt@gmail.com"

  parent_id = "ou-vjwu-r7jaiowf" # Replace this with the ID of the Organizational Unit you created
}

provider "aws" {
  alias  = "new_account"
  region = "us-east-1"

  assume_role {
    role_arn = "arn:aws:iam::${aws_organizations_account.new_account.id}:role/OrganizationAccountAccessRole"
  }
}