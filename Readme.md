# This is a Coding Challenge Project for NTI. 

## Technologies Used
[![License: GNU](https://img.shields.io/badge/License-GNU%20GPL-blue)](https://www.gnu.org/licenses/gpl-3.0) ![](https://img.shields.io/badge/.NET_Core-blue?logo=.net) ![](https://img.shields.io/badge/Entity_Framework_Core-purple?logo=.net) ![](https://img.shields.io/badge/xUnit-orange?logo=xunit) ![](https://img.shields.io/badge/-ReactJs-61DAFB?logo=react) ![](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white) ![](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)
- .NET 8 - A free, cross-platform, open-source framework for building modern cloud-based and internet-connected applications.
- Entity Framework Core - A modern object-database mapper for .NET that provides a clean and elegant way to access relational databases.
- xUnit - A free, open-source, and community-focused unit testing tool for the .NET Framework.
- Moq - Moq is a mocking library for C#. It enables developers to create mock objects (objects that simulate real objects) with predefined behaviors, allowing them to isolate and test specific components of their code independently.
- AutoFixture - AutoFixture makes it easier for developers to do Test-Driven Development by automating non-relevant Test Fixture Setup, allowing the Test Developer to focus on the essentials of each test case.
- PostgreSQL - A Relational Database (used to store data).
- SQLite - A Relational Database (used for unit test in this case)


# Getting Started

## Prerequisites
- Docker Installed, if you don't have docker you can install it from [here!](https://docs.docker.com/get-docker/)

## Installation

1. Clone the Repo:
```bash
git clone https://github.com/roniel06/ntiproject.git
```
2. Through the terminal navigate to the project
  ```bash
cd ntiproject
```
3. Run the following command to setup the Docker Containers:
```bash
docker-compose up
   ```
4. Wait until the container is mounted:

## Usage:
1. Once the containers are up and running you can navigate the SPA project going through
   [http:localhost:3000](http:localhost:3000)
2. If you want to access the Swagger Tool for the Web Api:
   [http:localhost:8081/swagger](http:localhost:8081/swagger)
 
### User Story:
As an employee I want to be able to create, delete, update, and get Items, the items must have
 - Id
 - Item Number
 - Description
 - Default Price
 - IsActive (status)
As an Employee I want to be able to create, delete, update, and get Customers, the customer must have:
 - Id
 - Name
 - Last Name
 - Phone
 - Email
 - IsActive (status)

 As an employee I want to be able to assign items to a created customer, I should be able to select the item, put a new price if necessary, and the quantity. 

 As An employee I want to have a report module in wich I can have the following reports:
  - Get Items and their associated customers from an item number range i.e.: itemNumber = 1 to itemNumber = 200.
  - Get the created customers and their most expensive items.


