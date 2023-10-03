# SolarWatch Project

## Table of Contents
- [About](#about)
- [Features](#features)
- [Installation](#installation)

## About

The SolarWatch Project is a sophisticated application designed to provide accurate information to the user about the sunset and sunrise of the day in a given city.
Currently this is just an API, that doesn't have a Frontend. It was a Codecool Project during the Advanced Module. But It was a great practice for **Exception Handling**, **User Authentication/Authorization** and **SQL Server Database Handling**.

## Features

- **1:** User Registration Handling
- **2:** Get Sunset & Sunrise For The Given City
- **3:** Save & Edit Info For The Desired City

## Installation

```shell
$ git clone https://github.com/SSzabolcs-glitch/solarwatch-project.git
$ cd solarwatch-project
$ docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server
$ dotnet tool install --global dotnet-ef
$ dotnet add package Microsoft.EntityFrameworkCore.Design
$ dotnet ef migrations add InitialCreate
$ dotnet ef database update
