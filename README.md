# Aleph1.Skeletons
This extension will add a new WebAPI Project Template Skeleton type for WebAPI projects.
and a new layer trio projects template.

### Project Template Features
* N-Tier project using DI (includes DAL-BL-API with moqs).
* WebAPI Auth using Tokens, with custom security project (includes moq).
* WebAPI Throttling on all controllers.
* Enables and configure Swagger automatically (with Documentation).
* Auto Logging (function tracing) using PostSharp and NLOG (configuration set to local file).
* Friendly exception handling on the webapi controllers.
* ModelValidation on the webapi controllers (hebrew locale by default).
* Build & Config & Publish for 3 Environment (Dev - Test - Prod)
* Security measurments (removing extra server headers & adding security headers)
* Models are exported as Nuget packages

# Prerequisites
* [Visual Studio](https://www.visualstudio.com/) 2017
* [VS Extensibility Tools](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ExtensibilityTools) installed.
* [Sidewaffle Creator (2017)](https://marketplace.visualstudio.com/items?itemName=Sayed-Ibrahim-Hashimi.SidewaffleCreator2017) installed.

# Installation
* Clone the project
* Run the Package project to create the .vsix
* Installable .vsix in [Visual Studio](https://www.visualstudio.com/) 2017.
