import { HttpClient } from "src/shared-module/api/http-client/http-client";
import { authStore } from "src/auth-module/store/auth.store.ts";

export const appHttpClient = new HttpClient({
  baseUrl: import.meta.env.VITE_API_URL,
  onTokenRefresh: async () => {
    if (window.location.pathname === "/auth/login") {
      return null;
    }
    return await authStore.signInWithRefreshToken();
  },
});

export const anonymousHttpClient = new HttpClient({
  baseUrl: import.meta.env.VITE_API_URL,
});

export { HttpClient };
