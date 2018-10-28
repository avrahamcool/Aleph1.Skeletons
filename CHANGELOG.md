# 2.1.0:
using ActivityID as default CorrelationID (shared between all function in the same request lifcycle)

removed extra folders: fixes https://github.com/avrahamcool/Aleph1.Skeletons/issues/6

defaults to https: fixes https://github.com/avrahamcool/Aleph1.Skeletons/issues/5

removed webdav module - enabling delete and put: fixes https://github.com/avrahamcool/Aleph1.Skeletons/issues/4

# 2.0.3:
fixed metadata for extention

# 2.0.2:
WebAPI app secret moved to code - and is now a randomly generated GUID

# 2.0.1:
updated to new logger

# 2.0.0:
added layer template + moved to new repo

# 1.2.2:
publish files are correctly loaded into csproj + updated deps

# 1.2.1:
taking X-Forward-For into considuration for throttling

# 1.2.0
WebAPI security headers Environment (Dev/Test/Prod) configuration + Publish Fixed Logger issues with new-line in linux style
