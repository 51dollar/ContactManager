FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ContactManager/ContactManager.csproj", "ContactManager/"]
RUN dotnet restore "ContactManager/ContactManager.csproj"

COPY . .
WORKDIR "/src/ContactManager"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

RUN mkdir /data

ENTRYPOINT ["dotnet", "ContactManager.dll"]
