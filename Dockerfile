##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## Stage 1: Build the .NET Backend
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend-build
#WORKDIR /src
#
## Copy the project file and restore dependencies
#COPY ElectricityTariffTest.Server/ElectricityTariffTest.Server.csproj ElectricityTariffTest.Server/
#RUN dotnet restore ElectricityTariffTest.Server/ElectricityTariffTest.Server.csproj
#
## Copy the rest of the backend code and build it
#COPY ElectricityTariffTest.Server/ ElectricityTariffTest.Server/
#WORKDIR /src/ElectricityTariffTest.Server
#RUN dotnet build -c Release -o /app/build
#RUN dotnet publish -c Release -o /app/publish
#
## Stage 2: Build the React + Vite Frontend
#FROM node:18-alpine AS frontend-build
#WORKDIR /src
#
## Copy the frontend project files and install dependencies
#COPY electricitytarifftest.client/package*.json electricitytarifftest.client/
#RUN npm install --prefix electricitytarifftest.client
#
## Copy the rest of the frontend code and build it
#COPY electricitytarifftest.client/ electricitytarifftest.client/
#RUN npm run build --prefix electricitytarifftest.client
#
## Stage 3: Final Stage for Production
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
#WORKDIR /app
#
## Copy the published backend code
#COPY --from=backend-build /app/publish ./backend
#
## Copy the frontend build output
#COPY --from=frontend-build /src/electricitytarifftest.client/dist ./frontend
#
## Expose the necessary ports
#EXPOSE 5000
#
## Set the entry point for the backend
#ENTRYPOINT ["dotnet", "ElectricityTariffTest.Server.dll"]


# Stage 1: Build the .NET Backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend-build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ElectricityTariffTest.Server/ElectricityTariffTest.Server.csproj ElectricityTariffTest.Server/
RUN dotnet restore ElectricityTariffTest.Server/ElectricityTariffTest.Server.csproj

# Copy the rest of the backend code and build it
COPY ElectricityTariffTest.Server/ ElectricityTariffTest.Server/
WORKDIR /src/ElectricityTariffTest.Server
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Build the React + Vite Frontend
FROM node:18-alpine AS frontend-build
WORKDIR /src

# Copy the frontend project files and install dependencies
COPY electricitytarifftest.client/package*.json electricitytarifftest.client/
RUN npm install --prefix electricitytarifftest.client

# Copy the rest of the frontend code and build it
COPY electricitytarifftest.client/ electricitytarifftest.client/
RUN npm run build --prefix electricitytarifftest.client

# Stage 3: Final Stage for Production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published backend code
COPY --from=backend-build /app/publish .

# Copy the frontend build output and ensure it's served by the backend
COPY --from=frontend-build /src/electricitytarifftest.client/dist ./wwwroot

# Expose the necessary ports
EXPOSE 5000

# Set the entry point for the backend
ENTRYPOINT ["dotnet", "ElectricityTariffTest.Server.dll"]