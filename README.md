# Library API

## Описание проекта

**Library API** — это API для управления библиотечным каталогом. Проект предоставляет возможности регистрации пользователей, бронирования книг, а также управления данными о книгах и бронированиях.

## Основные функции

- Регистрация и авторизация пользователей.
- Роли пользователей: “Администратор”, “Библиотекарь” и “Клиент”.
- Бронирование и снятие брони с книг.
- Управление каталогом книг.

## Стек технологий

- **ASP.NET Core 8.0** — разработка API.
- **Entity Framework Core** — взаимодействие с базой данных.
- **PostgreSQL** — реляционная база данных.
- **JWT (JSON Web Tokens)** — для аутентификации и авторизации.
- **Swagger** — документация API.

## Установка и запуск проекта

### 1. Клонирование репозитория

Склонируйте репозиторий на локальную машину:
```bash
git clone <URL репозитория>
cd <название директории>
```

### 2. Настройка базы данных

Создайте базу данных в PostgreSQL и настройте подключение в `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=library_db"
}
```

### 3. Установка зависимостей

Установите зависимости проекта через .NET CLI:
```bash
dotnet restore
```

### 4. Миграции базы данных

Примените миграции для создания структуры базы данных:
```bash
dotnet ef database update
```

### 5. Запуск приложения

Запустите проект:
```bash
dotnet run
```
Приложение будет доступно по адресу: `http://localhost:5199`.

## Конфигурация JWT

Для настройки JWT откройте `appsettings.json` и укажите следующие параметры:
```json
"Jwt": {
  "Key": "My_Very_Secret_Key_12345678901234561111",
  "Issuer": "your_issuer_here",
  "Audience": "your_audience_here"
}
```

## Использование API

После запуска приложения вы можете использовать Swagger для тестирования API. Swagger доступен по адресу:
```
http://localhost:5199/swagger
```

### Примеры запросов

#### 1. Авторизация
**POST** `/api/auth/login`
```json
{
  "username": "1",
  "password": "StrongP@ssw0rd!"
}
```

#### 2. Добавлеие книги
**POST** `/api/reservations/reserve](http://localhost:5199/api/Books/add`
```json
{
  "id": 11,
  "title": "Заголовок",
  "author": "Автор",
  "genre": "Жанр",
  "isReserved": false,
  "isIssued": false,
  "isReturnBook": true
}
```

#### 3. Удаление книги
**POST** `/api/Books/delete`
```json
{
  "id": 11,
  "title": "Заголовок",
  "author": "Автор",
  "genre": "Жанр",
  "isReserved": false,
  "isIssued": false,
  "isReturnBook": true
}
```

## Роли пользователей

- **Администратор**: управляет пользователями и ролями.
- **Библиотекарь**: управляет книгами и бронированием.
- **Клиент**: бронирует и снимает бронь с книг.


