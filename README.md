<br />
<div align="center">
  <h3 align="center">TestTaskAGSR</h3>

  <p align="center">
    Тестовое задание для "Агентство сервисизации и реинжиниринга"
    <br />
  </p>
</div>



<details>
  <summary>Оглавление</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#postman">Postman</a></li>
  </ol>
</details>



## About The Project

API приложение - Создание, чтение, изменение, удаление, а также поиск по фильтру [Hl7.Fhir.Search#DateTime](https://www.hl7.org/fhir/search.html#date) данных о пациентах

## Getting Started

Описание шагов для запуска программы 

### Prerequisites

Действия для установки `docker compose` на Ubuntu
* Требуется для установки и выполнения последующих шагов
  ```sh
  sudo apt-get install curl
  sudo apt-get install gnupg
  sudo apt-get install ca-certificates
  sudo apt-get install lsb-release
  ```
* Загрузка файла `gpg` для установки `docker`
  ```sh
  sudo mkdir -p /etc/apt/keyrings
  curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
  ```
* Добавление пакетов `docker` и `docker-compose` в Ubuntu
  ```
  echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-pluginsudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-pluginlinux/ubuntu   $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
  ```
* Установка `docker` и `docker-compose`
  ```
  sudo apt-get update
  sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin
  ```
* Проверка успешности установки `docker`
  ```
  sudo docker run hello-world
  ```

### Installation

* Клонирование репозитория
   ```sh
   git clone https://github.com/code-lime/TestTaskAGSR
   ```
* Настройка параметров запуска
   ```sh
   cp .env.sample .env
   nano .env
   ```

   > Параметры запуска:
   > ```cs
   > LOGGING_LEVEL=... //Уровни логирования (Verbose, Debug, Information, Warning, Error, Fatal)
   >
   > MARIADB_DATABASE=... //Название базы данных
   > MARIADB_USERNAME=... //Пользователь базы данных
   > MARIADB_PASSWORD=... //Пароль от пользователя базы данных
   > MARIADB_PORT=... //Порт базы данных
   >
   > API_PORT_HTTP=... //Порт http API
   > ```
* Запуск `docker compose`
   ```sh
   docker compose up
   ```

## Usage

После запуска `docker compose` создается папка `./etc`.

В папке `./etc/db` находятся файлы `MariaDB` базы данных в которой происходит сохранение всех данных.

Для добавления 100 сгенерированных сущностей через `API` требуется запустить проект `TaskAGSR.Console` и в окне программы ввести адрес до запущенного API приложения, либо нажать `ENTER` для использования адреса по умолчанию

## Postman

Postman - коллекция для демонстрации работы API приложения: [Postman - коллекция](https://github.com/code-lime/TestTaskAGSR/blob/master/TaskAGSR.postman_collection.json)