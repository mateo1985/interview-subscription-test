# interview-subscription-test
The interview task with subscriptions system.

![Schema image](https://github.com/mateo1985/interview-subscription-test/blob/master/app-schema.PNG "Schema")

## Prerequisites:
### If you run docker containers:
- Docker
- docker-compose

### If you are testing without docker:
- ms sql
- .net core 2.2
- angular cli
- node and npm

If you pull all files from git repository od not change EOL to windows type in *.sh scripts.

## System description:
There are four main parts of the application:
### REST API backend service:
  You can find it under: ./mail-subscriptions-api/MailSubscriptionsApi
  It uses asp.net core 2.2, EntityFramework Core. I used the code first approach so i could use migrations.
  When using IIS Express it starts on https://localhost:44322 on debug and connecting to MS SQL on localhost with datatbase Subscriptions.
  You can change these settings in appsettings.json and launchSettings.json
  Important thing for frontend are CORS. This is configured by environment variable CORS_ORIGIN (default set for http://localhost:4200)
  
### Frontend service:
  You can find it under: ./mail-subscriptions-ui/MailSubscriptionsApp
  I am using angular 7 with angular material, and for serving frontend asp.net core 2.2.
  The best approach is to run the `npm start` in ./mail-subscriptions-ui/MailSubscriptionsApp/ui-app. 
  The configuration for the frontend you can find at ./mail-subscriptions-ui/MailSubscriptionsApp/ui-app/src/environment (links and paths to backend)
  
### mail-subscriptions-db:
  You can find it under: ./mail-subscriptions-db
  database containers
  Container with ms sql.
  
### mailing service which is not exposed in code (limited only for 100 mails) and will be available only for 1 week
  
System is containerized with docker and docker-compose so it is easily deployable.
  

## Todo list:
- there are no unit tests for angular components because of lack of time - there are only unit tests for backend part
- the project is not secure because all connection strings are kept in plaintext. This should be fixed by storing this as environment variables and the best solution would be
  when it would be some secure store or encrypted data per user.
- on the backend side should be limitation in subscription per minute.
- all messages and ui visible for the end user should be translated and I18N should be implemented
- write more unit tests for front end application and backend api
- write rest api tests - for now there are no such tests
- add swagger documentation for rest api - this can be easily implemented by swashbuckle, but there are not descriptions prepared for controllers and models
- XML encryptor should be configured - otherwise we have a warning because of that
- all sensitive data should be kept in environment variables for user and the best would be when it these are encrypted per user
- full https should be implemented to communication
- logging for the system should be extended and maybe logging service should be implemented like elastic search
- in script.sql should be check if the db already exists


## How to run the project:

### Using docker-compose:
- using command line go to root project location
- type `docker-compose build` and wait some time
- type `docker-compose up` and wait about 3 minutes
- after that time go to http://localhost:10001 - this is the front-end part - try to subscribe something
- you can check if backend works going under http://localhost:10000/api/v1/topics

### Using local system:
- you should have ms sql server with integrated security enabled (for the out of the box configuration is for integrated security and localhost server)
- go to 'Package Manager Console' in Visual Studio in mail-subscriptions-api/MailSubscriptionsApi/MailSubscriptionsApi.sln and type `update-database`
- migration should be implemented and the Subscriptions database should be available right now
- run the project in mail-subscriptions-api/MailSubscriptionsApi/MailSubscriptionsApi.sln under debug configuration and using iis express
- backend is starting on https://localhost:44322/
- open folder mail-subscriptions-ui/MailSubscriptionsApp/MailSubscriptionsApp/ui-app in command line
- run `npm install`
- run project by typing `npm start`
- go to localhost:4200

## Tested on:
- Docker version 18.06.1-ce, build e68fc7a
- docker-compose version 1.22.0, build f46880fe
- windows 10 x64
- asp.net core 2.2
- Angular CLI: 7.2.1
- Node: 8.11.3
- npm 5.6.0





