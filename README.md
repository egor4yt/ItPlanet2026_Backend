# О проекте

Бекенд часть проекта для учатстия в олимпиапде ИТ-Планета 2026 в номинации "прикладное программирование"

# Запуск приложения

Для запуска приложения необходимо выполнить следующие шаги:
1. Установить Docker
2. Клонировать репозиторий с помощью команды `git clone <repository_url>`
3. В директории `/src/Launchpad` выполнить команду `docker-compose up -d --build --force-recreate` для запуска приложения и всех зависимостей
4. Приложение будет доступно по адресу `http://localhost:3333/swagger`

# Структура репозитория

- .github - описание процессов, которые запускаются на стороне github(continue integrations)
- docs - документация проекта
- infrastructure - вспомогательные инструменты для развертывания и мониторинга приложения
- src - исходный код

# Стек

Технологии, которые были использованы в процессе разработки

## Код
- **.NET 10.0** - основной фреймворк
- **MediatR** - реализация паттерна CQRS для разделения команд и запросов
- **FluentValidation** - валидация моделей через Fluent-интерфейс
- **NetTopologySuite** - поддержка работы с геоданными и пространственной геометрией
- **Serilog** - структурированное логирование
- **Swagger** - генерация документации OpenAPI и интерактивный UI
- **xUnit** - тестирование
- **AutoFixture** - генератор моковых данных
- **Testcontainers** - запуск временного сервисов в Docker для полноценных тестов
- **Respawn** - быстрая очистка состояния базы данных между прогонами тестов
- **Microsoft.AspNetCore.Mvc.Testing** - инструменты для создания интеграционных тестов Web API
- **coverlet.collector** - сбор и анализ данных о покрытии кода тестами

## Работа с данными
- PostgreSQL (OLTP база данных) <details><summary>Схема базы данных</summary><img alt="Infrastructure" src="docs%2FImages%2Fdatabase-scheme.svg" title="Database schemme"/></details>

## Интеграции
- React фронтенд приложение - [GitHub](https://github.com/officer04/launchpad-front/tree/stage-2)

# Контакты
Со мной можно связаться при помощи:
- Telegram: [@egor4yt](https://t.me/egor4yt)
- Gmail: [egor4yt@gmail.com](mailto:egor4yt@gmail.com)
