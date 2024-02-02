---
home: true
heroImage: images/hero.png
heroText: TerraMours
actions:
  - text: GPT管理系统
    link: /guide/
    type: primary
  - text: GPT站点
    link: https://first.terramours.site/
    type: secondary
features:
- title: 简洁高效
  details: TerraMours 采用了 Masa的MinimalAPI，通过极简的代码实现了高效的 HTTP API 接口的同时，提供更高的性能和更低的延迟。
- title: 领域驱动设计(DDD)
  details: TerraMours 框架实现领域驱动设计(DDD)的落地，提供更好的业务识别和管理，更好的模块划分，更好的数据映射，更好的维护性和可扩展性。
- title: 扩展开发
  details: TerraMours 已经实现了用户管理、权限验证、日志管理等基础功能，开发者可以根据业务需求快速开发相关业务系统。
footer: MIT Licensed | Copyright (c) 2023 terramours
---
### Docker Compose 快速部署

#### 一、安装 Docker 和 docker-compose

```
# 安装 Docker
curl -fsSL https://get.docker.com | bash -s docker --mirror Aliyun
systemctl enable --now docker
# 安装 docker-compose
curl -L https://github.com/docker/compose/releases/download/2.20.3/docker-compose-`uname -s`-`uname -m` -o /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose
# 验证安装
docker -v
docker-compose -v
# 如失效，自行百度~
```

#### 二、创建目录并下载 docker-compose.yml

依次执行下面命令，创建 terramours 文件并拉取 docker.zip 压缩包，解压后会有 docker-compose.yml和nginx文件夹。

非 Linux 环境或无法访问外网环境，可手动创建一个目录，并下载链接文件: docker.zip


```
mkdir terramours
cd terramours
curl -O https://raw.githubusercontent.com/labring/TerraMoursGpt2/main/Docker/Docker.zip
```

#### 三、启动容器

```
# 在 docker-compose.yml 同级目录下执行
docker-compose pull
docker-compose up -d
```

#### 四、访问 TerraMoursGpt2

目前可以通过 ip:8089 直接访问(注意防火墙)。登录用户名为` terramours@163.com`，密码为docker-compose.yml环境变量里设置的 DEFAULT_ROOT_PSW，默认值：`terramours@163.com`。

如果需要域名访问，请根据nginx/conf.d文件夹下default.conf 内容自行更新。
