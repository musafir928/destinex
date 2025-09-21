FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

COPY API/*.csproj .
RUN dotnet restore

COPY API/. .
RUN dotnet publish --no-restore -o /app

FROM node:24-alpine AS client-build
WORKDIR home/node

COPY client/. .
RUN npm ci 
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine
COPY --from=build /app .
COPY --from=client-build home-node/dist/client/browser wwwroot/

EXPOSE 8080
ENTRYPOINT [ "./API" ]