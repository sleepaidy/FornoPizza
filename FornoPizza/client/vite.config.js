import react from '@vitejs/plugin-react'
import { defineConfig } from 'vite'

export default defineConfig({
  plugins: [react()],
  base: './',
  build: {
    outDir: '../wwwroot/menu',
    emptyOutDir: true,
    rollupOptions: {
      output: {
        entryFileNames: 'menu.js',
        assetFileNames: 'menu.[ext]',
      },
    },
  },
  server: {
    proxy: {
      '/Home/Menu': 'http://localhost:5297',
      '/Home/CreateOrderJson': 'http://localhost:5297',
    },
  },
})