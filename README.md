# acs560_course_project
[![Go Report Card](https://goreportcard.com/badge/github.com/nguytk01/acs560_course_project)](https://goreportcard.com/report/github.com/nguytk01/acs560_course_project)
[![Build Status](https://travis-ci.org/nguytk01/acs560_course_project.svg?branch=master)](https://travis-ci.org/nguytk01/acs560_course_project)
[![Build status](https://ci.appveyor.com/api/projects/status/tm1nnakxbnw6eqad/branch/master?svg=true)](https://ci.appveyor.com/project/nguytk01/acs560-course-project/branch/master)

Personal Time Tracker

## Instructions for developers
### Requirements: 
- PostgreSQL installed on your local computer. PostgreSQL's bin directory is accessible in your PATH
- Go installed and accessible in your PATH.

### Optional:
- NUnit v3 installed, and nunit3-console.exe accessible in your PATH. (If you want to run tests on C# project)

### Command-line Building Instructions (Windows)
- If you have more than one python versions on your local computer, there is a chance that you can use `py` instead of `python` on command line.
- Clone this repository on your local computer. Currently, it should be in your GOPATH/src/. On a standard Windows Go installation, it should be in `%USERPROFILE%\go\src\`. For example, on my computer, the repository should be in `%USERPROFILE%\go\src\acs560_course_project`. Under this path, you should be able to see find .gitignore file, client folder, server folder and such if you clone correctly.
- Make sure you have python in your path
- If you have python 3, please run `pip install future`. 
- Add the bin directory of PostgreSQL to your PATH. The build script needs to access psql and createdb executables.
- Run `python run.py builddb` to initialize your database. You must set PGPASSWORD and PGUSER environment variable to the corresponding user and password on your local PostgreSQL server.
- Run `python run.py buildserver` to build the server executable. The server executable stays in the server directory.
- Start the server on the command line in the project directory as follows:
- `server.exe -database postgres -dbuser <POSTGRES_USER> -dbpassword <POSTGRES_USER_PASSWORD> -dbname sandbox -ipaddr 127.0.0.1 -port 5432`

- Open the client Visual Studio solution with Visual Studio, build and run.

- Sandbox.sql file may contain a sample credential you can use to log in. You may want to use it to see how the prototype looks like. This sample credential has some data.

### Testing Instructions
- Run `python run.py testserver` in project directory to test the server.
- Run `python run.py testclient` in project directory to test the client.

### Instructions to run NUnit tests in Visual Studio manually
- You need to make sure Nuget has installed `NUnit`, `NUnit3TestAdapter`, `NUnit.Extension.NUnitProjectLoader`, `NUnit.Extension.VSProjectLoader`
- Run the server before you run the client tests.
