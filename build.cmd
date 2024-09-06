@echo off
echo Starting .NET build process...
dotnet build
if %ERRORLEVEL% == 0 (
  echo Build succeeded.
) else (
  echo Build failed.
)
