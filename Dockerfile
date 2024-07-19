FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build


WORKDIR /app


COPY ./Src/AMAK.API/AMAK.API.csproj ./Src/AMAK.API/
COPY ./Src/AMAK.Domain/AMAK.Domain.csproj ./Src/AMAK.Domain/
COPY ./Src/AMAK.Application/AMAK.Application.csproj ./Src/AMAK.Application/
COPY ./Src/AMAK.Infrastructure/AMAK.Infrastructure.csproj ./Src/AMAK.Infrastructure/


RUN dotnet restore ./Src/AMAK.API/AMAK.API.csproj

COPY Src ./Src

RUN dotnet publish ./Src/AMAK.API/AMAK.API.csproj -c Release -o /publish


EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "AMAK.API.dll"]
