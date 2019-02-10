FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["./MailSubscriptionsApi.csproj", "MailSubscriptionsApi/"]
RUN dotnet restore "MailSubscriptionsApi/MailSubscriptionsApi.csproj"
COPY . "MailSubscriptionsApi/"
WORKDIR "/src/MailSubscriptionsApi"
RUN dotnet build "MailSubscriptionsApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MailSubscriptionsApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MailSubscriptionsApi.dll"]