FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["PostMortem.Web/PostMortem.Web.csproj", "PostMortem.Web/"]
RUN dotnet restore "PostMortem.Web/PostMortem.Web.csproj"
COPY . .
WORKDIR "/src/PostMortem.Web"
RUN dotnet build "PostMortem.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PostMortem.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PostMortem.Web.dll"]