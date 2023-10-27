# Leek Log

This project is intended to log your journey from being a human form of leek to being a muscular behemoth.

## Starting the app

If you just want to start the app, follow these steps:

1. Download the complete git repository
2. Execute the `docker-compose.yml` like this: `docker-compose up -d`
3. Enjoy!

### Debugging

To run the app locally *(for example in Visual Studio)*, you need a MySql database.
The easiest way to achieve this, is creating a MySql server in docker.
You can use the following command:

``` sql
docker run --name leeklog-mysql -e MYSQL_ROOT_PASSWORD=TestPassword123! -e MYSQL_DATABASE=leeklog -v mysql-data:/var/lib/mysql -p 3306:3306 -d mysql:latest
```

If you want to change the root password, feel free to do so, but it's running only on your local computer, so why bother with that?
