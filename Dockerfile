FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5148

ENV ASPNETCORE_URLS=http://+:5148

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["EmployeeManager.Application/EmployeeManager.Application.csproj", "EmployeeManager.Application/"]
RUN dotnet restore "EmployeeManager.Application/EmployeeManager.Application.csproj"
COPY . .
WORKDIR "/src/EmployeeManager.Application"
RUN dotnet build "EmployeeManager.Application.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "EmployeeManager.Application.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManager.Application.dll"]
