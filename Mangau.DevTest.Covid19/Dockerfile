#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Mangau.DevTest.Covid19/Mangau.DevTest.Covid19.csproj", "Mangau.DevTest.Covid19/"]
RUN dotnet restore "Mangau.DevTest.Covid19/Mangau.DevTest.Covid19.csproj"
COPY . .
WORKDIR "/src/Mangau.DevTest.Covid19"
RUN dotnet build "Mangau.DevTest.Covid19.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mangau.DevTest.Covid19.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mangau.DevTest.Covid19.dll"]