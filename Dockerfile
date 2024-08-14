FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the necessary files for dotnet restore
COPY OfficeAttendance.sln .
COPY ["OfficeAttendance.Core/OfficeAttendance.Core.csproj", "OfficeAttendance.Core/"]
COPY ["OfficeAttendance.Application/OfficeAttendance.Application.csproj", "OfficeAttendance.Application/"]
COPY ["OfficeAttendance.Infrastructure/OfficeAttendance.Infrastructure.csproj", "OfficeAttendance.Infrastructure/"]
COPY ["OfficeAttendance.Tests/OfficeAttendance.Tests.csproj", "OfficeAttendance.Tests/"]
COPY ["OfficeAttendance.WebAPI/OfficeAttendance.WebAPI.csproj", "OfficeAttendance.WebAPI/"]
WORKDIR /app

RUN dotnet restore OfficeAttendance.sln

# Copy the rest of the files and publish the application
COPY . .
RUN dotnet publish OfficeAttendance.sln -c Release -o out

# Use the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published files from the build image
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "OfficeAttendance.WebAPI.dll"]
