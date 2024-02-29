ifeq ($(OS),Windows_NT)
    NAME := $(shell powershell -Command "$$env:name")
    ENV_NAME := $(shell powershell -Command "if (Test-Path .env) { $$content = Get-Content .env; $$name = $$content -match '^NAME=(.*)$$'; if ($$name) { $$matches[1] } }")
	NAME := $(ENV_NAME)
else
    NAME := $(name)
    ENV_NAME := $(shell grep -m 1 '^NAME=' .env 2>/dev/null | cut -d '=' -f2)
	NAME := $(ENV_NAME)
endif

all:
	@echo "Current project: $(NAME)"
	@echo ""
	@echo "Usage:"
	@echo "  make install"
	@echo "  make new NAME=<project-name>"
	@echo "  make remove NAME=<project-name>"
	@echo "  make list"
	@echo "  make run NAME=<project-name>"
	@echo "  make add NAME=<project-name> PACK=<package-name>"

install:
ifeq ($(OS),Windows_NT)
	@choco install -y dotnet-6.0-sdk
else
ifeq ($(shell uname),Darwin)
	@brew tap isen-ng/dotnet-sdk-versions
	@brew install --cask dotnet-sdk6-0-400
else
	@sudo apt-get update
	@sudo apt-get install -y dotnet-sdk-6.0
endif
endif

new: .check-name
	@echo "Adding project: $(NAME)"
	@dotnet new console --framework net6.0 --use-program-main --name $(NAME)
	@dotnet sln add $(NAME)/$(NAME).csproj

remove: .check-name
	@echo "Removing project: $(NAME)"
	@dotnet sln remove $(NAME)/$(NAME).csproj
ifeq ($(OS),Windows_NT)
	@powershell -Command "Remove-Item -Recurse -Force $(NAME)"
else
	@rm -rf $(NAME)
endif

list:
	@dotnet --list-sdks
	@echo ""
	@dotnet sln list

run: .check-name
	@dotnet run --project $(NAME)

add: .check-name
	@echo "Adding $(PACK) to project: $(NAME)"
	@dotnet add $(NAME)/$(NAME).csproj package $(PACK)
	@dotnet list $(NAME) package

.check-name:
ifeq ($(OS),Windows_NT)
	@powershell -Command " \
		if (-not [String]::IsNullOrWhiteSpace('$(NAME)')) { \
			Set-Content -Path .env -Value ('NAME=$(NAME)'); \
			Write-Host 'Project name set to $(NAME) in .env file.'; \
		} elseif (-not [String]::IsNullOrWhiteSpace('$(ENV_NAME)')) { \
			$$env:NAME = '$(ENV_NAME)'; \
			Write-Host 'Using project name from .env: $(ENV_NAME)'; \
		} else { \
			Write-Host 'Name is not set. Please enter the project name:'; \
			$$name = Read-Host; \
			$$env:NAME = $$name; \
			Set-Content -Path .env -Value ('NAME=' + $$name); \
			Write-Host ('Project name set to ' + $$name + ' in .env file.'); \
		} \
	"
	$(eval NAME := $(shell powershell -Command "if (Test-Path .env) { $$content = Get-Content .env; $$name = $$content -match '^NAME=(.*)$$'; if ($$name) { $$matches[1] } }"))
else
	@if [ -z "$(NAME)" ]; then \
		if [ -z "$(ENV_NAME)" ]; then \
			echo "Name is not set. Please enter the project name:"; \
			read USER_INPUT; \
			echo "NAME=$$USER_INPUT" > .env; \
			$(eval NAME=$$USER_INPUT) \
			echo "Project name set to $$USER_INPUT in .env file."; \
		else \
			$(eval NAME=$(ENV_NAME)) \
			echo "Using project name from .env: $(ENV_NAME)"; \
		fi; \
	else \
		echo "NAME=$(NAME)" > .env; \
		echo "Project name set to $(NAME) in .env file."; \
	fi
endif
