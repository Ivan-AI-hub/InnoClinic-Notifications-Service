#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NotificationAPI.Web/NotificationAPI.Web.csproj", "NotificationAPI.Web/"]
COPY ["NotificationAPI.Application.Abstraction/NotificationAPI.Application.Abstraction.csproj", "NotificationAPI.Application.Abstraction/"]
COPY ["NotificationAPI.Application/NotificationAPI.Application.csproj", "NotificationAPI.Application/"]
COPY ["NotificationAPI.Domain/NotificationAPI.Domain.csproj", "NotificationAPI.Domain/"]
COPY ["NotificationAPI.Persistence/NotificationAPI.Persistence.csproj", "NotificationAPI.Persistence/"]
COPY ["NotificationAPI.Presentation/NotificationAPI.Presentation.csproj", "NotificationAPI.Presentation/"]
RUN dotnet restore "NotificationAPI.Web/NotificationAPI.Web.csproj"
COPY . .
WORKDIR "/src/NotificationAPI.Web"
RUN dotnet build "NotificationAPI.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationAPI.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationAPI.Web.dll"]