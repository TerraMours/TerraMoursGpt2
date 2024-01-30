import { VitePWA } from 'vite-plugin-pwa';

export default function setupVitePwa() {
  return VitePWA({
    registerType: 'autoUpdate',
    includeAssets: ['favicon.ico'],
    manifest: {
      name: 'TerraMoursAdmin',
      short_name: 'TerraMoursAdmin',
      theme_color: '#fff',
      icons: [
        {
          src: '/logoko.png',
          sizes: '192x192',
          type: 'image/png'
        },
        {
          src: '/logoko.png',
          sizes: '512x512',
          type: 'image/png'
        },
        {
          src: '/logoko.png',
          sizes: '512x512',
          type: 'image/png',
          purpose: 'any maskable'
        }
      ]
    }
  });
}
