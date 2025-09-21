FROM node:24-alpine AS client-build
WORKDIR /app/client
COPY ./client/ ./
RUN npm ci
RUN npm run build -- --configuration production

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY ./API/*.csproj ./api/
RUN dotnet restore ./api

COPY ./API/ ./api/
WORKDIR /src/api

COPY --from=client-build /app/client/dist ./wwwroot/

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "API.dll"]