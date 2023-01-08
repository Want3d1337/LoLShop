#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Web/LoLShop.Web/LoLShop.Web.csproj", "Web/LoLShop.Web/"]
COPY ["Web/LoLShop.Web.ViewModels/LoLShop.Web.ViewModels.csproj", "Web/LoLShop.Web.ViewModels/"]
COPY ["Services/LoLShop.Services.Mapping/LoLShop.Services.Mapping.csproj", "Services/LoLShop.Services.Mapping/"]
COPY ["Data/LoLShop.Data.Models/LoLShop.Data.Models.csproj", "Data/LoLShop.Data.Models/"]
COPY ["LoLShop.Common/LoLShop.Common.csproj", "LoLShop.Common/"]
COPY ["Data/LoLShop.Data.Common/LoLShop.Data.Common.csproj", "Data/LoLShop.Data.Common/"]
COPY ["Services/LoLShop.Services.Messaging/LoLShop.Services.Messaging.csproj", "Services/LoLShop.Services.Messaging/"]
COPY ["Services/LoLShop.Services.Data/LoLShop.Services.Data.csproj", "Services/LoLShop.Services.Data/"]
COPY ["Services/LoLShop.Services/LoLShop.Services.csproj", "Services/LoLShop.Services/"]
COPY ["Web/LoLShop.Web.Infrastructure/LoLShop.Web.Infrastructure.csproj", "Web/LoLShop.Web.Infrastructure/"]
COPY ["Data/LoLShop.Data/LoLShop.Data.csproj", "Data/LoLShop.Data/"]
RUN dotnet restore "Web/LoLShop.Web/LoLShop.Web.csproj"
COPY . .
WORKDIR "/src/Web/LoLShop.Web"
RUN dotnet build "LoLShop.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoLShop.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoLShop.Web.dll"]