.PHONY: build format clean

build:
	dotnet build SessionCSharp.sln --configuration Debug --force --no-incremental --verbosity normal
	dotnet build SessionCSharp.sln --configuration Release --force --no-incremental --verbosity normal

format:
	dotnet format SessionCSharp.sln --severity info

clean:
	dotnet clean SessionCSharp.sln --configuration Debug --verbosity normal
	dotnet clean SessionCSharp.sln --configuration Release --verbosity normal
	git clean -Xi
	find . -type d -empty -delete
