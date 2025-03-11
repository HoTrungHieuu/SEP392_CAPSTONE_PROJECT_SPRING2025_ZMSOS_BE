# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy và restore riêng các file .csproj trước
COPY ["Zoo_Management_And_Staff_Operations_System/Zoo_Management_And_Staff_Operations_System.csproj", "Zoo_Management_And_Staff_Operations_System/"]
COPY ["BO/BO.csproj", "BO/"]
COPY ["DAO/DAO.csproj", "DAO/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Service/Service.csproj", "Service/"]

RUN dotnet restore "Zoo_Management_And_Staff_Operations_System/Zoo_Management_And_Staff_Operations_System.csproj"

# Copy toàn bộ source code
COPY . .

# Build và publish
WORKDIR "/src/Zoo_Management_And_Staff_Operations_System"
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Zoo_Management_And_Staff_Operations_System.dll", "--urls=http://0.0.0.0:80"]
