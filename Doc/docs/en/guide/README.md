# Introduction

TerraMours is a service framework designed with Domain-Driven Design (DDD) principles. You can quickly deploy TerraMours using Docker to build your project management system and then integrate or develop your own business systems based on TerraMours.

The first project is TerraMoursGPT, which implements features such as user login, multi-language model chat based on SK, and multi-model image generation based on ChatGPT and SD. The administration panel includes features such as data dashboards, chat record management, image record management, user management, and system configuration.

## Project Components

* [TerraMours_Gpt_Api](https://github.com/TerraMours/TerraMours_Gpt_Api) - Backend API service developed using TerraMoursFrameWork. It is an intelligent assistant project that incorporates technologies like Net7+MinimalApi+EF Core+Postgresql+Seq+FluentApi.

* [TerraMours_Admin_Web](https://github.com/TerraMours/TerraMours_Admin_Web) - Backend management system developed using soybean-admin. It incorporates the latest frontend technologies such as Vue3, Vite3, TypeScript, NaiveUI, Pinia, and UnoCSS. It comes with rich theme configuration, high code standards, and can be used out of the box, as well as for learning and reference purposes.

* [TerraMours-Gpt-Web](https://github.com/TerraMours/TerraMours_Gpt_Web) - User interface for the ChatGPT project based on Vue3.0, TypeScript, Naive UI, and Vite.
## Developed Features
- **Data Dashboard**: Displays multiple data statistics that include several data types most important to management personnel. The charts present statistics on chat and drawing counts across multiple dimensions, divided into three dimensions: daily (segmented by hour), daily, and monthly. The following is the data content for daily statistics.
- **System Management**:
    - **Email Service Configuration**: Configures the API service parameters for system emails used for sending verification codes via email.
    - **GPT Settings**: Configures the proxy address, pricing plan, interface parameters, and KEY pool configuration for GPT.
    - **Image Service Address**: Configures the service address for AI image drawing.
- **Chat Records**: Manages chat records and queries user session information. (todo: 1. Export function, 2. Create fine-tuning model)
- **Sensitive Word Management**: Manages sensitive words and customizes word filtering to enhance system security.
- **Key Pool Management**: Manages the Key pool, allowing administrators to add multiple keys to form a Key pool for improved stability by using a round-robin approach when calling the AI interface.
- **System Prompt Words**: Provides system prompt words for various roles, enabling users to use AI conversation more effectively.
- **Drawing Records**: View the generation records of images in the system.
- **Menu Management**: Implements dynamic configuration of menus in the backend management system. The menu management interface allows setting menus, and the backend API includes basic menu options during initialization.
- **Role Management**: Controls roles in the backend management system. The default roles include Super Administrator and Regular User.
- **User Management**: Manages registered users in the system.
- **Product Management - Product Categories**: Sets the type and classification information for products to facilitate product management.
- **Product Management - Product List**: Sets product information.
- **Order List**: View generated orders.

## Quick Setup

### 1. Quick setup of AI chat and drawing system based on docker-compose

#### 1. Create a new empty file named docker-compose.yml

Create a new empty file named docker-compose.yml and paste the following contents into the file, then save it.

```yaml
version: "3.9"
services:
  redis:
    image: redis
    container_name: redis_container
    ports:
      - "6379:6379"
    restart: always
    networks:
      - server

  postgres:
    image: postgres
    container_name: postgres_container
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=terramours1024
      - POSTGRES_DB=TerraMoursGpt
    ports:
      - "5432:5432"
    restart: always
    networks:
      - server

  seq:
    image: datalust/seq
    container_name: seq_container
    environment:
      - ACCEPT_EULA=Y
    ports:
      - "5341:80"
    restart: always
    networks:
      - server

  server:
    image: raokun88/terramours_gpt_server:latest
    container_name: terramours_gpt_server
    environment:
      - TZ=Asia/Shanghai
      - ENV_DB_CONNECTION=Host=postgres;Port=5432;Userid=postgres;password=terramours1024;Database=TerraMoursGpt;
      - ENV_REDIS_HOST=redis:6379
      - ENV_SEQ_HOST=http://<YOUR-SERVER-IP>:5341/
    volumes:
      # 图片挂载地址，将容器中的图片挂载出来
      - /path/terra/images:/app/images
      # 可挂载自定义的配置文件快速进行系统配置
      #- F:\Docker\terra\server/appsettings.json:/app/appsettings.json
    ports:
      - "3116:80"
    restart: always
    networks:
      - server
    depends_on:
      - postgres
      - redis
  admin:
    image: raokun88/terramours_gpt_admin:latest
    container_name: terramoursgptadmin
    environment:
      - VUE_APP_API_BASE_URL=http://<YOUR-SERVER-IP>:3116
    ports:
      - "3226:8081"
    restart: always
    networks:
      - server

  web:
    image: raokun88/terramours_gpt_web:latest
    container_name: terramoursgptweb
    environment:
      - VUE_APP_API_BASE_URL=http://<YOUR-SERVER-IP>:3116
    ports:
      - "3216:8081"
    restart: always
    networks:
      - server

networks:
  server:
    driver:
      bridge

```

:::warning Installation Notes

1. Edit the yml file: Replace `<YOUR-SERVER-IP>` with your server's IP address.<br/>
2. Default admin account credentials: terramours@163.com terramours@163.com<br/>
3. If there are system errors, check the logs using seq. The log URL is: `http://<YOUR-SERVER-IP>:5341/`<br/>
4. If the seq log displays "Database initialized successfully," it means the backend service has been initialized. There may be error messages during the initial installation. It is recommended to restart the terramours_gpt_server container after completing the docker-compose installation.<br/>
5. For further service configuration, you can copy the appsettings.json file from the server repository and modify the file by mounting it in the container.<br/>
```
# Mount custom configuration file for quick system configuration
- /path/terra/appsettings.json:/app/appsettings.json
```

:::

#### 2. Upload the docker-compose file to the server

Upload the docker-compose file to the server using XFTP, the FTP client I am using.

#### 3. Execute Docker command to build the docker-compose

```shell
docker-compose up
```