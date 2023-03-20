# EventService

Сервис для работы с мероприятиями. Сделан в качестве тестового задания.

Для запуска сервиса необходимо:
1) Запустить терминал из корневой директории репозитория
2) Выполнить команду 

> cd EventService && docker build -t eventservice -f EventService/Dockerfile . && docker compose -f EventService/docker-compose.yaml up --detach && start http://localhost:5010
