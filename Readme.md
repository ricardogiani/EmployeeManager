## Requisitos:
* Node > 20
* Angular 20
* Net 8
* Imagem postgres
* Docker instalado

## gerar a imagem back-end
* na raiz do projeto: docker build -t employee-manager:latest .

## Subir docker compose
* docker-compose up -d

## Rodar os Scripts dentro da pata DataBase
1.employee.sql
2.begin-data.sql

## Subir front-end
* pasta "EmployeeManager.Front>" npm start

## Login
* admin@company.com, password: Senha123*

## Rodar testes unitários back-end
* pasta "EmployeeManager.Test" dotnet test

## Subir back-end local (opcional)
* pasta "EmployeeManager.Application" dotnet run