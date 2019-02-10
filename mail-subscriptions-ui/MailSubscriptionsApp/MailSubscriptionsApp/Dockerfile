FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["./MailSubscriptionsApp.csproj", "MailSubscriptionsApp/"]
RUN dotnet restore "MailSubscriptionsApp/MailSubscriptionsApp.csproj"
COPY . "MailSubscriptionsApp/"
WORKDIR "/src/MailSubscriptionsApp"
RUN dotnet build "MailSubscriptionsApp.csproj" -c Release -o /app

FROM build AS publish
RUN apt-get update  
RUN apt-get -f install  
RUN apt-get install -y wget  
RUN wget -qO- https://deb.nodesource.com/setup_10.x | bash -  
RUN apt-get install -y build-essential nodejs
RUN npm install -g @angular/cli
RUN npm rebuild node-sass --force
WORKDIR ui-app
RUN npm install
WORKDIR ../
RUN dotnet publish "MailSubscriptionsApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MailSubscriptionsApp.dll"]