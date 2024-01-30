<div align="center">
	<h1>TerraMours Gpt Vector</h1>
</div>

![](https://img.shields.io/github/stars/TerraMours/TerraMours_Gpt_Api) ![](https://img.shields.io/github/forks/TerraMours/TerraMours_Gpt_Api)

中文简介 | [English](README-EN.md)

## 简介

本地向量库项目，通过pgvector实现使用postgresql做向量库。

## 特性

##### 1.使用postgresql做向量库，可以业务数据同时存在一个数据库中间件中，节约成本

##### 2.PostgreSQL是一个强大的对象关系数据库系统，可在开源许可下使用。

##### 3.接口同pinecone接口相同，可通过http接口快速调用，也可以通过 Terramours_Gpt_Vector SDK整合调用

##### 4.apikey签发




## 开发功能

- [X] apikey
- [X] Index
- [X] vector



## 更新数据库
```
Add-Migration update

Add-Migration update -context FrameworkDbContext
update-database
```

## 开源作者

[@Raokun](https://github.com/raokun)
