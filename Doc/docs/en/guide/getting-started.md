# TerraMoursApi-V0.1 Deployment

## 1. Create the Server Project Folder

Create a project folder to manage the backend interface code:

```
/data/terramours/api
```

Assign permissions to the folder:

```shell
cd /data
```

```shell
chmod -R 777 terramours
```

## 2. Publish the Project

Right-click on the project node, select Publish, and choose to publish to a folder:

![image-20230524153537256](https://www.raokun.top/upload/2023/05/image-20230524153537256.png)

Select the appropriate runtime environment, and choose the Linux-64 server to publish:

![image-20230524153736263](https://www.raokun.top/upload/2023/05/image-20230524153736263.png)

Publish the Dockerfile (or create it in the `publish` folder on the server):

![image-20230524155959756](https://www.raokun.top/upload/2023/05/image-20230524155959756.png)

Dockerfile content:

```
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM base AS final
WORKDIR /app
COPY .  /app
ENTRYPOINT [\u0022dotnet\u0022, \u0022TerraMours.dll\u0022]
```

## 3. Package and Compress the Code

After successful publication, click \u0022Open Folder,\u0022 select the parent directory, package the entire `publish` folder, and compress it into a zip file for uploading to the server.

Zip format:

```
publish{“date time”}.zip
```

![image-20230524154015606](https://www.raokun.top/upload/2023/05/image-20230524154015606.png)

## 4. Upload to the Server

The project path is:

```
/data/terramours/api
```

Upload the compressed file to the designated folder on the server. XFTP is recommended for visual operation.

![image-20230524154252126](https://www.raokun.top/upload/2023/05/image-20230524154252126.png)

## 5. Unzip the Files

Run the unzip command in the project folder:

```shell
cd /data/terramours/api
```

```shell
unzip publish0521.zip -- change the zip file name. If replacing, enter \u0027A\u0027 to overwrite all
```

## 6. Create a New Script File: `terramours.sh`

Create a new script file for easy command execution:

Filename: `terramours.sh`

Content:

```
#!/bin/bash
cd publish
echo \u0022Stopping docker container...\u0022
docker stop terramours
echo \u0022Removing docker container...\u0022
docker rm terramours
echo \u0022Removing docker image...\u0022
docker rmi terramours
echo \u0022Building docker image...\u0022
docker build -t terramours .
echo \u0022Running docker container...\u0022
docker run --name terramours -p 4112:80 -d terramours

```

## 7. Run the Script: `terramours.sh`

Execute the script:

```shell
sh terramours.sh
```

The backend project has been deployed to Docker, and the project port is 4112.

## 8. View Portainer\u0027s Container Management:

Check the containers, and the deployment is successful:

![image-20230524155253942](https://www.raokun.top/upload/2023/05/image-20230524155253942.png)
