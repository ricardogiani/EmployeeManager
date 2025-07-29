## gerar a imagem back-end
* na raiz do projeto: docker build -t employee-manager:latest .

## Subir docker compose
* docker-compose up -d

## Rodar os Scripts dentro da pata DataBase
1.employee.sql
2.begin-data.sql

## Subir back-end local
* pasta "EmployeeManager.Application>" dotnet run

## Subir front-end
* pasta "EmployeeManager.Front>" npm start

## Login: admin@company.com, password: Senha123*




## opcional: subindo um container
docker run -p <porta_do_host>:<porta_do_container> --name <nome_do_container> <ID_da_imagem>
docker run -p 5148:5148 --name employee-container employee-manager