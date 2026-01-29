import path from "path";
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import checker from "vite-plugin-checker";

// https://vite.dev/config/
export default defineConfig({
  plugins: [
      react(),
    checker({
      typescript: {
        tsconfigPath: './tsconfig.app.json',
        buildMode: false,
      },
      overlay: true,
      terminal: true,
      enableBuild: false,
    }),
  ],
  resolve: {
    alias: {
      src: path.resolve(__dirname, "src"),
    },
  },
  server: {
    port: 5175,
  },
})
