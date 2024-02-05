FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookDemo.csproj", "BookDemo/"]
RUN dotnet restore "./BookDemo/BookDemo.csproj"
COPY . .
WORKDIR "/src/BookDemo"
RUN dotnet publish "./BookDemo.csproj" -o /app/build
ENTRYPOINT ["dotnet", "BookDemo.dll"]

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "BookDemo.dll"]
