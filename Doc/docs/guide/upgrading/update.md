# 升级说明
镜像名
docker
* api 镜像 raokun88/terramours_gpt_server:latest
* web 镜像 raokun88/terramours_gpt_admin:latest 

Docker-Compose 修改镜像
直接修改yml文件中的image: 即可。随后执行:
```
docker-compose pull
docker-compose up -d
```