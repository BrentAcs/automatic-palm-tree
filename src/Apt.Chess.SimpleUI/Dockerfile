FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Apt.Chess.SimpleUI/Apt.Chess.SimpleUI.csproj", "Apt.Chess.SimpleUI/"]
RUN dotnet restore "src/Apt.Chess.SimpleUI/Apt.Chess.SimpleUI.csproj"
COPY . .
WORKDIR "/src/Apt.Chess.SimpleUI"
RUN dotnet build "Apt.Chess.SimpleUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Apt.Chess.SimpleUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apt.Chess.SimpleUI.dll"]
