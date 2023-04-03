# ECS Cluster
resource "aws_ecs_cluster" "app" {
  name = "mrworldwide-today-cluster"
}

# ECS Task Definition
resource "aws_ecs_task_definition" "app" {
  family                   = "mrworldwide-today"
  requires_compatibilities = ["FARGATE"]
  network_mode             = "awsvpc"
  cpu                      = "256"
  memory                   = "512"
  execution_role_arn       = aws_iam_role.ecs_execution_role.arn
  task_role_arn            = aws_iam_role.ecs_task_role.arn

  container_definitions = jsonencode([
    {
      name         = "webserver"
      image        = "${aws_ecr_repository.app.repository_url}:latest"
      portMappings = [
        {
          containerPort = 80
          hostPort      = 80
          protocol      = "tcp"
        }
      ]
      logConfiguration = {
        logDriver = "awslogs"
        options   = {
          "awslogs-region"        = "us-east-1"
          "awslogs-group"         = aws_cloudwatch_log_group.app.name
          "awslogs-stream-prefix" = "webserver"
        }
      }
    }
  ])
}

## ECS Service
resource "aws_security_group" "app_sg" {
  name_prefix = "mrworldwide-today-"
  ingress {
    from_port       = 80
    to_port         = 80
    protocol        = "tcp"
    security_groups = [aws_security_group.alb.id]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
  tags = {
    Name = "mrworldwide-today-app-sg"
  }
}
resource "aws_ecs_service" "app" {
  name            = "mrworldwide-today-service"
  cluster         = aws_ecs_cluster.app.id
  task_definition = aws_ecs_task_definition.app.arn
  launch_type     = "FARGATE"

  desired_count = 1

  network_configuration {
    assign_public_ip = true
    subnets         = aws_default_subnet.default.*.id
    security_groups = [aws_security_group.app_sg.id]
  }

  load_balancer {
    target_group_arn = aws_lb_target_group.app.arn
    container_name   = "webserver"
    container_port   = 80
  }
}

# ECS Autoscaling
resource "aws_appautoscaling_target" "app" {
  service_namespace  = "ecs"
  scalable_dimension = "ecs:service:DesiredCount"
  resource_id        = "service/${aws_ecs_cluster.app.name}/${aws_ecs_service.app.name}"
  min_capacity       = 1
  max_capacity       = 3
}

resource "aws_appautoscaling_policy" "app_cpu" {
  name               = "cpu_scaling_policy"
  service_namespace  = aws_appautoscaling_target.app.service_namespace
  scalable_dimension = aws_appautoscaling_target.app.scalable_dimension
  resource_id        = aws_appautoscaling_target.app.resource_id
  policy_type        = "TargetTrackingScaling"

  target_tracking_scaling_policy_configuration {
    predefined_metric_specification {
      predefined_metric_type = "ECSServiceAverageCPUUtilization"
    }

    target_value       = 50.0
    scale_in_cooldown  = 300
    scale_out_cooldown = 300
  }
}

#CloudWatch Alarms
resource "aws_cloudwatch_metric_alarm" "cpu_utilization" {
  alarm_name          = "cpu_utilization_alarm"
  comparison_operator = "GreaterThanOrEqualToThreshold"
  evaluation_periods  = "1"
  metric_name         = "CPUUtilization"
  namespace           = "AWS/ECS"
  period              = "60"
  statistic           = "SampleCount"
  threshold           = "75"
  alarm_description   = "This metric checks for high CPU utilization"
  alarm_actions       = [] # Add SNS topic ARN here to send notifications
  dimensions          = {
    ClusterName = aws_ecs_cluster.app.name
    ServiceName = aws_ecs_service.app.name
  }
}

resource "aws_cloudwatch_metric_alarm" "health_check" {
  alarm_name          = "health_check_alarm"
  comparison_operator = "LessThanThreshold"
  evaluation_periods  = "1"
  metric_name         = "HealthyHostCount"
  namespace           = "AWS/ApplicationELB"
  period              = "60"
  statistic           = "SampleCount"
  threshold           = 1
  alarm_description   = "This metric checks if there are no healthy hosts based on the /health endpoint"
  alarm_actions       = [] # Add SNS topic ARN here to send notifications
  dimensions          = {
    LoadBalancer = aws_lb.app.name
    TargetGroup  = aws_lb_target_group.app.name
  }
}
