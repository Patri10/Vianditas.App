FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el archivo del proyecto y restaurar dependencias
COPY ["Vianditas.API.csproj", "./"]
RUN dotnet restore "Vianditas.API.csproj"

# Copiar el resto del código y compilar
COPY . .
RUN dotnet publish "Vianditas.API.csproj" -c Release -o /app/publish

# Generar la imagen final para producción
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto configurado por Railway
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Vianditas.API.dll"]
