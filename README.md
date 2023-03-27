# EventService

Сервис для работы с мероприятиями. Сделан в качестве тестового задания.

Для запуска сервиса необходимо:
1) Запустить терминал из корневой директории репозитория
2) Выполнить команду 

> cd EventService && docker build -t eventservice -f EventService/Dockerfile . && docker compose -f EventService/docker-compose.yaml up --detach && start http://localhost:5010

*для загрузки страницы может потребоваться время
2) Выполнить команды:
cd cd ImageService && docker build -t imageservice .

cd .. && cd SpaceService && docker build -t spaceservice .

cd .. && cd PaymentService && docker build -t paymentservice .

cd .. && docker build -t eventservice -f EventService/Dockerfile .

cd EventService && docker compose up
3) Для получения токена необходима отправить запрос POST по адресу http://localhost/connect/introspect
Authorization: Basic xxxyyy
token=<Token>