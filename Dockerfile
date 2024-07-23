# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080  

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["./Src/AMAK.API/AMAK.API.csproj", "./Src/AMAK.API/"]
COPY ["./Src/AMAK.Domain/AMAK.Domain.csproj", "./Src/AMAK.Domain/"] 
COPY ["./Src/AMAK.Application/AMAK.Application.csproj", "./Src/AMAK.Application/"]
COPY ["./Src/AMAK.Infrastructure/AMAK.Infrastructure.csproj", "./Src/AMAK.Infrastructure/"]

RUN dotnet restore "./Src/AMAK.API/AMAK.API.csproj"

COPY . .
WORKDIR "/src/Src/AMAK.API"
RUN dotnet build "AMAK.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
WORKDIR /src/Src/AMAK.API
RUN dotnet publish "AMAK.API.csproj" -c $BUILD_CONFIGURATION -o /publish

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "AMAK.API.dll"]
