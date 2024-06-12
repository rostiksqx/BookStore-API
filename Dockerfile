# Stage 1: Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Stage 2: Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY BookStore.sln ./
COPY BookStore.API/BookStore.API.csproj BookStore.API/
COPY BookStore.Application/BookStore.Application.csproj BookStore.Application/
COPY BookStore.Core/BookStore.Core.csproj BookStore.Core/
COPY BookStore.DataAccess/BookStore.DataAccess.csproj BookStore.DataAccess/
RUN dotnet restore

# Copy the entire project files and build
COPY . .
WORKDIR /src/BookStore.API
RUN dotnet build -c Release -o /app

# Stage 3: Publish image
FROM build AS publish
RUN dotnet publish -c Release -o /app

# Stage 4: Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BookStore.API.dll"]
