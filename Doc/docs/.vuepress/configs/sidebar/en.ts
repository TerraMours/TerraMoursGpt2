import type { SidebarConfig } from 'vuepress'

export const sidebarEn: SidebarConfig = {
    '/en/guide/': [
        {
            text: 'TerraMours-GPT Usages',
            children: [
                '/en/guide/gpt-use.md',
                '/en/guide/admin-show.md',
            ],
        },
        {
            text: 'TerraMours-GPT Guide',
            children: [
                '/en/guide/README.md',
                // '/en/guide/getting-started.md',
                // '/en/guide/getting-started-vue.md',
            ],
        },
      ],
}