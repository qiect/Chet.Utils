// @ts-check
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import { ion } from 'starlight-ion-theme';

// https://astro.build/config
export default defineConfig({
  site: 'https://qiect.github.io',
  base: '/Chet.Utils/',
  integrations: [
    starlight({
      title: 'Chet.Utils',
      logo: { src: './src/assets/logo.svg', alt: 'Chet.Utils' },
      customCss: ['./src/styles/custom.css'],
      defaultLocale: 'root',
      locales: {
        root: {
          label: '简体中文',
          lang: 'zh-CN',
        },
      },
      head: [
        {
          tag: 'script',
          attrs: { is: 'inline' },
          content: `
            (function() {
              var stored = localStorage.getItem('starlight-theme');
              if (!stored) {
                document.documentElement.dataset.theme = 'dark';
              }
            })();
          `,
        },
      ],
      social: [
        { icon: 'github', label: 'GitHub', href: 'https://github.com/qiect/Chet.Utils' },
      ],
      sidebar: [
        {
          label: '[lucide:rocket] 快速开始',
          slug: 'getting-started',
        },
        {
          label: '[lucide:puzzle] 扩展方法',
          items: [{ autogenerate: { directory: 'extensions', collapsed: true } }],
        },
        {
          label: '[lucide:wrench] 帮助类',
          items: [{ autogenerate: { directory: 'helpers', collapsed: true } }],
        },
      ],
      plugins: [
        ion({
          icons: {
            iconDir: './src/icons',
          },
          footer: {
            text: '© 2024 Chet.Utils. MIT License.',
            links: [
              { label: 'GitHub', link: 'https://github.com/qiect/Chet.Utils', newTab: true },
              { label: 'NuGet', link: 'https://www.nuget.org/packages/Chet.Utils', newTab: true },
            ],
          },
        }),
      ],
    }),
  ],
});
