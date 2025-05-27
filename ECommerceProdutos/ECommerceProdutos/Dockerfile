# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos de projeto (csproj) para dentro do container
COPY ./ECommerceProdutos/ECommerceProdutos.csproj ./ECommerceProdutos/
COPY ./Domain/Domain.csproj ./Domain/
COPY ./Application/Application.csproj ./Application/
COPY ./Infra/Infra.csproj ./Infra/

# Restaurar as dependências do projeto Api
RUN dotnet restore ./ECommerceProdutos/ECommerceProdutos.csproj

# Copiar todo o código para dentro do container
COPY ./ECommerceProdutos/ ./ECommerceProdutos/
COPY ./Domain/ ./Domain/
COPY ./Application/ ./Application/
COPY ./Infra/ ./Infra/

# Publicar a aplicação Api em Release na pasta /app/publish
RUN dotnet publish ./ECommerceProdutos/ECommerceProdutos.csproj -c Release -o /app/publish

# Etapa 2: Criar a imagem final só com runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "ECommerceProdutos.dll"]