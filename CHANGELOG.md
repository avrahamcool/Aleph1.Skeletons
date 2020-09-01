# 3.0.4
* [WebAPI] changed default DAL connection from SqlExpress to LocalDB
* [All] Update nugets + fix FluentValidation restriction

# 3.0.3
* [WebAPI] add camelCase contract resolver closes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/144
* [Layer] re-add the default Layer files closes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/145
* update nuget packages

# 3.0.2
* removed Authenticated attribute from About


# 3.0.1
* added static code analyzers to all projects (FxCop analyzers) - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/21
* fixed security flow regarding AllowAnonymous


# 3.0.0
*  **BREAKING CHANGES** all project are created with .net 4.8 (instead of 4.7.1)
*  **BREAKING CHANGES** improved ticket handling (using http only cookie instead of headers)
*  **BREAKING CHANGES** changed Moq to Mock [finally :)]
* all project use package reference instead of package.config (except the one that can't)
* improved many security aspects
* improved login flow
* improved injecting of currentUser
* included entity framework in DAL - with full demo . (enabling tracking of entities)
* improved proxy error handling
* fixed typos
* improved validators - fixes : https://github.com/avrahamcool/Aleph1.Skeletons/issues/22
* remove server data in PROD env - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/23
* added missing CORS - fixes : https://github.com/avrahamcool/Aleph1.Skeletons/issues/24
* added .tfignore and .gitignore - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/25
* better handle backups - fixes : https://github.com/avrahamcool/Aleph1.Skeletons/issues/41
* block TS execution in WebAPI project - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/54
* better handling of documentation creation - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/75

# 2.6.0
* enabling CORS for DEV - closes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/8
* no longer backup Logs - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/19
* better injection of security service - closes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/20
* added proxy template

# 2.5.0
* compatible with VisualStudio 2019

# 2.4.1
* removed proxy project from `template.json` - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/18

# 2.4.0
* Updated all underlying Nugets
* Separated AboutModel into it's own file - closes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/16

# 2.3.0
* The convention in names is automatically applied when creating a layer - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/9
* Layer is now created in the selected folder - fixes: https://github.com/avrahamcool/Aleph1.Skeletons/issues/10

# 2.2.0
* Backup before publish - https://github.com/avrahamcool/Aleph1.Skeletons/issues/11
* Moved all setting from user files to csproj - https://github.com/avrahamcool/Aleph1.Skeletons/issues/12

# 2.1.1:
* CorrelationID is now fixed even after async/await: fixes https://github.com/avrahamcool/Aleph1.Skeletons/issues/7

# 2.1.0:
* using ActivityID as default CorrelationID (shared between all function in the same request life cycle)
* removed extra folders: fixes https://github.com/avrahamcool/Aleph1.Skeletons/issues/6
* defaults to https: fixes https://github.com/avrahamcool/Aleph1.Skeletons/issues/5
* removed WebDav module - enabling delete and put: fixes https://github.com/avrahamcool/Aleph1.Skeletons/issues/4

# 2.0.3:
* fixed metadata for extension

# 2.0.2:
* WebAPI app secret moved to code - and is now a randomly generated GUID

# 2.0.1:
* updated to new logger

# 2.0.0:
* added layer template + moved to new REPO

# 1.2.2:
* publish files are correctly loaded into csproj + updated dependencies

# 1.2.1:
* taking X-Forward-For into consideration for throttling

# 1.2.0
* WebAPI security headers Environment (Dev/Test/Prod) configuration + Publish Fixed Logger issues with new-line in Linux style
