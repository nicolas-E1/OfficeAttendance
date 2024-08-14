PROJECT_NAME=office-attendance-api
COMPOSE_FILE=docker-compose.yml

# Default target
.PHONY: all
all: build up migrate

# Build the Docker containers
.PHONY: build
build:
	docker-compose -f $(COMPOSE_FILE) -p $(PROJECT_NAME) build

# Start the Docker containers
.PHONY: up
up:
	docker-compose -f $(COMPOSE_FILE) -p $(PROJECT_NAME) up -d

# Stop the Docker containers
.PHONY: down
down:
	docker-compose -f $(COMPOSE_FILE) -p $(PROJECT_NAME) down

# Run database migrations
.PHONY: migrate
migrate:
	dotnet ef database update --project OfficeAttendance.Infrastructure/OfficeAttendance.Infrastructure.csproj

# View logs
.PHONY: logs
logs:
	docker-compose -f $(COMPOSE_FILE) -p $(PROJECT_NAME) logs -f

# Restart the Docker containers
.PHONY: restart
restart: down up

# Clean up Docker resources
.PHONY: clean
clean:
	docker-compose -f $(COMPOSE_FILE) -p $(PROJECT_NAME) down -v --rmi all --remove-orphans
	docker system prune -f
	
# Run tests with coverage
.PHONY: test
test:
	dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/
