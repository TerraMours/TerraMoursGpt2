import type { NavbarConfig } from 'vuepress'
export const navbarZh: NavbarConfig =[
    {
        text: '指南',
        link: '/guide/',
      },
      {
        text: '项目开发者',
        children: [
          {
            text:"raokun",
            link:"https://www.raokun.top/"
          },
          {
            text:"firstsaofan",
            link:"https://www.firstsaofan.top/"
          },
        ],
      },
]