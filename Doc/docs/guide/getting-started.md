# TerraMoursApi-V0.1部署
## 1.服务器项目文件夹创建

创建项目文件夹，用于管理后端接口代码

```
/data/terramours/api
```

给文件夹赋权限

```shell
cd /data
```

```shell
chmod -R 777 terramours
```

## 2.项目发布

右键项目节点 > 发布 > 选择发布到文件夹

![image-20230524153537256](https://www.raokun.top/upload/2023/05/image-20230524153537256.png)

选择好对应的运行环境，发布服务器选linux-64

![image-20230524153736263](https://www.raokun.top/upload/2023/05/image-20230524153736263.png)

发布Dockerfile(或者在服务器publish文件夹中创建Dockerfile)：

![image-20230524155959756](https://www.raokun.top/upload/2023/05/image-20230524155959756.png)

Dockerfile内容：

```
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM base AS final
WORKDIR /app
COPY .  /app
ENTRYPOINT ["dotnet", "TerraMours.dll"]
```

## 3.代码打包压缩

发布成功后点击-打开文件夹，选择上一级目录，打包整个publish文件夹，打包成zip文件准备上传服务器。

zip格式：

```
publish{“日期时间”}.zip
```



![image-20230524154015606](https://www.raokun.top/upload/2023/05/image-20230524154015606.png)

## 4.上传至服务器

项目路径为

```
/data/terramours/api
```

将压缩包上传至服务器指定文件夹中。推荐使用XFTP，可视化操作

![image-20230524154252126](https://www.raokun.top/upload/2023/05/image-20230524154252126.png)

## 5.解压文件

到项目文件夹下运行解压命令

```shell
cd /data/terramours/api
```

```shell
unzip publish0521.zip --改成压缩包名称，如果是替换，输入A，全部覆盖
```

## 6.新建脚本文件terramours.sh

新建一个脚本文件，方便执行脚本命令

文件名称：terramours.sh

内容：

```
#!/bin/bash
cd publish
echo "Stopping docker container..."
docker stop terramours
echo "Removing docker container..."
docker rm terramours
echo "Removing docker image..."
docker rmi terramours
echo "Building docker image..."
docker build -t terramours .
echo "Running docker container..."
docker run --name terramours -p 4112:80 -d terramours

```

## 7.运行terramours.sh

执行脚本

```shell
sh terramours.sh
```



后端项目发布docker完成，项目端口4112.

## 8.查看portainer的容器管理：

查看容器，已经启动成功

![image-20230524155253942](https://www.raokun.top/upload/2023/05/image-20230524155253942.png)
