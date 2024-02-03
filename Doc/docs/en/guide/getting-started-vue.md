# TerraMoursVue-V0.1 Deployment
## 1.Create the Server Project Folder
Demo site:[http://43.134.164.127:8089/](http://43.134.164.127:8089/)

Create a project folder to manage the backend interface code:

```
/data/terramours/client/terramoursvue
```

Assign permissions to the folder:

```shell
cd /data
```

```shell
chmod -R 777 terramours
```

## 2.Build the Project

Run the build command:

```shell
pnpm build
```



![image-20230524155502411](https://www.raokun.top/upload/2023/05/image-20230524155502411.png)

The built files are located in the dist folder.

## 3.Package and Compress the Code

After successful publication, click "Open Folder," select the parent directory, package the entire publish folder, and compress it into a zip file for uploading to the server.

Zip format:

```
dist{“date time”}.zip
```

## 4.Upload to the Server

The project path is:

```
/data/terramours/client/terramoursvue
```

Upload the compressed file to the designated folder on the server. XFTP is recommended for visual operation.

![image-20230524155722403](https://www.raokun.top/upload/2023/05/image-20230524155722403.png)

## 5.Unzip the Files

Run the unzip command in the project folder:

```shell
cd /data/terramours/client/terramoursvue
```

```shell
unzip dist.zip --改成压缩包名称，如果是替换，输入A，全部覆盖
```

## 6.Create a Dockerfile

Create a Dockerfile in the folder with this content:

```
FROM nginx
  
MAINTAINER ps
 
RUN rm /etc/nginx/conf.d/default.conf  
 
ADD default.conf /etc/nginx/conf.d/ 
 
COPY dist/ /usr/share/nginx/html/
```

## 7.Create default.conf

Create default.conf for configuring Nginx inside the container:

```
server {
    listen       8081;# 配置端口
    server_name  http://43.134.164.127/; # 修改为docker服务宿主机的ip
 
    location / {
        root   /usr/share/nginx/html;
        index  index.html index.htm;
        try_files $uri $uri/ /index.html =404;
    }
 
    error_page   500 502 503 504  /50x.html;
    location = /50x.html {
        root   html;
    }
}
```

## 8.Create a New Script File: terramoursvue.sh

Create a new script file for easy command execution:

Filename: terramoursvue.sh

Content:

```
#!/bin/bash

echo "Stopping terramoursvue container..."
docker stop terramoursvue
echo "Removing terramoursvue container..."
docker rm terramoursvue
echo "Removing terramoursvue image..."
docker rmi terramoursvue
echo "Building terramoursvue image..."
docker build -t terramoursvue .
echo "Running terramoursvue container..."
docker run --name terramoursvue -p 8089:8081 -d terramoursvue

exit 0

```

## 

## 9. Run the Script: terramours.sh
Execute the script:

```shell
sh terramoursvue.sh
```



The frontend project has been deployed to Docker, and the project port is 8089.

## 10. View Portainer's Container Management:
Check the containers, and the deployment is successful:

![image-20230524160641351](https://www.raokun.top/upload/2023/05/image-20230524160641351.png)

