# Project2_RickandMorty_Group1

## Project Overview
### Title: Rick and Morty CMS
### Layers: Client-Servers
### Architecture: RESTful
### Platform: .NET
### Framework: ASP.NET Core (Web API)

#### Client
The frontend uses HTML, CSS, and JavaScript. And front-end testing is done with Jest. The user will be able to login. And once authenticated, an authorized user can add, view, edit, and delete a character in the user’s character list. An authorized user can also add, view, edit, and delete an episode in the user’s episode list.

To add a character, the Rick and Morty API will be used to provide the options and attributes for a Rick and Morty character. The user picks a character to add to the user’s list with notes from the user. The character is then persisted to the remote SQL db on the server. After a user adds a character to the list, the user can then manage the characters on the list by performing the following CRUD: view, edit, and delete from the list of characters.

Viewing the character will display an image of the character along with some attributes or properties of the character. Editing the character will involve editing the notes about the character.

Viewing an episode from the episode list will display an image related to the episode along with some attributes or properties of the episode. Editing the episode will involve editing the notes about the episode.

#### Server
The backend uses ASP.NET and talks to an SQL Db server to persist data retrieved from the external API to the db tables. The frontend will make a call to the server with the selected character to be persisted to the user’s character list.

The backend technologies encompass:

    C# (Backend programming language)
    EF Core (ORM)
    SQL Server (Azure hosted)
    ASP.NET Core (Web API Framework)
    xUnit/Moq (Backend Testing)
    Azure App Service (for application hosting)



#### MVP Features (Requirements)
    Application must build and run.
    xUnit Testing (70% branch coverage for Services + Utilities and Models layer) for backend.
    Utilize an external API.
    Allows for multiple users to authenticate and store their data.
    HTML/CSS/JS or React front-end.
    Front end unit testing (20% coverage.)
    Hosted on Github in our training Org in it's own repository.


#### ERD

#### External API
Rick and Morty API url link: https://rickandmortyapi.com/

#### Stretch Goals
    JWT or IDaaS for authentication (such as ASP.NET Core Identity, or Auth0.)
    Backend hosted on Azure Cloud Services.
    React front end.
    React testing.
