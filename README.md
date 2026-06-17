## SETUP:

First make sure you have the .NET SDK (version 10)

- clone the repo:

`$ git clone https://github.com/Sfn15/Acksession-API-project.git`

- go to the correct directory:

`$ cd FinalProject`

- install the required dependencies:

`$ dotnet restore`

 - run the app:

`$ dotnet run`




## Info:

The app runs on http://localhost:5159 by default, you can change the port in `Properties/launchSettings.json`


For swagger documentation, go to http://localhost:5159/swagger


### Auth flow 
1. Register or Login to get your JWT (the admin user's credentials are hardcoded, you need to create users yourself)


Admin credentials are:

- Email: admin@test.com
- Username: admin

2. add the token in the auth section of your postman requests as a bearer token, you will require it for all non-login or register endpoints

### Roles:
- Admins have access to everything
-  Users can't modify categories, and can only view/modify their own tasks.
