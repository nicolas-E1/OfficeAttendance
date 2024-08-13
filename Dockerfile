FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the necessary files for dotnet restore
COPY OfficeAttendance.sln .
COPY ["OfficeAttendanceAPI.Core/OfficeAttendanceAPI.Core.csproj", "OfficeAttendanceAPI.Core/"]
COPY ["OfficeAttendanceAPI.Application/OfficeAttendanceAPI.Application.csproj", "OfficeAttendanceAPI.Application/"]
COPY ["OfficeAttendanceAPI.Infrastructure/OfficeAttendanceAPI.Infrastructure.csproj", "OfficeAttendanceAPI.Infrastructure/"]
COPY ["OfficeAttendanceAPI/OfficeAttendanceAPI.csproj", "OfficeAttendanceAPI/"]
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
ENTRYPOINT ["dotnet", "OfficeAttendanceAPI.dll"]
