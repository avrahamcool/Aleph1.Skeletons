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
