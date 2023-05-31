#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Asp.NetCore.MVC/Asp.NetCore.MVC.csproj", "Asp.NetCore.MVC/"]
RUN dotnet restore "Asp.NetCore.MVC/Asp.NetCore.MVC.csproj"
COPY . .
WORKDIR "/src/Asp.NetCore.MVC"
RUN dotnet build "Asp.NetCore.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Asp.NetCore.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asp.NetCore.MVC.dll"]