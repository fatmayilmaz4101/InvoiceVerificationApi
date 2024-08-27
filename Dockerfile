# 1. Aşama: Build Aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Çalışma dizinini oluştur ve belirle
WORKDIR /source

# Proje dosyalarını kopyala ve bağımlılıkları geri yükle
COPY *.sln .
COPY src/ProjectName/*.csproj ./src/ProjectName/
RUN dotnet restore

# Tüm proje dosyalarını kopyala
COPY . .

# Uygulamayı yayınlamak (publish) için build et
RUN dotnet publish -c Release -o /app --no-restore

# 2. Aşama: Çalışma Aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Çalışma dizinini belirle
WORKDIR /app

# Yayınlanan dosyaları kopyala
COPY --from=build /app .

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "ProjectName.dll"]
