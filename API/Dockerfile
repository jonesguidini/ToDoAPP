FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["APP.API/APP.API.csproj", "APP.API/"]
RUN dotnet restore "APP.API/APP.API.csproj"
COPY . .
WORKDIR "/src/APP.API"
RUN dotnet build "APP.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APP.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APP.API.dll"]

## The maintainer name and email
MAINTAINER Jones Guidini <jonesguidini@gmail.com>
