# WebApi

Для работы программы нужен Docker
1. Запустить Docker
2. Скачать API (или клонировать ветвь)
3. В папке API запустить консоль и вписать следующее: <br>
```Batchfile
    docker compose build
    docker compose up
```
<br>
API запуститься на порте 5021, так что он должен быть открыт и свободен. 
Для проверки работоспособности можно воспользоваться swagger (http://localhost:5021/swagger/index.html) или выполнить запросы на этот порт. <br>
Запросы:<br>
get     http://localhost:5021/api/Order/{id}  - получение записи по id <br>
delete  http://localhost:5021/api/Order/{id}  - удаление записи по id <br>
post    http://localhost:5021/api/Order       - создание записи (json формат данных для записи) <br>
put     http://localhost:5021/api/Order       - изменяет запись (json формат данных для записи) <br>
