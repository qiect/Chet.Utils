// @ts-check
import { defineConfig } from 'astro/config';
import vue from '@astrojs/vue';
import tailwindcss from '@tailwindcss/vite';
import sitemap from '@astrojs/sitemap';

export default defineConfig({
  site: 'https://qiect.github.io',
  base: '/Chet.Utils/',
  integrations: [vue(), sitemap()],
  vite: {
    plugins: [tailwindcss()]
  },
  markdown: {
    shikiConfig: {
      theme: 'one-dark-pro',
      langs: ['csharp', 'xml', 'json', 'bash', 'powershell'],
    },
  },
});
