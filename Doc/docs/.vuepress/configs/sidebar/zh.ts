import type { SidebarConfig } from 'vuepress'

export const sidebarZh: SidebarConfig = {
    '/guide/': [
        {
            text: '快速了解TerraMoursGPT',
            children: [
                '/guide/README.md',
                // '/guide/getting-started.md',
                // '/guide/getting-started-vue.md',
            ],
        },
        {
            text: '开发与部署指南',
            children: [
                '/guide/build.md',
                '/guide/docker.md',
                '/guide/qa.md',
            ],
        },
        {
            text: '版本更新/升级操作',
            children: [
                '/guide/upgrading/update.md',
                '/guide/upgrading/01.md',
            ],
        },
        {
            text: '基础教程',
            children: [
                '/guide/admin-show.md',
                '/guide/gpt-use.md',
            ],
        },
        {
            text: '协议',
            children: [
                '/guide/agreement/open-source.md',
                '/guide/agreement/disclaimer.md',
            ],
        },
      ],  
}