# TerraMoursVue-V0.1部署
## 1.服务器项目文件夹创建
演示站点：[http://43.134.164.127:8089/](http://43.134.164.127:8089/)

创建项目文件夹，用于管理后端接口代码

```
/data/terramours/client/terramoursvue
```

给文件夹赋权限

```shell
cd /data
```

```shell
chmod -R 777 terramours
```

## 2.项目构建

运行构建命令：

```shell
pnpm build
```



![image-20230524155502411](https://www.raokun.top/upload/2023/05/image-20230524155502411.png)

构建后的文件在dist文件夹中

## 3.代码打包压缩

发布成功后点击-打开文件夹，选择上一级目录，打包整个publish文件夹，打包成zip文件准备上传服务器。

zip格式：

```
dist{“日期时间”}.zip
```

## 4.上传至服务器

项目路径为

```
/data/terramours/client/terramoursvue
```

将压缩包上传至服务器指定文件夹中。推荐使用XFTP，可视化操作

![image-20230524155722403](https://www.raokun.top/upload/2023/05/image-20230524155722403.png)

## 5.解压文件

到项目文件夹下运行解压命令

```shell
cd /data/terramours/client/terramoursvue
```

```shell
unzip dist.zip --改成压缩包名称，如果是替换，输入A，全部覆盖
```

## 6.创建Dockerfile

在文件夹中创建Dockerfile,文件内容：

```
FROM nginx
  
MAINTAINER ps
 
RUN rm /etc/nginx/conf.d/default.conf  
 
ADD default.conf /etc/nginx/conf.d/ 
 
COPY dist/ /usr/share/nginx/html/
```

## 7.创建default.conf

创建default.conf，用于容器内nginx配置

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

## 8.新建脚本文件terramoursvue.sh

新建一个脚本文件，方便执行脚本命令

文件名称：terramoursvue.sh

内容：

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

## 9.运行terramours.sh

执行脚本

```shell
sh terramoursvue.sh
```



前端项目发布docker完成，项目端口8089.

## 10.查看portainer的容器管理：

查看容器，已经启动成功

![image-20230524160641351](https://www.raokun.top/upload/2023/05/image-20230524160641351.png)

