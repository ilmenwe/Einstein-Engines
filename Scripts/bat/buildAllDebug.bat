@echo off
cd ../../

call git submodule update --init --recursive
call dotnet build /nowarn:RS1038 -c Debug

pause
