# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copia todo o conteúdo do diretório atual (onde o Dockerfile está) para /src
COPY . .
WORKDIR /src/SmartDrones.API
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
# Cria um usuário não-root para rodar a aplicação por segurança
RUN useradd -m appuser
USER appuser
# Define o ambiente da aplicação ASP.NET Core
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_HTTP_PORTS=5000
ENV ASPNETCORE_URLS=http://+:5000
# Copia os artefatos da etapa de build para a etapa final
COPY --from=build /app/publish .
# Define o comando de entrada que será executado quando o contêiner iniciar
ENTRYPOINT ["dotnet", "SmartDrones.API.dll"]