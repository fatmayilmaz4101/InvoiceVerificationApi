FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Çalışma dizinini ayarla
WORKDIR /source

# Tüm dosyaları kopyala
COPY . .

# Restore işlemi
RUN dotnet restore "InvoiceVerificationApi/InvoiceVerificationApi.csproj"

# Build ve publish işlemi
RUN dotnet publish "InvoiceVerificationApi/InvoiceVerificationApi.csproj" -c Release -o /app --no-restore

# Runtime imajı oluştur
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Çalışma dizinini ayarla
WORKDIR /app

# Yayınlanan dosyaları kopyala
COPY --from=build /app .

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "InvoiceVerificationApi.dll"]
