# 常见开发 & 部署问题

TerraMoursGPT 常见开发 & 部署问题

# 错误排查方式
遇到问题先按下面方式排查。
* ` docker ps -a ` 查看所有容器运行状态，检查是否全部 running，如有异常，尝试docker logs 容器名查看对应日志。
* 不懂 docker 不要瞎改端口，只需要改 `DEFAULT_ROOT_PSW` 即可
* 系统报错，通过seq查看，查看地址：`http://<YOUR-SERVER-IP>:5341/`
* 无法解决时，可以找找Issue，或新提 Issue，私有部署错误，务必提供详细的日志，否则很难排查。
* seq日志中显示`初始化数据库成功` 即代表后端服务初始化成功，首次安装可能会有报错的现象，建议dockercompose安装完成后重启terramours_gpt_server容器