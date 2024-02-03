#该镜像是基于nginx:latest镜像构建的
FROM nginx
#作者名  
MAINTAINER raokun
#删除目录下的default.conf文件
RUN rm /etc/nginx/conf.d/default.conf  
#将default.conf复制到/etc/nginx/conf.d/下，用本地的default.conf配置来替换nginx镜像里的默认配置 
ADD default.conf /etc/nginx/conf.d/ 
#将项目根目录下dist文件夹复制到镜像/usr/share/nginx/html/目录下 
COPY dist/ /usr/share/nginx/html/


# sed [-nefr] [动作]
# 选项与参数：
# -n ：使用安静(silent)模式。在一般 sed 的用法中，所有来自 STDIN 的数据一般都会被列出到终端上。但如果加上 -n 参数后，则只有经过sed 特殊处理的那一行(或者动作)才会被列出来。
# -e ：直接在命令列模式上进行 sed 的动作编辑；
# -f ：直接将 sed 的动作写在一个文件内， -f filename 则可以运行 filename 内的 sed 动作；
# -r ：sed 的动作支持的是延伸型正规表示法的语法。(默认是基础正规表示法语法)
# -i ：直接修改读取的文件内容，而不是输出到终端。
# sed -i 's/原字符串/新字符串/' /home/1.txt
# sed -i 's/原字符串/新字符串/g' /home/1.txt

CMD ["/bin/bash", "-c", "sed -i \"s@<html@<html env_now=\"$VUE_APP_API_BASE_URL\"@\" /usr/share/nginx/html/index.html; nginx -g \"daemon off;\""]