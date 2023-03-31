# EventService

Сервис для работы с мероприятиями. Сделан в качестве тестового задания.

Для запуска сервиса необходимо:
1) Запустить терминал из корневой директории репозитория
2) Выполнить команды:
>`cd ImageService && docker build -t imageservice .`

>`cd .. && cd SpaceService && docker build -t spaceservice .`

>`cd .. && cd PaymentService && docker build -t paymentservice .`

>`cd .. && cd EventService && docker build -t eventservice -f EventService/Dockerfile .`

>`cd EventService && docker compose up --detach && start http://localhost:5010/index.html`  

 **для загрузки страницы может потребоваться время*

3) Для получения токена необходима отправить запрос POST по адресу 
>`http://localhost:5000/connect/token`
 -   header:
        -   Authorization: Bearer Token
 -   body (x-www-form-urlencoded) :
        -   grant_type = “password” 
        -   username = “dev”
        -   password = “hardtoguess”            
        -   scope = “myapi.access openid”            
        -   client_id = “spaWeb”            
        -   client_secret = “hardtoguess”

4) В интерфейс Swagger нужно передать полученный токен в виде
>`Bearer <token>` 
