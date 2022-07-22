FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ./ ./

RUN dotnet build
RUN dotnet publish Barracuda.Janus.Core.BlobProxy.Server/Barracuda.Janus.Core.BlobProxy.Server.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 5000
ENV BLOB_ACCOUNT=cudacodingexercise
ENV BLOB_CONTAINER=files
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Barracuda.Janus.Core.BlobProxy.Server.dll"]